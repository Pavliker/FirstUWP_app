
using AlbumApp1._0._1.Core.Helpers;
using AlbumApp1._0._1.Models;
using Microsoft.EntityFrameworkCore.Storage.Json;
using Microsoft.Extensions.Logging.Console;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Newtonsoft.Json.Bson;
using Newtonsoft.Json.Linq;
using System.Collections;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.SqlTypes;
using System.IdentityModel.Tokens.Jwt;
using System.IO.Pipelines;
using System.Linq;
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

        public static string GenerateRandomCryptographicKey(int keyLength)
        {
            return Convert.ToBase64String(GenerateRandomCryptographicBytes(keyLength));
        }

        public static byte[] GenerateRandomCryptographicBytes (int keyLength)
        {
            RNGCryptoServiceProvider rngCryptoServiceProvider = new RNGCryptoServiceProvider();
            byte[] randomBytes = new byte[keyLength];
            rngCryptoServiceProvider.GetBytes(randomBytes);
            return randomBytes; 
        }

        public static HashWithSaltResult HashingPassword(string login, string? password, int saltLength, HashAlgorithm hashAlgo)
        {
            var bytes = Encoding.UTF8.GetBytes(password);
            byte[] saltBytes = GenerateRandomCryptographicBytes((int)bytes.Length);
            
            List<byte> passwordWithSaltBytes = new List<byte>();
            passwordWithSaltBytes.AddRange(bytes);
            passwordWithSaltBytes.AddRange(saltBytes);

            var hashed = hashAlgo.ComputeHash(passwordWithSaltBytes.ToArray());
            //var hashConvertToText = BitConverter.ToString(hashed).Replace("-", "");
            return  new HashWithSaltResult(login, Convert.ToBase64String(saltBytes), Convert.ToBase64String(hashed));
            /*hashConvertToText.ToString()*/
        }

        public static async Task SerializeObject<T> (T Obj) 
        {
            string jsonString = null;
            string path = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..\\..\\..\\..\\..\\..\\file1.json"));
            string str = File.ReadAllText(path);
            JArray array = new JArray();
            if (!string.IsNullOrEmpty(str))
            {
                array = JArray.Parse(str);
            }
            var lst = new List<T>();

            if (array != null)
            {
                foreach (var i in array)
            {

                lst.Add(i.ToObject<T>());
            }
           
                //foreach (var i in lst)
                //{
                //    if (lst.Contains(Obj) || lst.Count!=0)
                //    {
                //        if (Obj != null)
                //        {
                //            lst.Remove(i);
                //            break;
                //        }
                //    }

                   
                //}
                if (!lst.Contains(Obj))
                {
                  
                        lst.Add(Obj);
                    
                }
            }
         







            jsonString = JsonConvert.SerializeObject(lst, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore, Formatting = Formatting.Indented });

            await File.WriteAllTextAsync(path, jsonString);
            //string json = File.ReadAllText(path);
            //using (FileStream fs = new FileStream(path, FileMode.OpenOrCreate,FileAccess.ReadWrite))
            //{

            //    using (StreamReader sr = new StreamReader(fs))
            //    {

            //        using (JsonTextReader reader = new JsonTextReader(sr))
            //        {

            //            while (reader.Read())
            //            {
            //                if (reader.TokenType == JsonToken.StartObject)

            //                {
            //                    obj = JObject.Load(reader);

            //                    //obj = JArray.Load(reader);

            //                }
            //            }
            //            reader.Close();
            //        }
            //    }
            //}
            //foreach (var i in obj)
            //{
            //    array.Add(JObject.FromObject(i));
            //}

            //if (array.Count() == 0)
            //{
            //    //obj.Add(Obj);
            //    lst.Add(Obj);

            //}



            //var items = new List<string>();

            //string jsonString = JsonConvert.SerializeObject(Obj, Formatting.None, new JsonSerializerSettings {NullValueHandling = NullValueHandling.Ignore, Formatting = Formatting.Indented });


        }
        public static string Verify(string password, string salt)
        {
            RNGCryptoServiceProvider rngCryptoServiceProvider = new RNGCryptoServiceProvider();

            var passwordbytes = Encoding.UTF8.GetBytes(password);
            var saltBytes = Convert.FromBase64String(salt);

            //rngCryptoServiceProvider.GetBytes(salt);

            List<byte> passwordWithSaltBytes = new List<byte>();
            passwordWithSaltBytes.AddRange(passwordbytes);
            passwordWithSaltBytes.AddRange(saltBytes);
            var hashed1 = SHA512.Create().ComputeHash(passwordWithSaltBytes.ToArray());

            //var hashed = SHA512.Create().ComputeHash(passwordWithSaltBytes.ToArray());
            string smp1 = Convert.ToBase64String(saltBytes);
            string smp2 = Convert.ToBase64String(hashed1);
            return string.Concat(smp2,smp1);

        }
        public static async Task<IList<T>> DeserializeObject<T>() where T : new() 
        {
            string path = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..\\..\\..\\..\\..\\..\\file1.json"));
            string jsonnew = await File.ReadAllTextAsync(path);
            // //Collection<String> list = new Collection<String>();
            // //var lst = new List<JArray>();
            // //foreach (var obj in jsonnew)
            // //{
            // //    list.Add(obj);
            // //}
            // //var modelList = lst.Select(o => o.ToObject<HashWithSaltResult>()).ToList();
            // var model = JsonConvert.DeserializeObject<List<jsonnew>>
            // //dynamic array = JsonConvert.DeserializeObject<dynamic>(jsonnew);
            // //var items = (List<HashWithSaltResult>)JsonConvert.DeserializeObject(jsonnew, typeof(List<HashWithSaltResult>));
            // //foreach (var i in array)
            // //{

            // //}
            // //return lst;
            // return modelList;
            List<string> InvalidJsonElements = null;

            var array = JArray.Parse(jsonnew);
            //JObject o = JObject.Parse(jsonnew);

            //var array = (JArray)o["HashWithSaltResult"];
            IList<T> objectList = new List<T>();
            foreach (var item in array)
            {
                try
                {
                    objectList.Add(item.ToObject<T>());

                }
                catch(Exception ex)
                {
                    InvalidJsonElements = InvalidJsonElements ?? new List<string>();
                    InvalidJsonElements.Add(item.ToString());
                }
            }
            return objectList;
        }
    }
}