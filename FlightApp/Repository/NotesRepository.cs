using FlightApp.Database;
using FlightApp.Entities;
using Microsoft.EntityFrameworkCore;

namespace FlightApp.Repository
{
    public interface INotesRepository
    {
        List<Note> GetNotes();
        Note AddNote(Note note);
        List<Note> GetNotesByIataAndDateTime(string flightIata, DateTime dateTimeScheduled);
        Note GetNoteById(int id);
        List<Note> GetNotesByUserId(string userId);
        Note DeleteNoteById(int id);
    }

    public class NotesRepository : INotesRepository
    {
        private readonly FlightAppDbContext db;
        public NotesRepository(FlightAppDbContext _db)
        {
            db = _db;
        }

        // ------GET REQUESTS------
        public List<Note> GetNotes()
        {
            try
            {
                return db.Notes.ToList();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return null;
            }
        }
        public Note GetNoteById(int id)
        {
            try
            {
                return db.Notes.Where(n => n.NoteId == id)
                    .Include(n => n.User)
                    .Include(n => n.Replies)
                    .FirstOrDefault()!;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return null;
            }
        }
        public List<Note> GetNotesByUserId(string userId)
        {
            try
            {
                return db.Notes.Where(n => n.UserId == userId)
                    .Include(n => n.User)
                    .Include(n => n.Replies)
                    .ToList()!;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return null;
            }
        }
        public List<Note> GetNotesByIataAndDateTime(string flightIata, DateTime dateTimeScheduled)
        {
            try
            {
                var res = db.Notes.Where(n => n.FlightIata == flightIata && n.ScheduledDeparture == dateTimeScheduled)
                    .Include(n => n.User)
                    .Include(n => n.Votes)
                    .Include(n => n.Replies)!
                    .ThenInclude(n => n.User)!
                    .Include(n => n.Replies)!
                    .ThenInclude(r => r.Votes)
                    .ToList();
               
                return res;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return null;
            }
        }
        // -----POST REQUESTS-----
        public Note AddNote(Note note)
        {
            try
            {
                db.Notes.Add(note);
                db.SaveChanges();
                return note;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return null;
            }
        }
        // -----DELETE REQUESTS-----
        public Note DeleteNoteById(int id)
        {
            try
            {
                var replies = db.Replies.Where(r => r.NoteId == id).ToList();
                foreach(Reply reply in replies) 
                { 
                    db.Votes.RemoveRange(db.Votes.Where(v => v.ReplyId == reply.ReplyId));
                }

                db.Votes.RemoveRange(db.Votes.Where(v => v.NoteId == id));

                Note note = db.Notes.Where(n => n.NoteId == id).FirstOrDefault()!;
                db.Notes.Remove(note);
                db.SaveChanges();
                return note;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return null;
            }
        }
    }
}
