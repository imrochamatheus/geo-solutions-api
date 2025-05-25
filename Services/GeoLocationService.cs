using GeoSolucoesAPI.DTOs;
using GeoSolucoesAPI.DTOs.BudgetResponse;
using GeoSolucoesAPI.Models;
using Google.Api.Gax.Grpc;
using Google.Apis.Auth.OAuth2;
using Google.Maps.Routing.V2;
using Google.Type;
using System.Net.Http;
using System.Text.Json;

namespace GeoSolucoesAPI.Services
{
    public class GeoLocationService : IGeoLocationService
    {
        private readonly HttpClient _httpClient;

        public GeoLocationService()
        {
            _httpClient = new HttpClient();
        }


        public async Task<decimal> GetDistanceFromStartEndPoint(StartPointDbo origin, DestinyDto destiny)
        {
            try
            {
                string originString = $"{origin.Street} {origin.Number} {origin.Neighborhood} {origin.City} {origin.State} {origin.Country}";
                string destinyString = $"{destiny.Street} {destiny.Number} {destiny.Neighborhood} {destiny.City} {destiny.State} {destiny.Country}";

                var originGeoLocation = await GetGeoLocation(Uri.EscapeDataString(originString));
                var destinyGeoLocation = await GetGeoLocation(Uri.EscapeDataString(destinyString));


                string jsonCredentials = @"{
                  ""type"": ""service_account"",
                  ""project_id"": ""glass-arcanum-456502-d5"",
                  ""private_key_id"": ""63d4835b825f8f0f1482998aebb06fa21ff02934"",
                  ""private_key"": ""-----BEGIN PRIVATE KEY-----\nMIIEvgIBADANBgkqhkiG9w0BAQEFAASCBKgwggSkAgEAAoIBAQDBGf8bjXPAKuUK\nTIC8WsaZ/vTmtJivme12gtvg5qZVSfSaTyQVnyFheI1EQEeRK5rHj00msj8SAWh9\nUKOLJBZvwaRf0oR7rTz9GyiG6M8tjoyvszkEO6KljtJ30OgZhUiQlPNM1H5uZUrM\nTOu7HXoaODq6AqD2RDWfkmS3q358bgVW8uWe5/AOkJWLHQvyo0JVOnStTxfVfUjK\nlugYom7HhaP8jfgOnKdM/WPDTrl7SA0WPMBBbpjBBt+vAouGPYZTE90oYE7s2Owv\nB/Zkfh1nFWxGzc7UGYVKgkX3Bsq4FgC3nD7ss20eP8LzR0iRXxPCXk5BZ9z/KcWc\nhrNm1HyFAgMBAAECggEAJLwg02mD+S1P2Dc4kFyGhppBlbgKifFzAsYfNTg7sWL5\n1Pc0Sqa0r3nDChZeZwgQEGG7EB79r5uz+I8EJb6uRAFnF1GRoYOR5Car/YpBi42g\nRvaF5L/Bj9RNRrznwp8f8F1dsaKFENRVN+t0NusDJQTIa0V/wIm2V85+OZC25c2l\nknn1y/DLj6r7UWntWVcJ/TGtXeToNhu9dHP8zPWBJIKyyu4yhgyOyckndCJORRH1\n3qlu9/97VCaP6yQcVwsK1SSa8ZAtjlDVjhwI3oDGK5GnmPBXjYzB9rP6zCrzXV0u\nM76ZczcSdcQXxqJUHDbYaE3tHEgULqF2knl+y559IQKBgQDtF9Z2VqtI2SzKZGfL\nqlxu309lh2rGMUDj6Yxp6Ytp0BpXQb9M3oxcK4cGQKMRS+CqKvfCZSl+t3ZO8EUu\n3rYqyEkkwV+yVhIaTcYbJQvCoZaKwOi2HZfGVps2IsF0UaeIbqIylV7+ycoqqFQq\n0rlNuVxmLSnWQ1aam+u7hEx0lQKBgQDQgBabnaTHJiSU8fkmTVcnUG5b0XaWGXuI\nbbyn6Ky3SdKol+vY+38a8StFDzydSNRo3wZ+34PleBvCg4qVUCleWfb6bywUEqgl\nRwTU5vnEyd9yMGfIDLr7gaqPaQz5AgB1/KM/5N2xbRPqrabJ4A5owlsArx8qVZNt\nnSvI5FJ8MQKBgQCJU+1czgaYQ3K2KEIWra6saZxLaoxmD9FOMdCq6CNgPrGDKQnt\nNFqIaI3lCTtqoVCYQBsR9hZ61zknr/Pimg1Z6nizWW5clY4WvkWpI2QUcBQKeoJQ\nOgPXVgLA8JCtFz66v5ojQnGrkqYO30EhLL04T809QZuiB476LqX0D0VZoQKBgQCq\n3GyqddN0x9Muo+Sy+Ko1M3pckBiIBLXxJUx2pPv07/BL7MN/ewm8QGpfG49mrSo0\nDqA6FFz4DSdijX1hwuFDyqHUtIoAoRFeCwbwmZ+RHoWYBAboL1aDiM0G3OWeoX9T\nMQdu8tSh82tg45FM2em0+6CrvNbjUGGScDxTUpgt4QKBgAMn0ywe56ocL0v/G5Fo\njeoMOkniU+yrV8WBQGlJKlFtsaiI6h2W+7HDGhuNdDJSXm2qhIfWE68q/kzJlqR6\ngJeurFy87MP4RY4Dc5WyPl0xdnWYt4E0HAOxNJGhOstUKBWfH0aZ7A4mvxj+cF25\np9i3gpYqSC2Qdxgfnwg/MCYx\n-----END PRIVATE KEY-----\n"",
                  ""client_email"": ""conta-teste@glass-arcanum-456502-d5.iam.gserviceaccount.com"",
                  ""client_id"": ""116991096208423079053"",
                  ""auth_uri"": ""https://accounts.google.com/o/oauth2/auth"",
                  ""token_uri"": ""https://oauth2.googleapis.com/token"",
                  ""auth_provider_x509_cert_url"": ""https://www.googleapis.com/oauth2/v1/certs"",
                  ""client_x509_cert_url"": ""https://www.googleapis.com/robot/v1/metadata/x509/conta-teste%40glass-arcanum-456502-d5.iam.gserviceaccount.com"",
                  ""universe_domain"": ""googleapis.com""
                }";

                var credential = GoogleCredential.FromJson(jsonCredentials);
                //var credential = await GoogleCredential.GetApplicationDefaultAsync();

                // Cria o client usando o builder com a credencial
                var builder = new RoutesClientBuilder
                {
                    Credential = credential
                };

                var client = builder.Build();

                // Configura o header para retornar todos os campos
                CallSettings callSettings = CallSettings.FromHeader("X-Goog-FieldMask", "*");

                // Define a requisição
                ComputeRoutesRequest request = new ComputeRoutesRequest
                {
                    Origin = new Waypoint
                    {
                        Location = new Location
                        {
                            LatLng = new LatLng
                            {
                                Latitude = originGeoLocation.lati,
                                Longitude = originGeoLocation.longi
                            }
                        }
                    },
                    Destination = new Waypoint
                    {
                        Location = new Location
                        {
                            LatLng = new LatLng
                            {
                                Latitude = destinyGeoLocation.lati,
                                Longitude = destinyGeoLocation.longi
                            }
                        }
                    },
                    TravelMode = RouteTravelMode.Drive,
                    RoutingPreference = RoutingPreference.TrafficAware
                };

                ComputeRoutesResponse response = client.ComputeRoutes(request, callSettings);


                return response.Routes.First().DistanceMeters;

            }
            catch
            {
                throw;
            }


        }

