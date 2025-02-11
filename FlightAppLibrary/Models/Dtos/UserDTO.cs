using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace FlightAppLibrary.Models.Dtos
{
    public class UserDTO
    {
        [JsonPropertyName("id")]
        public string? Id { get; set; }
        [JsonPropertyName("title")]
        public string? Pronouns { get; set; }

        [JsonPropertyName("firstName")]
        [Required(ErrorMessage = "First name is required")]
        public string? FirstName { get; set; }

        [JsonPropertyName("lastName")]
        [Required(ErrorMessage = "Second name is required")]
        public string? LastName { get; set; }

        [JsonPropertyName("displayName")]
        [MinLength(3, ErrorMessage = "Must be at least three characters")]
        public string? DisplayName { get; set; }

        [JsonPropertyName("email")]
        [Required(ErrorMessage = "Email is required")]
        [RegularExpression(@"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$", ErrorMessage = "Invalid Email Format")]
        public string Email { get; set; }
        [JsonPropertyName("phoneNumber")]
        public string? PhoneNumber { get; set; }
        [JsonPropertyName("karma")]
        public int Karma { get; set; }

        [JsonPropertyName("avatar")]
        public string Avatar { get; set; }
    }
}
