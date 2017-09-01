using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using TravelConnect.Models;

namespace TravelConnect.Sabre
{
    public partial class SabreConnector
    {
        private string RequestAccessToken(string clientId, string clientSecret)
        {
            var plainTextBytes = Encoding.UTF8.GetBytes(string.Format("{0}:{1}", clientId, clientSecret));
            string base64Credential = Convert.ToBase64String(plainTextBytes);

            string request = "grant_type=client_credentials";
            byte[] data = Encoding.ASCII.GetBytes(request);

            WebRequest webRequest = WebRequest.Create(endPoint + "/v2/auth/token");
            webRequest.Method = "POST";
            webRequest.Headers[HttpRequestHeader.Authorization] = "Basic " + base64Credential;
            webRequest.ContentType = "application/x-www-form-urlencoded";
            webRequest.ContentLength = data.Length;

            using (Stream stream = webRequest.GetRequestStream())
            {
                stream.Write(data, 0, data.Length);
            }

            string sLine = "";
            using (WebResponse response = webRequest.GetResponse())
            {
                using (Stream stream = response.GetResponseStream())
                {
                    using (StreamReader sr = new StreamReader(stream))
                    {
                        sLine = sr.ReadToEnd();
                    }
                }
            }

            return sLine.ToString();
        }

        private async Task<SabreCredential> GetSabreCredential()
        {
            var sabreCredential = await _context.SabreCredentials
               .SingleOrDefaultAsync(s => s.IsActive);

            return sabreCredential;
        }

        private async Task<AccessToken> GetAccessToken()
        {
            AccessToken accessToken = null;

            SabreCredential sabreCredential = await GetSabreCredential();

            string json = RequestAccessToken(
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

        private async Task<string> AccessToken()
        {
            //AccessToken accessToken = null;
            if (_AccessToken == null)
            {
                _AccessToken = await GetAccessToken();

            }
            else
            {
                if (_AccessToken.ExpiryTime <= DateTime.Now)
                    _AccessToken = await GetAccessToken();
            }

            if (_AccessToken == null)
            {
                throw new ApplicationException("Cannot Get Access Token");
            }

            return _AccessToken.Token;
        }
    }
}
