using FlightApp.Entities;
using FlightApp.Repository;
using FlightAppLibrary.Models.Dtos;

namespace FlightApp.Service
{
    public interface INotificationsService
    {
        public NotificationDto AddNotification(NotificationDto notificationDto);
        public List<NotificationDto>? FetchReports();
        public bool DeleteNotificationById(int id);
        public List<NotificationDto>? FetchUserNotifications(string userId);
    }

    public class NotificationsService : INotificationsService
    {
        private readonly INotificationsRepository _notificationsRepository;

        public NotificationsService(INotificationsRepository notificationsRepository)
        {
            _notificationsRepository = notificationsRepository;
        }

        public NotificationDto AddNotification(NotificationDto notificationDto)
        {
            Notification notification = new()
            {
                NotificationType = notificationDto.NotificationType,
                TargetId = notificationDto.TargetId,
                SenderId = notificationDto.SenderId,
                Content = notificationDto.Content,
                TimeStamp = notificationDto.TimeStamp,
                IsRead = notificationDto.IsRead,
            };

            _notificationsRepository.AddNotification(notification);
            return notificationDto;
        }

        public List<NotificationDto>? FetchReports()
        {
            var reports = _notificationsRepository.FetchReports();

            if(reports != null)
            {
                return reports.Select(r => new NotificationDto
                {
                    NotificationId = r.NotificationId,
                    NotificationType = r.NotificationType,
                    TargetId = r.TargetId,
                    SenderId = r.SenderId,
                    Content = r.Content,
                    TimeStamp = r.TimeStamp,
                    IsRead = r.IsRead,
                }).ToList();
            }
            return [];
        }

        public List<NotificationDto>? FetchUserNotifications(string userId)
        {
            var reports = _notificationsRepository.FetchUserNotifications(userId);

            if (reports != null)
            {
                return reports.Select(r => new NotificationDto
                {
                    NotificationId = r.NotificationId,
                    NotificationType = r.NotificationType,
                    TargetId = r.TargetId,
                    SenderId = r.SenderId,
                    Content = r.Content,
                    TimeStamp = r.TimeStamp,
                    IsRead = r.IsRead,
                }).ToList();
            }
            return [];
        }

        public bool DeleteNotificationById(int id)
        {
            return _notificationsRepository.DeleteNotificationById(id);
        }
    }
}
