using MovieApp.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieApp
{
    public class ShowHeaderConverter : JsonConverter
    {

        /// <summary>
        /// Megadja, hogy átalakítható-e a json.
        /// </summary>
        /// <param name="objectType">Az objektum típusa.</param>
        /// <returns>Igaz, ha átalakítható. Hamis ,ha nem.</returns>
        public override bool CanConvert(Type objectType)
        {
           return objectType == typeof(ShowHeader);
        }

        /// <summary>
        /// A json olvasása és megfelelő objektum létrehozása.
        /// </summary>
        /// <param name="reader">A json olvasó.</param>
        /// <param name="objectType">Az objektum típusa.</param>
        /// <param name="existingValue">A kilépő érték.</param>
        /// <param name="serializer">A json sorosító.</param>
        public override object ReadJson(JsonReader reader, Type objectType,object existingValue, JsonSerializer serializer)
        {
            JObject obj = JObject.Load(reader);
            if (obj["name"] != null)
            {
                
                return obj.ToObject<SeriesHeader>(serializer);
            }
            else
            {
                return obj.ToObject<MovieHeader>(serializer);
            }
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }
    }

}
