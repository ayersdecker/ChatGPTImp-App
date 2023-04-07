using Newtonsoft.Json;
using System.Net.Http.Headers;

namespace ConsoleInterface
{
    internal class Program
    {
        
        static async Task Main(string[] args)
        {
            // Holds the API key
            string apiKey = "YOUR API HERE";
            
            // Console interface
            Console.Title = "ChatGPT Console Interface";

            // Main loop
            while (true)
            {
                Console.Write("Prompt: ");
                Console.WriteLine(await API(Console.ReadLine(), apiKey));
                Console.ForegroundColor = ConsoleColor.Gray;
            }
        }
        static async Task<string> API(string prompt, string apiKey) // Takes the prompt and the API key and returns the response
        {
            // API call
            var client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", apiKey);
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri("https://api.openai.com/v1/completions"),
                Content = new StringContent("{\n   \"model\": \"text-davinci-003\", \"prompt\": \"" + $"{prompt}\",\n    \"max_tokens\": 2000,\n    \"temperature\": 0.5\n}}")
            };
            request.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            // Response Handling
            var response = await client.SendAsync(request);
            var responseContent = await response.Content.ReadAsStringAsync();

            Console.ForegroundColor = ConsoleColor.Green;
            
            // Returns the Response Deserialized
            dynamic json = JsonConvert.DeserializeObject(responseContent);
            return json.choices[0].text + "\n\n";

        }
    }
}