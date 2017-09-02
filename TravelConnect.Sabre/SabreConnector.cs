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
    public interface ISabreConnector
    {
        Task<string> SendRequestAsync(string url, string request, bool isPost);
    }

    public partial class SabreConnector : ISabreConnector
    {
        private string endPoint = "https://api.test.sabre.com";
        private static AccessToken _AccessToken = null;

        private readonly TCContext _context;

        public SabreConnector(TCContext _context)
        {
            this._context = _context;
        }
        
        public async Task<string> SendRequestAsync(string url, string request, bool isPost)
        {
            HttpWebRequest webRequest;
            string token = await AccessTokenAsync();

            //string token = AccessTokenManager.GetAccessToken().Token;
            if (isPost)
            {
                byte[] data = Encoding.ASCII.GetBytes(request);

                webRequest = (HttpWebRequest) WebRequest.Create(endPoint + url);
                webRequest.Method = "POST";
                webRequest.ContentType = "application/x-www-form-urlencoded";
                webRequest.ContentLength = data.Length;
                webRequest.Headers[HttpRequestHeader.Authorization] = "Bearer " + token;

                using (Stream stream = webRequest.GetRequestStream())
                {
                    await stream.WriteAsync(data, 0, data.Length);
                }
            }
            else
            {
                webRequest = (HttpWebRequest) WebRequest.Create(endPoint + url + "?" + request);
                webRequest.Headers[HttpRequestHeader.Authorization] = "Bearer " + token;
            }

            MemoryStream content = new MemoryStream();
            using (WebResponse response = await webRequest.GetResponseAsync())
            {
                using (Stream stream = response.GetResponseStream())
                {
                    await stream.CopyToAsync(content);
                }
            }

            return Encoding.UTF8.GetString(content.ToArray());
        }

        
    }
}
