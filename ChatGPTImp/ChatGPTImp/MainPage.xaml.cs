using System.Net.Http.Headers;

namespace ChatGPTImp;

public partial class MainPage : ContentPage
{

	public MainPage()
	{
		InitializeComponent();
	}
	public async Task CallGPT()
	{
        var client = new HttpClient();
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", "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");

        var request = new HttpRequestMessage
        {
            Method = HttpMethod.Post,
            RequestUri = new Uri("https://api.openai.com/v1/completions"),
            Content = new StringContent("{\n    \"prompt\": \"Hello, how are you today?\",\n    \"max_tokens\": 50,\n    \"temperature\": 0.5\n}")
        };
        request.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

        var response = await client.SendAsync(request);
        var responseContent = await response.Content.ReadAsStringAsync();


    }

}

