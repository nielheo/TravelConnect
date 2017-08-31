using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

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
    
    public static class AccessTokenManager
    {
        private static AccessToken _AccessToken = null;

        private static AccessToken GenerateAccessToken()
        {
            AccessToken accessToken = null;

            string json = SabreConnector.GetAccessToken("VjE6OW5vY3U5dzJ1c3QybzZ4ZzpERVZDRU5URVI6RVhU"
                        , "ZkFBcDZsNkk=");
            var token = JsonConvert.DeserializeObject<dynamic>(json);

            if (token?.error == null)
            {
                accessToken = new AccessToken
                {
                    Token = token.access_token,
                    ExpiryTime = DateTime.Now.AddSeconds((int)token.expires_in).AddMinutes(-15)
                };

            };

            return accessToken;
        }

        public static string GetAccessToken()
        {
            if (_AccessToken == null)
                _AccessToken = GenerateAccessToken();
            else
            {
                if (_AccessToken.ExpiryTime <= DateTime.Now)
                    _AccessToken = GenerateAccessToken();
            }

            if (_AccessToken == null)
            {
                throw new ApplicationException("Cannot Get Access Token");
            }

            return _AccessToken.Token;
        }
    }
}
