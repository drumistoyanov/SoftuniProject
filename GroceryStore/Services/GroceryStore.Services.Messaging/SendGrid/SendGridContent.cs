using Newtonsoft.Json;

namespace GroceryStore.Services.Messaging.SendGrid
{
    public class SendGridContent
    {
        public SendGridContent()
        {
        }

        public SendGridContent(string type, string content)
        {
            Type = type;
            Value = content;
        }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("value")]
        public string Value { get; set; }
    }
}
