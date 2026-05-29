
using System;
using System.Collections.Generic;
using System.Linq;

namespace CybersecurityChatbot
{
    public class KeywordResponder
    {
        private readonly Dictionary<string, List<string>> _responses = new();
        private readonly Random _random = new Random();
        private string _lastTopic = "";

        public KeywordResponder()
        {
            PopulateResponses();
        }

        private void PopulateResponses()
        {
            // === Expanded with full question support ===
            AddResponses(new[] { "password", "strong password", "create password" }, new List<string>
            {
                "A strong password should be at least 12 characters long, including uppercase, lowercase, numbers, and symbols. Use a password manager like Bitwarden or LastPass.",
                "Never reuse the same password across different websites. Enable Two-Factor Authentication (2FA) everywhere possible."
            });

            AddResponses(new[] { "phishing", "what is phishing", "phishing email" }, new List<string>
            {
                "Phishing is when attackers pretend to be trusted companies to trick you into giving away passwords or clicking malicious links. Always check the sender's email address carefully.",
                "Signs of phishing: urgent language, suspicious links, or requests for personal information. Hover over links before clicking!"
            });

            AddResponses(new[] { "privacy", "data privacy", "protect privacy" }, new List<string>
            {
                "Use privacy-focused tools like DuckDuckGo for searching, Signal for messaging, and a good VPN on public Wi-Fi.",
                "Review app permissions regularly and limit what you share on social media."
            });

            AddResponses(new[] { "malware", "virus", "trojan" }, new List<string>
            {
                "Keep your operating system and antivirus updated. Avoid downloading cracked software or clicking pop-up ads.",
                "Use Windows Defender or Malwarebytes for regular scans."
            });

            AddResponses(new[] { "ransomware", "ransom" }, new List<string>
            {
                "Ransomware locks your files and demands payment. The best defense is regular backups (3-2-1 rule: 3 copies, 2 media types, 1 offsite).",
                "Never pay the ransom — it funds more crime and there's no guarantee you'll get your files back."
            });

            AddResponses(new[] { "scam", "online scam", "fraud" }, new List<string>
            {
                "Common scams include fake tech support, romance scams, and fake investment opportunities. If it sounds too good to be true, it is.",
                "Verify any unexpected calls or messages before taking action."
            });

            AddResponses(new[] { "2fa", "two factor", "two-factor", "mfa" }, new List<string>
            {
                "Two-Factor Authentication greatly increases your security. Prefer authenticator apps (Google Authenticator, Authy) over SMS.",
                "Avoid SMS 2FA if possible due to SIM-swapping attacks."
            });

            AddResponses(new[] { "vpn", "virtual private network" }, new List<string>
            {
                "A VPN encrypts your internet connection, protecting you on public Wi-Fi and hiding your activity from your ISP.",
                "Choose reputable providers like Mullvad, ProtonVPN, or ExpressVPN that have a strict no-logs policy."
            });

            AddResponses(new[] { "social engineering", "social engineer" }, new List<string>
            {
                "Social engineering is manipulating people into breaking security procedures. Always be cautious when someone asks for sensitive information.",
                "Training and awareness are the best defenses against social engineering attacks."
            });

            AddResponses(new[] { "backup", "back up data" }, new List<string>
            {
                "Follow the 3-2-1 backup rule: 3 copies of your data, on 2 different types of media, with 1 copy offsite.",
                "Use tools like OneDrive, Google Drive, or external hard drives combined with cloud backup."
            });
        }

        private void AddResponses(string[] keywords, List<string> responses)
        {
            foreach (var keyword in keywords)
            {
                _responses[keyword.ToLower()] = responses;
            }
        }

        public string GetResponse(string input)
        {
            input = input.ToLower();

            foreach (var kvp in _responses.OrderByDescending(k => k.Key.Length))
            {
                if (input.Contains(kvp.Key))
                {
                    _lastTopic = kvp.Key;
                    var responses = kvp.Value;
                    return responses[_random.Next(responses.Count)];
                }
            }

            return "";
        }

        public string GetFollowUp()
        {
            if (string.IsNullOrEmpty(_lastTopic)) return "What else would you like to know about cybersecurity?";

            return $"Regarding {_lastTopic}: " + _responses[_lastTopic][_random.Next(_responses[_lastTopic].Count)];
        }

        public string GetAllTopics() => string.Join(", ", _responses.Keys);
    }
}