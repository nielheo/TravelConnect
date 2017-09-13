using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using TravelConnect.Models;

namespace TravelConnect.Sabre
{
    public partial class SabreConnector
    {
        private async Task<string> RequestAccessTokenAsync(string clientId, string clientSecret)
        {
            var plainTextBytes = Encoding.UTF8.GetBytes(string.Format("{0}:{1}", clientId, clientSecret));
            string base64Credential = Convert.ToBase64String(plainTextBytes);

            string request = "grant_type=client_credentials";
            byte[] data = Encoding.ASCII.GetBytes(request);

            var webRequest = (HttpWebRequest)WebRequest.Create(endPoint + "/v2/auth/token");
            webRequest.Method = "POST";
            webRequest.Headers[HttpRequestHeader.Authorization] = "Basic " + base64Credential;
            webRequest.ContentType = "application/x-www-form-urlencoded";
            webRequest.ContentLength = data.Length;

            using (Stream stream = webRequest.GetRequestStream())
            {
                await stream.WriteAsync(data, 0, data.Length);
            }

            var content = new MemoryStream();
            using (WebResponse response = await webRequest.GetResponseAsync())
            {
                using (Stream stream = response.GetResponseStream())
                {
                    await stream.CopyToAsync(content);
                }
            }

            return Encoding.UTF8.GetString(content.ToArray());
        }

        private async Task<SabreCredential> GetSabreCredentialAsync()
        {
            //var sabreCredential = await _context.SabreCredentials
            //   .SingleOrDefaultAsync(s => s.IsActive);

            return new SabreCredential {
                ClientId = "VjE6OW5vY3U5dzJ1c3QybzZ4ZzpERVZDRU5URVI6RVhU",
                ClientSecret = "ZkFBcDZsNkk=",
            };
        }

        private async Task<AccessToken> GetAccessTokenAsync()
        {
            AccessToken accessToken = null;

            SabreCredential sabreCredential = await GetSabreCredentialAsync();

            string json = await RequestAccessTokenAsync(
                sabreCredential.ClientId,
                sabreCredential.ClientSecret);

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

        private async Task<string> AccessTokenAsync()
        {
            //AccessToken accessToken = null;
            if (_AccessToken == null)
            {
                _AccessToken = await GetAccessTokenAsync();
            }
            else
            {
                if (_AccessToken.ExpiryTime <= DateTime.Now)
                    _AccessToken = await GetAccessTokenAsync();
            }

            if (_AccessToken == null)
            {
                throw new ApplicationException("Cannot Get Access Token");
            }

            return _AccessToken.Token;
        }
    }
}