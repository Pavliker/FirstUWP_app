using AlbumApp1._0._1.Interfaces;
using AlbumApp1._0._1.Models;
using Microsoft.CodeAnalysis;
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
using static System.Runtime.InteropServices.JavaScript.JSType;
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
            //DTE dte = (DTE)GetService(typeof(DTE));
            // Get the full path of the executing assembly
            //string assemblyLocation = System.Reflection.Assembly.GetExecutingAssembly().Location;

            // Extract the directory path
            //string? assemblyDirectory = System.IO.Path.GetDirectoryName(assemblyLocation);
            //string? projectDirectory = Directory.GetParent(workingDirectory).Parent.Parent.FullName;
            //FileInfo fileinfo = new FileInfo("FirstUWP_app\\AlbumApp1.0.1\\appsettings.json");
            //string sCurrentDirectory = AppDomain.CurrentDomain.BaseDirectory;   
            //string path1 = AppDomain.CurrentDomain.BaseDirectory;
            //string path1 = Assembly.GetAssembly(typeof(SomeClassInOtherProject)).Location;

           string path =  Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..\\..\\..\\..\\..\\..\\appsettings.json"));

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
             
               string? connStr = TakeConnectionString()?.ConnectionStrings?.DefaultConnection;   
                
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
