﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TravelConnect.Models.Responses;

namespace TravelConnect.Interfaces
{
    public interface IGeoService
    {
        Task<AirportAutocompleteRS> GetAirportAutocompleteAsync(string query); 
    }
}