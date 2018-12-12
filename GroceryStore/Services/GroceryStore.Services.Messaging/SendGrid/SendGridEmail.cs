using Newtonsoft.Json;

namespace GroceryStore.Services.Messaging.SendGrid
{
    public class SendGridEmail
    {
        public SendGridEmail()
        {
        }

        public SendGridEmail(string email, string name = null)
        {
            Email = email;
            Name = name;
        }

        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }
    }
}
