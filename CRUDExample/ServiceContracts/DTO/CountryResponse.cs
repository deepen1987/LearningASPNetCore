﻿using Entities;

namespace ServiceContracts.DTO
{
    /// <summary>
    /// DTO class tht is used as return type for most of CountryService methods
    /// </summary>
    public class CountryResponse
    {
        public Guid CountryId { get; set; }
        public string? CountryName { get; set; }
    }

    public static class CountryExtensions
    {
        public static CountryResponse ToCountryResponse(this Country country)
        {
            return new CountryResponse { CountryId = country.Id, CountryName = country.CountryName };
        }
    }
}
