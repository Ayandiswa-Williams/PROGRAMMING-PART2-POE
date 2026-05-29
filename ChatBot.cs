// CybersecurityChatbot/ChatBot.cs
using System;

namespace CybersecurityChatbot
{
    public class ChatBot
    {
        private readonly KeywordResponder _keywords;
        private readonly SentimentDetector _sentiment;
        private readonly MemoryStore _memory;
        private bool _awaitingName = true;
        private string _lastTopic = "";

        public ChatBot()
        {
            _keywords = new KeywordResponder();
            _sentiment = new SentimentDetector();
            _memory = new MemoryStore();
        }

        public string GetGreeting()
        {
            return "Hello! I'm your Cybersecurity Awareness Bot. What's your name?";
        }

        public string ProcessInput(string input)
        {
            input = input.Trim();

            if (string.IsNullOrEmpty(input))
                return "Please type something so I can help you!";

            string lowerInput = input.ToLower();

            // 1. Capture Name
            if (_awaitingName)
            {
                _memory.UserName = input;
                _awaitingName = false;
                return $"Nice to meet you, {_memory.UserName}! Ask me anything about cybersecurity.";
            }

            // 2. Follow-up questions
            if (lowerInput.Contains("tell me more") || lowerInput.Contains("explain more") ||
                lowerInput.Contains("elaborate") || lowerInput.Contains("again"))
            {
                return _keywords.GetFollowUp();
            }

            // 3. Sentiment
            var sentiment = _sentiment.Detect(lowerInput);
            string emotion = _sentiment.GetSentimentResponse(sentiment);

            // 4. Keyword Response
            string response = _keywords.GetResponse(lowerInput);

            if (!string.IsNullOrEmpty(response))
            {
                _lastTopic = _keywords.GetAllTopics().Split(',').FirstOrDefault()?.Trim() ?? "";

                string fullResponse = string.IsNullOrEmpty(emotion) ? response : $"{emotion} {response}";

                // Personalization
                if (!string.IsNullOrEmpty(_memory.UserName))
                    fullResponse = $"{_memory.GetPersonalisedOpener()}{fullResponse}";

                return fullResponse;
            }

            // Special Commands
            if (lowerInput.Contains("how are you"))
                return "I'm running at full security capacity and ready to help! 😊";
            if (lowerInput.Contains("what can you") || lowerInput.Contains("help"))
                return $"I can answer questions about: {_keywords.GetAllTopics()}\nJust ask me!";

            // Default fallback
            return "I'm not sure about that one. Try asking about passwords, phishing, malware, VPNs, or backups!";
        }
    }
}
