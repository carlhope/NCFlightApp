using FlightApp.Service;
using FlightAppLibrary.Models.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FlightApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class NotificationsController : ControllerBase
    {
        private readonly INotificationsService _notificationsService;

        public NotificationsController(INotificationsService notificationsService)
        {
            _notificationsService = notificationsService;
        }

        //-----POST REQUESTS-----
        [HttpPost]
        [Authorize]
        public IActionResult PostNotification([FromBody] NotificationDto notificationDto)
        {
            if (User != null)
            {
                var userId = User.FindFirst("Id");
                string userIdValue = userId!.Value;
                var result = _notificationsService.AddNotification(notificationDto);
                return result == null ? BadRequest($"Unable to process {notificationDto.NotificationType.ToString()}") : Ok(result);
            }
            return BadRequest($"User info may not be correct");
        }

        [HttpGet("Reports")]
        [Authorize(Roles="Admin")]
        public IActionResult GetReports()
        {
            var result = _notificationsService.FetchReports();
            return result == null ? BadRequest($"Unable to find reports") : Ok(result);
        }

        [HttpGet]
        [Authorize]
        public IActionResult GetUserNotifications()
        {
            var userId = User.FindFirst("Id");
            string userIdValue = userId!.Value;
            var result = _notificationsService.FetchUserNotifications(userIdValue);
            return result == null ? BadRequest($"Unable to find notifications") : Ok(result);
        }

        [HttpDelete("{id}")]
        [Authorize]
        public IActionResult DeleteNotificationById(int id)
        {
            var result = _notificationsService.DeleteNotificationById(id);
            return result == false ? BadRequest($"Unable to delete") : Ok(result);
        }
    }
}
