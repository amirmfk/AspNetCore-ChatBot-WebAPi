namespace ChatBot.Services
{
    public interface IChatService
    {
        public Task<string> GetResponseAsync(string message);
    }
}
