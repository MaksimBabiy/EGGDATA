namespace Server.Infrastructure.Models
{
    using Newtonsoft.Json;

    [JsonObject(MemberSerialization.OptOut)]
    public class TokenResponseViewModel
    {
        public string Token { get; set; }

        public int Expiration { get; set; }

        public string RefreshToken { get; set; }
    }
}
