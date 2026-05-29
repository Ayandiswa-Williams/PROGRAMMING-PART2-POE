
using System;
using System.Collections.Generic;

namespace CybersecurityChatbot
{
    public enum Sentiment { Neutral, Worried, Curious, Frustrated, Happy }

    public class SentimentDetector
    {
        private readonly Dictionary<Sentiment, List<string>> _triggers = new();

        public SentimentDetector()
        {
            _triggers[Sentiment.Worried] = new List<string> { "worried", "scared", "afraid", "anxious", "nervous", "unsafe", "hack" };
            _triggers[Sentiment.Curious] = new List<string> { "curious", "wonder", "how does", "explain", "interested", "learn" };
            _triggers[Sentiment.Frustrated] = new List<string> { "frustrated", "annoyed", "confused", "don't understand", "angry" };
            _triggers[Sentiment.Happy] = new List<string> { "great", "thanks", "helpful", "awesome", "love", "good" };
        }

        public Sentiment Detect(string input)
        {
            input = input.ToLower();
            foreach (var kvp in _triggers)
            {
                foreach (var word in kvp.Value)
                {
                    if (input.Contains(word))
                        return kvp.Key;
                }
            }
            return Sentiment.Neutral;
        }

        public string GetSentimentResponse(Sentiment sentiment)
        {
            return sentiment switch
            {
                Sentiment.Worried => "I understand your concern. Let's address this together:",
                Sentiment.Curious => "Great question! Here's what you should know:",
                Sentiment.Frustrated => "I see you're having trouble. Let me simplify this:",
                Sentiment.Happy => "Glad you're finding this useful! ",
                _ => ""
            };
        }
    }
}