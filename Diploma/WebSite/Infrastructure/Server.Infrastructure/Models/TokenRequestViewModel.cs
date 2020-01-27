namespace Server.Infrastructure.Models
{
    using Newtonsoft.Json;

    [JsonObject(MemberSerialization.OptOut)]
    public class TokenRequestViewModel
    {
        public TokenRequestViewModel()
        {
        }

        public string GrantType { get; set; }

        public string ClientId { get; set; }

        public string UserSecret { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }

        public string RefreshToken { get; set; }
    }
}
