using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;

namespace TravelConnect.Sabre
{
    public class SabreConnector
    {
        private static string endPoint = "https://api.test.sabre.com";

        public static string GetAccessToken(string clientId, string clientSecret)
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


        public static string SendRequest(string url, string request, bool isPost)
        {
            WebRequest webRequest;
            string token = AccessTokenManager.GetAccessToken();
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
