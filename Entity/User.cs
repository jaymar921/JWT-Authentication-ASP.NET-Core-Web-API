using System.Text.Json.Serialization;

namespace JWT_Authentication_ASP.NET_Core_Web_API.Entity
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Username { get; set; }

        [JsonIgnore]
        public string Password { get; set; }
    }
}
