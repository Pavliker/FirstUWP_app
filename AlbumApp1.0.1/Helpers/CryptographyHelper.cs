
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Newtonsoft.Json.Bson;

using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.Json.Serialization.Metadata;
using System.Threading.Tasks;
using Windows.Storage.Streams;
namespace AlbumApp1._0._1.Helpers
{
    public static class CryptographyHelper
    {
        public static string HashingPassword(string? password)
        {
            var bytes = Encoding.UTF8.GetBytes(password);
            using (SHA512 SHA512 = System.Security.Cryptography.SHA512.Create())
            {
                var hashed = SHA512.ComputeHash(bytes);
                var hashConvertToText = BitConverter.ToString(hashed).Replace("-", "");
                return hashConvertToText.ToString();
            }

        }
        //System.Text.Json

        public static void WriteToJsonFile<T>(string filePath, T objectToWrite, bool append = true) where T : new()
        {
            
            TextWriter writer = null;
            JsonSerializerOptions options = new JsonSerializerOptions { WriteIndented = true };
            try
            {
                using (MemoryStream _MemoryStream = new MemoryStream())
                {
                    using (BsonDataWriter bsonWriterObject = new BsonDataWriter(_MemoryStream))
                    {

                        //var contentsToWriteToFile = System.Text.Json.JsonSerializer.Serialize(bsonWriterObject, objectToWrite);
                        ////var contentsToWriteToFile = Newtonsoft.Json.JsonConvert.Serialize(bsonWriterObject, objectToWrite);
                        //writer = new StreamWriter(contentsToWriteToFile);
                        //writer.Write(contentsToWriteToFile);
                    }
                }
                
                
              
            }
            finally
            {
                if (writer!=null)
                {
                    writer.Close();
                }
            }
        
        }
        
        public static T ReadFromJsonFile<T>(string filePath) where T : new()
        {
            TextReader reader = null;
            try
            {
                reader = new StreamReader(filePath);
                var fileContents = reader.ReadToEnd();
                return Newtonsoft.Json.JsonConvert.DeserializeObject<T>(fileContents);

            }
            finally
            {
                if (reader != null )
                {
                    reader.Close();
                }
            }
        }
      
    }
}
