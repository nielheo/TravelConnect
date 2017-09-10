using System;

namespace TravelConnect.Sabre
{
    public class AccessToken
    {
        public int SabreCredentialId { get; set; }
        public string ClientId { get; set; }
        public string ClientSecret { get; set; }
        public string Token { get; set; }
        public DateTime ExpiryTime { get; set; }
    }
}