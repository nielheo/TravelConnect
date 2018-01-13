using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;
using TravelConnect.CommonServices;
using TravelConnect.Gta.Models;

namespace TravelConnect.Gta.Services
{
    public class BaseService
    {
        protected LogService _LogService = null;
        protected string _ClientId = "36196";
        protected string _EmailAddress = "XML.INSOURCEASIA@TRAVELBULLZ.COM";
        protected string _Password = "TRAVB@1212";
        
        protected Source Source
        {
            get
            {
                return new Source
                {
                    RequestorID = new Models.Requestorid
                    {
                        Client = _ClientId,
                        EMailAddress = _EmailAddress,
                        Password = _Password
                    },
                    RequestorPreferences = new Requestorpreferences
                    {
                        RequestMode = "SYNCHRONOUS"
                    }
                };
            }
        }

        protected class Utf8StringWriter : StringWriter
        {
            public override Encoding Encoding => Encoding.UTF8;
        }

        protected string Serialize(object obj)
        {
            XmlSerializer x = new XmlSerializer(obj.GetType());
            var xml = "";

            using (var sww = new Utf8StringWriter())
            {
                using (XmlWriter writer = XmlWriter.Create(sww))
                {
                    x.Serialize(writer, obj);
                    xml = sww.ToString(); // Your XML
                }
            }

            return xml;
        }

        protected T Deserialize<T>(string xmlString)
        {
            XmlSerializer x = new XmlSerializer(typeof(T));

            T result;

            using (TextReader reader = new StringReader(xmlString))
            {
                result = (T)x.Deserialize(reader);
            }
            
            return result;
        }

        protected async Task<string> SubmitAsync(string request)
        {
            //_LogService = new LogService();

            _LogService.LogInfo($"GTA/Request - {request}");
            HttpWebRequest webRequest;

            //string token = AccessTokenManager.GetAccessToken().Token;

            byte[] data = Encoding.ASCII.GetBytes(request);

            webRequest = (HttpWebRequest)WebRequest.Create("https://rs.gta-travel.com/wbsapi/RequestListenerServlet");
            webRequest.Method = "POST";
            webRequest.ContentType = "application/xml";
            webRequest.ContentLength = data.Length;
            //webRequest.Headers[HttpRequestHeader.Authorization] = "Bearer " + token;
            webRequest.Headers[HttpRequestHeader.AcceptEncoding] = "gzip";

            using (Stream stream = webRequest.GetRequestStream())
            {
                await stream.WriteAsync(data, 0, data.Length);
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

            _LogService.LogInfo($"GTA/Response - {rs}");

            return rs;
        }
    }
}
