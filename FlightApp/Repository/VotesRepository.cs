using FlightApp.Database;
using FlightApp.Entities;
using FlightApp.Models;
using FlightAppLibrary.Models.Dtos;
using FlightAppLibrary.Models.Enums;
using Microsoft.AspNetCore.Identity;

namespace FlightApp.Repository
{
    public interface IVotesRepository
    {
        public Task<int> AddVote(Vote vote, string commenterId);
    }

    public class VotesRepository : IVotesRepository
    {
        private readonly FlightAppDbContext _db;

        private readonly UserManager<ApplicationUser> _userManager;
        public VotesRepository(FlightAppDbContext db, UserManager<ApplicationUser> userManager)
        {
            _db = db;
            _userManager = userManager;
        }

        public async Task<int> AddVote(Vote vote, string commenterId)
        {
            try
            {
                int value = vote.Value;

                var sameVote = _db.Votes.FirstOrDefault(
                    v => v.UserId == vote.UserId && v.ReplyId == vote.ReplyId && v.NoteId == null || 
                    v.UserId == vote.UserId && v.NoteId == vote.NoteId && v.ReplyId == null);

                if(sameVote != null && sameVote.Value == vote.Value)
                {
                    return 0;
                }
                else if(sameVote != null && sameVote.Value != vote.Value)
                {
                    value = vote.Value * 2;
                    sameVote.Value = vote.Value;
                }
                else
                {
                    _db.Votes.Add(vote);
                }

                if(vote.CommentType == CommentType.Note)
                {
                    var note = _db.Notes.FirstOrDefault(n => n.NoteId == vote.NoteId);
                    note!.Karma += value;
                    if(note!.Karma % 5 == 0 && note!.Karma > 0)
                    {
                        Notification notification = new()
                        {
                            NotificationType = NotificationType.VoteMilestone,
                            TargetId = commenterId,
                            SenderId = commenterId,
                            Content = $"Your comment received {note!.Karma} upvotes!",
                            TimeStamp = DateTime.Now,
                            IsRead = false
                        };
                        _db.Notifications.Add(notification);
                    }
                }
                else
                {
                    var reply = _db.Replies.FirstOrDefault(r => r.ReplyId == vote.ReplyId);
                    reply!.Karma += value;
                    if (reply!.Karma % 5 == 0 && reply!.Karma > 0)
                    {
                        Notification notification = new()
                        {
                            NotificationType = NotificationType.VoteMilestone,
                            TargetId = commenterId,
                            SenderId = commenterId,
                            Content = $"Your comment received {reply!.Karma} upvotes!",
                            TimeStamp = DateTime.Now,
                            IsRead = false
                        };
                        _db.Notifications.Add(notification);
                    }
                }

                ApplicationUser? user = await _userManager.FindByIdAsync(commenterId);
                if (user != null)
                {
                    user.Karma += value;
                    if(user.Karma % 10 == 0)
                    {
                        Notification notification = new()
                        {
                            NotificationType = NotificationType.LevelUp,
                            TargetId = commenterId,
                            SenderId = commenterId,
                            Content = "You have gone up a level!",
                            TimeStamp = DateTime.Now,
                            IsRead = false
                        };
                        _db.Notifications.Add(notification);
                    }
                }

                _db.SaveChanges();
                return value;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return 0;
            }
        }
    }
}
