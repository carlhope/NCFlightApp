
namespace FlightAppFrontend.Shared.Services
{
    public static class UserService
    {
        public static readonly List<string> Avatars =
        [
            "_content/FlightAppFrontend.Shared/Images/Avatars/1.png",
            "_content/FlightAppFrontend.Shared/Images/Avatars/2.png",
            "_content/FlightAppFrontend.Shared/Images/Avatars/3.png",
            "_content/FlightAppFrontend.Shared/Images/Avatars/4.png",
            "_content/FlightAppFrontend.Shared/Images/Avatars/5.png",
            "_content/FlightAppFrontend.Shared/Images/Avatars/6.png",
            "_content/FlightAppFrontend.Shared/Images/Avatars/7.png",
            "_content/FlightAppFrontend.Shared/Images/Avatars/8.png",
            "_content/FlightAppFrontend.Shared/Images/Avatars/9.png",
            "_content/FlightAppFrontend.Shared/Images/Avatars/10.png",
            "_content/FlightAppFrontend.Shared/Images/Avatars/11.png",
            "_content/FlightAppFrontend.Shared/Images/Avatars/12.png",
            "_content/FlightAppFrontend.Shared/Images/Avatars/13.png",
            "_content/FlightAppFrontend.Shared/Images/Avatars/14.png",
            "_content/FlightAppFrontend.Shared/Images/Avatars/15.png",
            "_content/FlightAppFrontend.Shared/Images/Avatars/16.png"
        ];

        public static readonly List<string> Pronouns =
        [
            "",
            "(She/Her)",
            "(He/Him)",
            "(They/Them)"
        ];

        public static string UserRank(int karma)
        {
            if(karma < 0)
            {
                return "Grounded";
            }
            else if(karma < 10)
            {
                return "Cleared for Takeoff";
            }
            else if(karma < 20)
            {
                return "Frequent Flyer";
            }
            else if(karma < 30)
            {
                return "Aero Enthusiast";
            }
            else if(karma < 40)
            {
                return "Jet Setter";
            }
            else if(karma < 50)
            {
                return "Top Gun";
            }
            else if(karma < 60)
            {
                return "Cloud Rider";
            }
            return "Cleared for Takeoff";
        }
    }
}
