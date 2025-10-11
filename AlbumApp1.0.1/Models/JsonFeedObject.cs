using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlbumApp1._0._1.Models
{
    [Serializable]
    public class JsonFeedObject
    {
        public JsonFeedObject() { }
        public DefaultConnectionString? ConnectionStrings { get; set; }
    }
}
