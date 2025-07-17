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


                //string jsonCredentials = @"{
                //  ""type"": ""service_account"",
                //  ""project_id"": ""glass-arcanum-456502-d5"",
                //  ""private_key_id"": ""63d4835b825f8f0f1482998aebb06fa21ff02934"",
                //  ""private_key"": ""-----BEGIN PRIVATE KEY-----\nMIIEvgIBADANBgkqhkiG9w0BAQEFAASCBKgwggSkAgEAAoIBAQDBGf8bjXPAKuUK\nTIC8WsaZ/vTmtJivme12gtvg5qZVSfSaTyQVnyFheI1EQEeRK5rHj00msj8SAWh9\nUKOLJBZvwaRf0oR7rTz9GyiG6M8tjoyvszkEO6KljtJ30OgZhUiQlPNM1H5uZUrM\nTOu7HXoaODq6AqD2RDWfkmS3q358bgVW8uWe5/AOkJWLHQvyo0JVOnStTxfVfUjK\nlugYom7HhaP8jfgOnKdM/WPDTrl7SA0WPMBBbpjBBt+vAouGPYZTE90oYE7s2Owv\nB/Zkfh1nFWxGzc7UGYVKgkX3Bsq4FgC3nD7ss20eP8LzR0iRXxPCXk5BZ9z/KcWc\nhrNm1HyFAgMBAAECggEAJLwg02mD+S1P2Dc4kFyGhppBlbgKifFzAsYfNTg7sWL5\n1Pc0Sqa0r3nDChZeZwgQEGG7EB79r5uz+I8EJb6uRAFnF1GRoYOR5Car/YpBi42g\nRvaF5L/Bj9RNRrznwp8f8F1dsaKFENRVN+t0NusDJQTIa0V/wIm2V85+OZC25c2l\nknn1y/DLj6r7UWntWVcJ/TGtXeToNhu9dHP8zPWBJIKyyu4yhgyOyckndCJORRH1\n3qlu9/97VCaP6yQcVwsK1SSa8ZAtjlDVjhwI3oDGK5GnmPBXjYzB9rP6zCrzXV0u\nM76ZczcSdcQXxqJUHDbYaE3tHEgULqF2knl+y559IQKBgQDtF9Z2VqtI2SzKZGfL\nqlxu309lh2rGMUDj6Yxp6Ytp0BpXQb9M3oxcK4cGQKMRS+CqKvfCZSl+t3ZO8EUu\n3rYqyEkkwV+yVhIaTcYbJQvCoZaKwOi2HZfGVps2IsF0UaeIbqIylV7+ycoqqFQq\n0rlNuVxmLSnWQ1aam+u7hEx0lQKBgQDQgBabnaTHJiSU8fkmTVcnUG5b0XaWGXuI\nbbyn6Ky3SdKol+vY+38a8StFDzydSNRo3wZ+34PleBvCg4qVUCleWfb6bywUEqgl\nRwTU5vnEyd9yMGfIDLr7gaqPaQz5AgB1/KM/5N2xbRPqrabJ4A5owlsArx8qVZNt\nnSvI5FJ8MQKBgQCJU+1czgaYQ3K2KEIWra6saZxLaoxmD9FOMdCq6CNgPrGDKQnt\nNFqIaI3lCTtqoVCYQBsR9hZ61zknr/Pimg1Z6nizWW5clY4WvkWpI2QUcBQKeoJQ\nOgPXVgLA8JCtFz66v5ojQnGrkqYO30EhLL04T809QZuiB476LqX0D0VZoQKBgQCq\n3GyqddN0x9Muo+Sy+Ko1M3pckBiIBLXxJUx2pPv07/BL7MN/ewm8QGpfG49mrSo0\nDqA6FFz4DSdijX1hwuFDyqHUtIoAoRFeCwbwmZ+RHoWYBAboL1aDiM0G3OWeoX9T\nMQdu8tSh82tg45FM2em0+6CrvNbjUGGScDxTUpgt4QKBgAMn0ywe56ocL0v/G5Fo\njeoMOkniU+yrV8WBQGlJKlFtsaiI6h2W+7HDGhuNdDJSXm2qhIfWE68q/kzJlqR6\ngJeurFy87MP4RY4Dc5WyPl0xdnWYt4E0HAOxNJGhOstUKBWfH0aZ7A4mvxj+cF25\np9i3gpYqSC2Qdxgfnwg/MCYx\n-----END PRIVATE KEY-----\n"",
                //  ""client_email"": ""conta-teste@glass-arcanum-456502-d5.iam.gserviceaccount.com"",
                //  ""client_id"": ""116991096208423079053"",
                //  ""auth_uri"": ""https://accounts.google.com/o/oauth2/auth"",
                //  ""token_uri"": ""https://oauth2.googleapis.com/token"",
                //  ""auth_provider_x509_cert_url"": ""https://www.googleapis.com/oauth2/v1/certs"",
                //  ""client_x509_cert_url"": ""https://www.googleapis.com/robot/v1/metadata/x509/conta-teste%40glass-arcanum-456502-d5.iam.gserviceaccount.com"",
                //  ""universe_domain"": ""googleapis.com""
                //}";

                string jsonCredentials = @"{
                          ""type"": ""service_account"",
                          ""project_id"": ""geosolutionsroutes"",
                          ""private_key_id"": ""89c9f614c48898216f5c58bbcf3efc0bb26c265b"",
                          ""private_key"": ""-----BEGIN PRIVATE KEY-----\nMIIEvQIBADANBgkqhkiG9w0BAQEFAASCBKcwggSjAgEAAoIBAQCPQnuy+IGQ/Vms\n8M0axqqfcZjvBJvQV7xlVmEl6zWFlwHYJalTSBtKpKWYV8NTR/mufpARLE32u4Bb\nNPC/+3pWAUpidTdn8YFi2MBAP86AhCVyaHaw9hi05FYKdub8RtNhWTrv2VnIxdQF\n6VVYePTiOOuYSZHsCoIawYOeT+bD2h6c55RvCpiZVUB440q1GKTYzBiOIWkxpw+R\nV/EjPRHCeBkSQozIpBUlU1tuHN0KCC8CtmLjNvj/1OPf/z8ISi4brBzUY8BDQyz5\ntlyWFzSwS+bvovyK3qZlOPLA9/Tjx0Hk3zMM/q8roOkbmobPtmng9TRids5fFxE2\n8PeXly6JAgMBAAECggEAFsWhJxpEcjkrc4eVes+gmJa0ki3f3LbNjig0ahQg3lra\nIrMd3syj9fIFFpnAxTC4OioJGYm63OECLr1UFZhSYnBzFAKA66dig0VkDfJ2j0wy\nLaT4VPnRhJOVL7Wl0u0pNZy69xxMHtN/d+8Wr2kOuBUHMh5jsSscckdXdidKTdBV\nwpCbYNqtuRzInc/XRhHJdSjekki6duLhRhn/l+zOmVZsJp/9iGvD6PkHk1TKVVIZ\nscgCeiJLDmnV1lAkbCuM3r7SoZ1BsoYvGCwf/MrueVNLm8xh6KBgRIeDVOT1zmSz\nsOG90sWIB8xmnXDQBf0lpeWFwnrRphyTFzkBiQPMXwKBgQDIVAqq47JHWY5yzlfU\nmnz17+aRpLOE8fhTJ+/ICdu3zqGzwIhMBEFo2RwESVc4eaQEoF3Xxl9RTlp4oVN5\nCF3QZAqoBKdVltDsWzd6YVfhz2NMGN/e4Cr77o5cpansdZh53fLVAOOeT+zkvGE0\n3BJs6089H74G06w+uq8kahbKPwKBgQC3Emj/ekO/PEACWb8TtjJ4BmPCFRPiEjZO\nl0Fdmnt/YAx4oh43XqdmApgGl+eZoDf03WU0eS9a6Bi+Rt3vf2CQO/grNrqBKgwO\n8j0SB3WbfQ7FGpx+IcfU2yer7kDhgjwlxmGa5GwXX+BWplSlt5oeR+fbd6PZ/TbR\njnn1PKGFNwKBgQChVW2x6uatJ6bdNujtQ+3Xc3lRWj9pliXTcppdk/LruPHpfelG\nrzauZgt2OEqSJQS+5RguqoghRuT9uY8sLly/c5JRdk39VBS2BVBMsxC/vvHNW0sQ\nh6CWfK0SWCjdUmeV3fWvLaQi32N9Y2k11PbR2UBbjbDQdrCFZHug6/+mqQKBgD9l\ngbTO+zzzOVtXBuNlmc6Ur+Ea+Xq1Qdcva/RlNdUjRs5TndEg1yltRoYp/orlv9rs\n/EoSmUKK0165Hcv9OUxZaBRW7HkkPW7DlkBzEtgYk4/QKldHjXL5vaA9bnxpZDjU\nnSJNbd0eV4rKKNAGZ7mKvxDyOmyX0m75PuSc7XY7AoGACwDw1wk98j+Ntha2gx8N\nRx1E1zpj20PJQFF4oRVojZaKnpv/j3VC5EKpApl+/BUvjIxXGurkIbIDjAmtGSpA\nWdpi/Uvcrk4KJ2og+UuYLVD/nitZxskqMBNDVIzDkKysGSEoHDnI4gRnifzQphYq\nuSCh5zE66iFIR5RPfxurjBo=\n-----END PRIVATE KEY-----\n"",
                          ""client_email"": ""geosolutionsservice@geosolutionsroutes.iam.gserviceaccount.com"",
                          ""client_id"": ""117186778766407727801"",
                          ""auth_uri"": ""https://accounts.google.com/o/oauth2/auth"",
                          ""token_uri"": ""https://oauth2.googleapis.com/token"",
                          ""auth_provider_x509_cert_url"": ""https://www.googleapis.com/oauth2/v1/certs"",
                          ""client_x509_cert_url"": ""https://www.googleapis.com/robot/v1/metadata/x509/geosolutionsservice%40geosolutionsroutes.iam.gserviceaccount.com"",
                          ""universe_domain"": ""googleapis.com""
                        }
                        ";


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

                //string url = $"https://maps.googleapis.com/maps/api/geocode/json?address={end}&key={"AIzaSyCNPIJ4r9_wIh7xZhX1qY9jsaXippYj8gA"}";
                string url = $"https://maps.googleapis.com/maps/api/geocode/json?address={end}&key={"AIzaSyDKjNQ4LFm01o93_xLsVAGlCVmf4yE6ezU"}";
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
