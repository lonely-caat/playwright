using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Zip.Api.CustomerSummary.Domain.Common.Constants;
using Zip.Api.CustomerSummary.Domain.Entities.GoogleAddress;
using Zip.Api.CustomerSummary.Infrastructure.Common.Exceptions;
using Zip.Api.CustomerSummary.Infrastructure.Configuration;
using Zip.Api.CustomerSummary.Infrastructure.Services.GoogleService.Interfaces;

namespace Zip.Api.CustomerSummary.Infrastructure.Services.GoogleService
{
    public class AddressSearchService : IAddressSearch
    {
        private readonly GoogleSettings _options;
        private readonly IHttpClientFactory _clientFactory;

        private const string GoogleMapsApiEndpoint = "https://maps.googleapis.com";
        private const string AddressLookupResource = "/maps/api/place/autocomplete/json?components=country:au|country:nz";
        private const string PlaceIdLookupResource = "/maps/api/place/details/json?fields=address_component,formatted_address,geometry,place_id,vicinity";
        private const string CountryAddressLookupResource = "/maps/api/place/autocomplete/json?components=country:";

        public AddressSearchService(IHttpClientFactory clientFactory, Microsoft.Extensions.Options.IOptions<GoogleSettings> options)
        {
            if (string.IsNullOrEmpty(options.Value.ApiKey))
            {
                throw new ApiKeyNotFoundException();
            }
            
            _clientFactory = clientFactory;
            _options = options.Value;
        }

        public async Task<GoogleAddress> FindAndVerifyAsync(string addressLine)
        {
            var addresses = await SearchAddressAsync(addressLine);
            var placeId = ParsePlaceId(addresses);

            if (string.IsNullOrEmpty(placeId))
            {
                return null;
            }

            return await FindPlaceAsync(placeId);
        }

        public async Task<IEnumerable<Prediction>> SearchAsync(string countryCode, string keyword)
        {
            var addresses = await SearchAddressWithCountryAsync(countryCode, keyword);

            return addresses.Predictions;
        }

        public async Task<GoogleAddressResponse> SearchAddressWithCountryAsync(string countryCode, string value)
        {
            using (var client = _clientFactory.CreateClient())
            using (var request = new HttpRequestMessage(HttpMethod.Get, $"{GoogleMapsApiEndpoint}{CountryAddressLookupResource}{countryCode ?? Regions.Australia}&input={value}&key={_options.ApiKey}"))
            {
                var response = await client.SendAsync(request);

                response.EnsureSuccessStatusCode();

                var responseString = await response.Content.ReadAsStringAsync();

                return JsonConvert.DeserializeObject<GoogleAddressResponse>(responseString);
            }
        }

        public async Task<GoogleAddressResponse> SearchAddressAsync(string value)
        {
            using (var client = _clientFactory.CreateClient())
            using (var request = new HttpRequestMessage(
                    HttpMethod.Get,
                    $"{GoogleMapsApiEndpoint}{AddressLookupResource}&input={value}&key={_options.ApiKey}"))
            {
                var response = await client.SendAsync(request);

                response.EnsureSuccessStatusCode();

                var responseString = await response.Content.ReadAsStringAsync();

                return JsonConvert.DeserializeObject<GoogleAddressResponse>(responseString);
            }
        }

        public async Task<GoogleAddress> FindPlaceAsync(string placeId)
        {
            using (var client = _clientFactory.CreateClient())
            using (var request = new HttpRequestMessage(
                    HttpMethod.Get,
                    $"{GoogleMapsApiEndpoint}{PlaceIdLookupResource}&placeid={placeId}&key={_options.ApiKey}"))
            {
                var response = await client.SendAsync(request);

                response.EnsureSuccessStatusCode();

                var responseString = await response.Content.ReadAsStringAsync();

                var result = JsonConvert.DeserializeObject<GooglePlaceDetailsResponse>(responseString);

                return ParseAddress(result);
            }
        }

        public string ParsePlaceId(GoogleAddressResponse result)
        {
            // check result is valid and has placeId
            if (result?.Predictions == null ||
                result.Predictions.Count == 0 ||
                result.Predictions[0].Place_Id == null)
            {
                return string.Empty;
            }

            return result.Predictions[0].Place_Id;
        }

        public GoogleAddress ParseAddress(GooglePlaceDetailsResponse placeDetails)
        {
            // check result is valid and has placeId
            if (placeDetails?.Result?.Address_Components == null ||
                placeDetails.Result.Address_Components.Count == 0)
            {
                return null;
            }

            var address = new GoogleAddress();
            
            foreach (var addressComponent in placeDetails.Result.Address_Components)
            {
                if (addressComponent.Types == null || addressComponent.Types.Count == 0)
                {
                    continue;
                }

                foreach (var type in addressComponent.Types)
                {
                    if (string.IsNullOrEmpty(type) ||
                        !Enum.TryParse(type, true, out GoogleAddressComponentType componentType))
                    {
                        continue;
                    }
                    
                    switch (componentType)
                    {
                        case GoogleAddressComponentType.Subpremise:
                            address.UnitNumber = addressComponent.Long_Name ?? string.Empty;
                            break;
                        
                        case GoogleAddressComponentType.Street_number:
                            address.StreetNumber = addressComponent.Long_Name ?? string.Empty;
                            break;
                        
                        case GoogleAddressComponentType.Route:
                            address.StreetName = addressComponent.Long_Name ?? string.Empty;
                            break;
                        
                        case GoogleAddressComponentType.Locality:
                            address.Suburb = addressComponent.Long_Name ?? string.Empty;
                            break;
                        
                        case GoogleAddressComponentType.Administrative_area_level_1:
                            address.State = addressComponent.Short_Name ?? string.Empty;
                            break;
                        
                        case GoogleAddressComponentType.Postal_code:
                            address.PostCode = addressComponent.Long_Name ?? string.Empty;
                            break;
                        
                        case GoogleAddressComponentType.Country:
                            address.Country = addressComponent.Long_Name ?? string.Empty;
                            address.CountryCode = addressComponent.Short_Name ?? string.Empty;
                            break;
                    }
                }
            }

            if (placeDetails.Result.Geometry?.Location == null ||
                placeDetails.Result.Geometry.Location.Lat == 0 ||
                placeDetails.Result.Geometry.Location.Lng == 0)
            {
                return address;
            }

            address.Latitude = placeDetails.Result.Geometry.Location.Lat;
            address.Longitude = placeDetails.Result.Geometry.Location.Lng;
            
            return address;
        }
    }
}
