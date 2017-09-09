using System.IO;
using System.IO.Compression;
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

                webRequest = (HttpWebRequest)WebRequest.Create(endPoint + url);
                webRequest.Method = "POST";
                webRequest.ContentType = "application/json";
                webRequest.ContentLength = data.Length;
                webRequest.Headers[HttpRequestHeader.Authorization] = "Bearer " + token;
                webRequest.Headers[HttpRequestHeader.AcceptEncoding] = "gzip";

                using (Stream stream = webRequest.GetRequestStream())
                {
                    await stream.WriteAsync(data, 0, data.Length);
                }
            }
            else
            {
                webRequest = (HttpWebRequest)WebRequest.Create(endPoint + url + "?" + request);
                webRequest.Headers[HttpRequestHeader.Authorization] = "Bearer " + token;
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

            return Encoding.UTF8.GetString(content.ToArray());
        }
    }
}