using System.Threading.Tasks;
using System.Net.Http;
using System.Text.Json;
using System.Text;
using ChatBot.Services;

public class ChatService : IChatService
{
    private readonly HttpClient _httpClient;

    public ChatService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<string> GetResponseAsync(string message)
    {
        var url = "https://api.mistral.ai/v1/chat/completions";
        var apiKey = "Api-key"; 

        var requestBody = new
        {
            model = "mistral-small-latest",
            messages = new[]
        {
            new { role = "system", content = "You are an AI assistant." },
            new { role = "user", content = message }
        },
            max_tokens = 50
        };

        var request = new HttpRequestMessage(HttpMethod.Post, url)
        {
            Content = new StringContent(JsonSerializer.Serialize(requestBody), Encoding.UTF8, "application/json")
        };

        request.Headers.Add("Authorization", $"Bearer {apiKey}");

        using var response = await _httpClient.SendAsync(request);

        var responseString = await response.Content.ReadAsStringAsync();

        if (!response.IsSuccessStatusCode)
        {
            return $"API Error: {response.StatusCode} - {responseString}";
        }

        return responseString;
    }

}

