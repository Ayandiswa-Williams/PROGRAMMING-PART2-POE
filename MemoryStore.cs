
namespace CybersecurityChatbot
{
    public class MemoryStore
    {
        public string UserName { get; set; } = "";
        public string FavouriteTopic { get; set; } = "";

        public void Store(string key, string value)
        {
            if (key.ToLower().Contains("topic") || key.ToLower().Contains("interest"))
                FavouriteTopic = value;
        }

        public string Recall(string key)
        {
            return key.ToLower().Contains("name") ? UserName : "";
        }

        public string GetPersonalisedOpener()
        {
            return string.IsNullOrEmpty(UserName) ? "" : $"Hey {UserName}, ";
        }
    }
}
