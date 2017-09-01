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
        Task<string> SendRequest(string url, string request, bool isPost);
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
        
        public async Task<string> SendRequest(string url, string request, bool isPost)
        {
            WebRequest webRequest;
            string token = await AccessToken();

            //string token = AccessTokenManager.GetAccessToken().Token;
            if (isPost)
            {
                byte[] data = Encoding.ASCII.GetBytes(request);

                webRequest = WebRequest.Create(endPoint + url);
                webRequest.Method = "POST";
                webRequest.ContentType = "application/x-www-form-urlencoded";
                webRequest.ContentLength = data.Length;
                webRequest.Headers[HttpRequestHeader.Authorization] = "Bearer " + token;

                using (Stream stream = webRequest.GetRequestStream())
                {
                    stream.Write(data, 0, data.Length);
                }
            }
            else
            {
                webRequest = WebRequest.Create(endPoint + url + "?" + request);
                webRequest.Headers[HttpRequestHeader.Authorization] = "Bearer " + token;
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

        
    }
}
