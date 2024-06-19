using System.Text.Json;

namespace _253504_Patrebka_23;

public class RateService : IRateService
    {
        private readonly HttpClient httpClient;

        public RateService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public IEnumerable<Rate> GetRates(DateTime date)
        {
            var baseAddress = httpClient.BaseAddress;
            string apiUrl = $"{baseAddress}?ondate={date.ToString("yyyy-MM-dd")}&periodicity=0";

            try
            {
                HttpResponseMessage response = httpClient.GetAsync(apiUrl).Result;

                if (response.IsSuccessStatusCode)
                {
                    string content = response.Content.ReadAsStringAsync().Result;

                    IEnumerable<Rate>? rates = JsonSerializer.Deserialize<IEnumerable<Rate>>(content);

                    return rates ?? [];
                }
                else
                {
                    Console.WriteLine($"[ERR]: {response.StatusCode}");
                    return [];
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[EXC]: {ex.Message}");
                return [];
            }
        }
    }