        private async Task<(double lati, double longi)> GetGeoLocation(string end)
        {
            try
            {

                string url = $"https://maps.googleapis.com/maps/api/geocode/json?address={end}&key={"AIzaSyCNPIJ4r9_wIh7xZhX1qY9jsaXippYj8gA"}";
                HttpResponseMessage response = await _httpClient.GetAsync(url);

                // Garantir que a requisição foi bem-sucedida
                response.EnsureSuccessStatusCode();

                // Lendo o conteúdo da resposta como string
                string content = await response.Content.ReadAsStringAsync();

                using var json = JsonDocument.Parse(content);
                var results = json.RootElement.GetProperty("results");

                if (results.GetArrayLength() > 0)
                {
                    var location = results[0]
                        .GetProperty("geometry")
                        .GetProperty("location");

                    double lat = location.GetProperty("lat").GetDouble();
                    double lng = location.GetProperty("lng").GetDouble();

                    return (lat, lng);
                }

                return default;
            }
            catch (Exception)
            {

                throw;
            }

        }

        public async Task<AddressResponse> GetAddressByCep(string cep)
        {
            if (string.IsNullOrWhiteSpace(cep))
                throw new ArgumentException("CEP não pode estar vazio.");

            // Remove traços e espaços
            var cleanedCep = cep.Replace("-", "").Trim();

            if (cleanedCep.Length != 8 || !cleanedCep.All(char.IsDigit))
                throw new ArgumentException("CEP inválido.");

            var url = $"https://maps.googleapis.com/maps/api/geocode/json?address={cleanedCep}&key={"AIzaSyCNPIJ4r9_wIh7xZhX1qY9jsaXippYj8gA"}";
            var response = await _httpClient.GetAsync(url);
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            using var json = JsonDocument.Parse(content);

            var results = json.RootElement.GetProperty("results");

            if (results.GetArrayLength() == 0)
                return null;

            var result = results[0];

            var addressComponents = result.GetProperty("address_components");
            string GetComponent(string type)
            {
                foreach (var component in addressComponents.EnumerateArray())
                {
                    var types = component.GetProperty("types").EnumerateArray().Select(t => t.GetString());
                    if (types.Contains(type))
                        return component.GetProperty("long_name").GetString();
                }
                return null;
            }

            var geometry = result.GetProperty("geometry").GetProperty("location");
            var lat = geometry.GetProperty("lat").GetDouble();
            var lng = geometry.GetProperty("lng").GetDouble();

            return new AddressResponse()
            {
                Zipcode = cleanedCep,
                Street = GetComponent("route"),
                Number = int.TryParse(GetComponent("street_number"), out var num) ? num : (int?)null,
                Neighborhood = GetComponent("sublocality") ?? GetComponent("political"),
                City = GetComponent("administrative_area_level_2"),
                State = GetComponent("administrative_area_level_1"),
                Complement = GetComponent("subpremise")
            };

        }
    }
}
