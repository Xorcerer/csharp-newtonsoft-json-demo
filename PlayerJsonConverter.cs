using System;
using Newtonsoft.Json;

namespace JsonSerializationDemo
{
	public class PlayerJsonConverter : JsonConverter
	{
		#region implemented abstract members of JsonConverter
		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
		{
			var player = (Player)value;
			serializer.Serialize(writer, player.Info);
		}

		public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
		{
			if (reader.TokenType == JsonToken.Null)
				return null;

			var info = serializer.Deserialize<PlayerInfo>(reader);
			return new Player { Info = info };
		}

		public override bool CanConvert(Type objectType)
		{
			return objectType == typeof(Player);
		}
		#endregion
	}


}