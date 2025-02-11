using FlightApp.Database;
using FlightApp.Entities;
using FlightAppLibrary.Models.Dtos;
using FlightAppLibrary.Models.Enums;

namespace FlightApp.Repository
{
    public interface INotificationsRepository
    {
        public Notification? AddNotification(Notification vote);
        List<Notification>? FetchReports();
        public bool DeleteNotificationById(int id);
        public List<Notification>? FetchUserNotifications(string userId);
    }

    public class NotificationsRepository : INotificationsRepository
    {
        private readonly FlightAppDbContext _db;

        public NotificationsRepository(FlightAppDbContext db)
        {
            _db = db;
        }
        public Notification? AddNotification(Notification notification)
        {
            try
            {
                _db.Notifications.Add(notification);
                _db.SaveChanges();
                return notification;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return null; 
            }
        }

        public List<Notification>? FetchReports()
        {
            try
            {
                return _db.Notifications
                    .Where(n => n.NotificationType == NotificationType.Report || n.NotificationType == NotificationType.Issue)
                    .ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return null;
            }
        }

        public List<Notification>? FetchUserNotifications(string userId)
        {
            try
            {
                return _db.Notifications
                    .Where(n => n.TargetId == userId)
                    .Where(n => n.NotificationType == NotificationType.VoteMilestone || n.NotificationType == NotificationType.LevelUp)
                    .ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return null;
            }
        }

        public bool DeleteNotificationById(int id)
        {
            try
            {
                var notification = _db.Notifications.Find(id);

                if(notification != null)
                {
                    _db.Remove(notification);
                    _db.SaveChanges();
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex);
                return false;
            }
        }
    }
}
