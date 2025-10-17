namespace TaskVault.API.Helpers
{
    public class HelperResponse
    {
        public bool Success { get; set; }
        public string Message { get; set; } = null!;
        public object? Data { get; set; }
    }
}
