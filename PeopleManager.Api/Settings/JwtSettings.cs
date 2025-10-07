namespace PeopleManager.Api.Settings
{
    public class JwtSettings
    {
        public required string Secret { get; set; }

        public TimeSpan ExpiryTime { get; set; }
    }
}
