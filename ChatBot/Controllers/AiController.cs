using ChatBot.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

[Route("api/chat")]
[ApiController]
public class AiController : ControllerBase
{
    private readonly IChatService _chatService;

    public AiController(IChatService chatService)
    {
        _chatService = chatService;
    }

    [HttpPost]
    public async Task<IActionResult> Chat([FromBody] ChatRequest request)
    {
        var response = await _chatService.GetResponseAsync(request.Message);
        return Ok(new { reply = response });
    }
}

public class ChatRequest
{
    public string Message { get; set; }
}
