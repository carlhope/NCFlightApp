
using FlightAppLibrary.Models.Enums;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace FlightAppLibrary.Models.Dtos
{
    public class NotificationDto
    {
        [JsonPropertyName("notificationId")]
        public int NotificationId { get; set; } = 0;

        [JsonPropertyName("notificationType")]
        public NotificationType NotificationType { get; set; }

        [JsonPropertyName("targetId")]
        public string TargetId { get; set; }

        [JsonPropertyName("senderId")]
        public string SenderId { get; set; }

        [JsonPropertyName("content")]
        [Required]
        [MinLength(10, ErrorMessage = "Must be at least ten characters")]
        public string Content { get; set; }

        [JsonPropertyName("timeStamp")]
        public DateTime TimeStamp { get; set; }

        [JsonPropertyName("isRead")]
        public bool IsRead { get; set; } = false;
    }
}
