using AlbumApp1._0._1.Interfaces;
using AlbumApp1._0._1.Models;
using Microsoft.EntityFrameworkCore.SqlServer.Storage.Internal;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Windows.Media.Casting;
namespace AlbumApp1._0._1.Services
{
    public partial class SqlServerConnectionStatus :  ISqlConnectionStatus
    {
       
        public SqlServerConnectionStatus()
        {
            DbProviderFactories.RegisterFactory("SqlServer", SqlClientFactory.Instance);
               
        }
        public  JsonFeedObject? TakeConnectionString()
        {

            string path = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, "..", "..", "..", "..", "..", "..", "appsettings.json"));


            string jsonText = File.ReadAllText(path);
          
            var dto = JsonConvert.DeserializeObject<JsonFeedObject>(jsonText);

            return dto;

        }
        public   bool Validate()
        {
            var logger = LoggerFactory.Create(cfg => cfg.AddConsole().AddDebug()).CreateLogger("DefaultConnection");
            List<Exception>errors = new List<Exception>();
            IEnumerable<string> invariants = DbProviderFactories.GetProviderInvariantNames();
            try
            {
                var factory = DbProviderFactories.GetFactory(invariants.FirstOrDefault());
                using var connection = factory.CreateConnection();
                if (connection is null)
                {
                    throw new Exception($"\"{invariants.FirstOrDefault()}\"  did not  have a valid database provider registered ");
                }
             
               string? connStr = TakeConnectionString()?.ToString();   
                
                 connection.ConnectionString = connStr;
                connection.Open();
            }
            catch (Exception e)
            {

                var message = $"Could not connect to \"{invariants.FirstOrDefault()}\".";
                logger.LogError(message);
                errors.Add(new Exception(message, e));
            }
            return errors.IsNullOrEmpty();
        }
    }
}
