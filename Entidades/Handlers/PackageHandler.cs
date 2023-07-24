using Entidades.Generic;
using Entidades.Specific;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades.Handlers
{
    public static class PackageHandler<T> where T : class
    {
        public static string SerializePackage(EventPackage<T> eventPackage) 
        {

            try
            {
                var jsonSerializerSettings = new JsonSerializerSettings()
                {
                    TypeNameHandling = TypeNameHandling.All
                };
                var json = JsonConvert.SerializeObject(eventPackage, jsonSerializerSettings);            
                return json;
            }
            catch (Exception ex)
            {

                throw ex;
            }                       
        }

        public static EventPackage<T> DeserializePackage(string message) {

            try
            {
                var cleanContent = message.TrimEnd('\u0013');

                var jsonDeserializerSettings = new JsonSerializerSettings()
                {
                    TypeNameHandling = TypeNameHandling.All
                };

                var objData = JsonConvert.DeserializeObject<dynamic>(cleanContent, jsonDeserializerSettings);
                return objData;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
