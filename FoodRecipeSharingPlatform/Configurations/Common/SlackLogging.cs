using System.Text;
using Serilog.Core;
using Serilog.Events;

namespace FoodRecipeSharingPlatform.Configurations.Common;

public class SlackLogging : ILogEventSink
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly string _webhookUrl = null!;
    public SlackLogging(IHttpClientFactory httpClientFactory, string webhook_url)
    {
        _httpClientFactory = httpClientFactory;
        _webhookUrl = webhook_url;
    }
    public void Emit(LogEvent logEvent)
    {
        var payload = new
        {
            username = "Food Recipe Sharing Platform Logger",
            text = logEvent.RenderMessage(),
            icon_emoji = ":alert:"
        };
        var jsonPayload = Newtonsoft.Json.JsonConvert.SerializeObject(payload);
        var httpContent = new StringContent(jsonPayload, Encoding.UTF8, "application/json");
        var client = _httpClientFactory.CreateClient();
        try
        {
            var response = client.PostAsync(_webhookUrl, httpContent).Result;
            if (!response.IsSuccessStatusCode)
            {
                Console.WriteLine($"Failed to send log to Slack: {response.StatusCode}");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error sending log to Slack: {ex.Message}");
        }
    }
}