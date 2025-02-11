using FlightApp.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using FlightAppLibrary.Models.Enums;

namespace FlightApp.Entities
{
    public class Notification
    {
        [Key]
        public int NotificationId { get; set; }
        public NotificationType NotificationType { get; set; }
        public string TargetId { get; set; }
        public string SenderId { get; set; }
        public string Content { get; set; } = "New Notification";
        public DateTime TimeStamp { get; set; }
        public bool IsRead { get; set; }
    }
}
