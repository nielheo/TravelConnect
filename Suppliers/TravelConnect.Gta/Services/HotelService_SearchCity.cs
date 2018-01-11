using TravelConnect.Gta.Models;
using System;
using System.Xml;
using System.Xml.Serialization;
using System.IO;
using System.Threading.Tasks;
using System.Text;

namespace TravelConnect.Gta.Services
{
    public class Utf8StringWriter : StringWriter
    {
        public override Encoding Encoding => Encoding.UTF8;
    }

    public partial class HotelService : BaseService
    {
        public string SearchHotelPriceRequest()
        {
            Request request = new Request
            {

                Source = new Source
                {
                    RequestorID = new Requestorid
                    {
                        Client = _ClientId,
                        EMailAddress = _EmailAddress,
                        Password = _Password
                    },
                    RequestorPreferences = new Requestorpreferences
                    {
                        RequestMode = "SYNCHRONOUS"
                    }
                },
                RequestDetails = new Requestdetails
                {
                    SearchHotelPriceRequest = new Searchhotelpricerequest
                    {
                        ItemDestination = new Itemdestination
                        {
                            DestinationType = "city",
                            DestinationCode = "BKK"
                        },
                        ImmediateConfirmationOnly = "",
                        PeriodOfStay = new Periodofstay
                        {
                            CheckInDate = "2018-07-01",
                            Duration = "2"
                        },
                        Rooms = new Room[]
                        {
                            new Room
                            {
                                Code = "SB"
                            }
                        },
                        OrderBy = "pricelowtohigh",
                        NumberOfReturnedItems = "10"
                    },
                }
            };

            XmlSerializer x = new XmlSerializer(request.GetType());
            var xml = "";

            using (var sww = new Utf8StringWriter())
            {
                using (XmlWriter writer = XmlWriter.Create(sww))
                {
                    x.Serialize(writer, request);
                    xml = sww.ToString(); // Your XML
                }
            }

            return SubmitAsync(xml, RequestType.HotelList).Result;
        }
    }
}
