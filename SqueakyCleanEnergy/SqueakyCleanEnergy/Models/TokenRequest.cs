using Newtonsoft.Json;

namespace SqueakyCleanEnergy.Models
{
    class TokenRequest
    {
        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("password")]
        public string Password { get; set; }
    }
}
