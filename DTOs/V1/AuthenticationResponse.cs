namespace AspNetCore_RestAPI.DTOs.V1
{
    public class AuthenticationResponse
    {
        public string? Token { get; set; }
        public bool? Success { get; set; }
        public IEnumerable<string>? ErrorMessages { get; set; }
    }
}