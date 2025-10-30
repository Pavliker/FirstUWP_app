using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlbumApp1._0._1.Helpers
{
    [JsonObject]
    public class HashWithSaltResult

    {
        [JsonProperty("Логин")]
        public string Логин {  get; set; }
        [JsonProperty("Salt")]
        public string Salt { get; set; }
        [JsonProperty("Hash")]
        public string Hash { get; set; }

        public HashWithSaltResult(string Логин, string salt,string hash)
        {
            Salt = salt;
            Hash = hash;
            this.Логин = Логин;
        }
        public HashWithSaltResult() { }
    }
}
