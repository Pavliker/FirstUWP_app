using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlbumApp1._0._1.Models
{
    [Serializable]
    public class DefaultConnectionString

    {
        [JsonProperty]

        public string? DefaultConnection { get; set; }

        public DefaultConnectionString() {
        }
    }
}
