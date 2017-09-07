﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TravelConnect.Models.Requests;

namespace TravelConnect.Services.Models
{
    public class SegmentRQ
    {
        public DateTime Departure { get; set; }
        public string Origin { get; set; }
        public string Destination { get; set; }
    }

    public class SearchRQ
    {
        public List<SegmentRQ> Segments { get; set; }
        public bool AvailableFlightsOnly { get; set; }
        public AirLowFareSearchRQ AirLowFareSearchRQ
        {
            get
            {
                AirLowFareSearchRQ rq = new AirLowFareSearchRQ();
                int segmentIndex = 1;
                rq.OTA_AirLowFareSearchRQ = new OTA_Airlowfaresearchrq
                {
                    AvailableFlightsOnly = this.AvailableFlightsOnly,

                    Target = "Production",
                    POS = new POS
                    {
                        Source = new Source[]
                        {
                            new Source
                            {
                                PseudoCityCode = "F9CE",
                                RequestorID = new Requestorid
                                {
                                    Type = "1",
                                    ID = "1",
                                    CompanyName = new Companyname()
                                }
                            }
                        }
                    },
                    OriginDestinationInformation = this.Segments.Select(s =>
                        new Origindestinationinformation
                        {
                            RPH = (segmentIndex++).ToString(),
                            DepartureDateTime = s.Departure,
                            OriginLocation = new Originlocation
                            {
                                LocationCode = s.Origin
                            },
                            DestinationLocation = new Destinationlocation
                            {
                                LocationCode = s.Destination
                            }
                        }).ToArray(),
                    TravelerInfoSummary = new Travelerinfosummary
                    {
                        SeatsRequested = new int[] { 1 },
                        AirTravelerAvail = new Airtraveleravail[]
                        {
                            new Airtraveleravail
                            {
                                PassengerTypeQuantity = new Passengertypequantity[]
                                {
                                    new Passengertypequantity
                                    {
                                        Code = "ADT",
                                        Quantity = 1
                                    }

                                }
                            }
                        }
                    },
                    TravelPreferences = new Travelpreferences
                    {
                        CabinPref = new Cabinpref[]
                        {
                            new Cabinpref
                            {
                                Cabin = "Y",
                                PreferLevel = "Preferred"
                            }
                        }, 
                    },
                    TPA_Extensions = new TPA_Extensions2
                    {
                        IntelliSellTransaction = new Intelliselltransaction
                        {
                            RequestType = new Requesttype
                            {
                                Name = "50ITINS"
                            }
                        }
                    }
                };

                return rq;
            }
        }
    }
}
