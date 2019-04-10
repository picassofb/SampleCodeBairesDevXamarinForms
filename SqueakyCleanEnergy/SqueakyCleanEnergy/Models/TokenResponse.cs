using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace SqueakyCleanEnergy.Models
{
    class TokenResponse
    {
        [JsonProperty("token")]
        public string Token { get; set; }

        [JsonProperty("expiration")]
        public DateTimeOffset Expiration { get; set; }
    }
}
