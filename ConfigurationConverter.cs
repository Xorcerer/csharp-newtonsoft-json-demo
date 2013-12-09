using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;

namespace JsonSerializationDemo
{

	public class ConfigurationConverter : JsonConverter
	{
		IDictionary<int, SomeConfiguration> configurationGetter;

		public ConfigurationConverter(IDictionary<int, SomeConfiguration> configurationGetter)
        {
			this.configurationGetter = configurationGetter;
        }

		#region implemented abstract members of JsonConverter
		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
		{
			var config = (SomeConfiguration)value;

			writer.WriteStartObject();
			writer.WritePropertyName("id");
			writer.WriteValue(config.Id);
			writer.WriteEndObject();
		}

		public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
		{
			if (reader.TokenType == JsonToken.Null)
				return null;

			JObject jObject = JObject.Load(reader);

			int id = (int)jObject.GetValue("id");

			return configurationGetter[id];
		}

		public override bool CanConvert(Type objectType)
		{
			return objectType == typeof(SomeConfiguration);
		}
		#endregion
	}

}