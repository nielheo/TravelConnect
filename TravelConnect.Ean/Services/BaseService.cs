using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using TravelConnect.CommonServices;

namespace TravelConnect.Ean.Services
{
    public class BaseService
    {
        protected LogService _LogService = null;
        protected string _ApiKey = "2wt5kd9pdbvbycdrrk3y9yzp";
        protected string _SharedSecret = "RrwEwN7j";
        protected string _Cid = "454244";
        protected string _MinorRev = "30";

        //protected string _ApiKey = "sb4ps442bpwzv4fc6m9gd7rb";
        //protected string _SharedSecret = "eETy7g9Y";
        //protected string _Cid = "461518";
        //protected string _MinorRev = "30";


        //https://api.eancdn.com/ean-services/rs/hotel/v3/list?minorRev=30&cid=461518&apiExperience=PARTNER_AFFILIATE&apiKey=sb4ps442bpwzv4fc6m9gd7rb&sig=3d0876455851f6941e809bf018e0a703&customerIpAddress=119.81.47.27&customerUserAgent=web&customerSessionId=54321&minorRev=30&locale=en_US&currencyCode=USD&city=Bali&countryCode=ID&arrivalDate=09/04/2018&departureDate=09/05/2018&room1=2&_type=json

        protected string _BaseUrl(RequestType requestType)
        {
            switch (requestType)
            {
                case RequestType.HotelList:
                    return "https://api.eancdn.com/ean-services/rs/hotel/v3/list";
                case RequestType.RoomAvailability:
                    return "https://api.eancdn.com/ean-services/rs/hotel/v3/avail";
                default:
                    return "";
            } 
        }

        protected bool _IsPost(RequestType requestType)
        {
            switch (requestType)
            {
                case RequestType.HotelList:
                case RequestType.RoomAvailability:
                    return false;
                default:
                    return false;
            }
        }

        protected enum RequestType
        {
            HotelList = 0,
            RoomAvailability = 1,
        }

        protected async Task<string> SubmitAsync(string request, RequestType requestType)
        {
            
            string fullRequest = GenerateFullRequest(request, requestType);
            _LogService.LogInfo($"EAN/Request - {fullRequest}");
            HttpWebRequest webRequest;
            
            //string token = AccessTokenManager.GetAccessToken().Token;
            if (_IsPost(requestType))
            {
                byte[] data = Encoding.ASCII.GetBytes(request);

                webRequest = (HttpWebRequest)WebRequest.Create(fullRequest);
                webRequest.Method = "POST";
                webRequest.ContentType = "application/json";
                webRequest.ContentLength = data.Length;
                //webRequest.Headers[HttpRequestHeader.Authorization] = "Bearer " + token;
                webRequest.Headers[HttpRequestHeader.AcceptEncoding] = "gzip";

                using (Stream stream = webRequest.GetRequestStream())
                {
                    await stream.WriteAsync(data, 0, data.Length);
                }
            }
            else
            {
                webRequest = (HttpWebRequest)WebRequest.Create(fullRequest);
                //webRequest.Headers[HttpRequestHeader.Authorization] = "Bearer " + token;
                webRequest.Headers[HttpRequestHeader.AcceptEncoding] = "gzip";
            }

            MemoryStream content = new MemoryStream();
            using (HttpWebResponse response = (HttpWebResponse)await webRequest.GetResponseAsync())
            {
                switch (response.ContentEncoding?.ToLower())
                {
                    case "gzip":
                    case "deflate":
                        using (GZipStream stream = new GZipStream(response.GetResponseStream(), CompressionMode.Decompress))
                        {
                            await stream.CopyToAsync(content);
                        }
                        break;

                    default:
                        using (Stream stream = response.GetResponseStream())
                        {
                            await stream.CopyToAsync(content);
                        }
                        break;
                }
            }

            var rs = Encoding.UTF8.GetString(content.ToArray());

            _LogService.LogInfo($"EAN/Response - {rs}");

            return rs;
        }

        private string GenerateFullRequest(string request, RequestType requestType)
        {
            return ($"{_BaseUrl(requestType)}?{request}&minorRev={_MinorRev}&cid={_Cid}&apiKey={_ApiKey}&sig={GenerateSignature()}&_type=json");
        }

        protected string GenerateSignature()
        {
            Int32 timeStamp = (Int32)(DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).TotalSeconds;
            return CreateMD5($"{_ApiKey}{_SharedSecret}{timeStamp.ToString()}");
        }

        protected string CreateMD5(string input)
        {
            // Use input string to calculate MD5 hash
            using (System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create())
            {
                byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(input);
                byte[] hashBytes = md5.ComputeHash(inputBytes);

                // Convert the byte array to hexadecimal string
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < hashBytes.Length; i++)
                {
                    sb.Append(hashBytes[i].ToString("X2"));
                }
                return sb.ToString().ToLower();
            }
        }
    }
}
