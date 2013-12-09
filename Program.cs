using System;
using Newtonsoft.Json.Converters;
using System.Dynamic;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace JsonSerializationDemo
{
    class MainClass
    {

        public static void Main(string[] args)
        {
			ExtractDataManually();
			PojoEmbedded();
			ComplexObject();

			// Clone();
			// Backup();

			// var b = player.toJson();
			// player.doSomething();
			// rollback: player = b.toPlayer();

        }

		public static void ExtractDataManually()
		{
			var settings = new JsonSerializerSettings
			{
				Formatting = Formatting.Indented,
			};

			Dictionary<string, Object> dict = new Dictionary<string, object>();

			dynamic input = new ExpandoObject();
			input.name = "Ke Ge";
			input.weight = new ExpandoObject();
			input.weight.value = 98;
			input.weight.unit = "kg";

			var json = JsonConvert.SerializeObject(input, settings);
			dynamic output = JsonConvert.DeserializeObject<ExpandoObject>(json, new ExpandoObjectConverter());

			Console.WriteLine("--- ExtractDataManually ---");
			Console.WriteLine(json);
		}

		public static void PojoEmbedded()
		{
			var settings = new JsonSerializerSettings
			{
				Formatting = Formatting.Indented,
			};
			settings.Converters.Add(new PlayerJsonConverter());

			var player = new Player
			{
				Info = new PlayerInfo
				{
					Name = "浩川哥",
					Id = 100,
				},
			};

			var json = JsonConvert.SerializeObject(player, settings);
			var anotherInstance = JsonConvert.DeserializeObject<Player>(json);

			Console.WriteLine("--- PojoEmbedded ---");
			Console.WriteLine(json);

		}

		public static void ComplexObject()
		{
			var settings = new JsonSerializerSettings
			{
				PreserveReferencesHandling = PreserveReferencesHandling.Objects,
				Formatting = Formatting.Indented,
				TypeNameHandling = TypeNameHandling.All,
			};

			var configurationGetter = new Dictionary<int, SomeConfiguration>
			{
				{ 1, new SomeConfiguration { Id = 1 } },
				{ 2, new SomeConfiguration { Id = 2 } },
			};

			settings.Converters.Add(new ConfigurationConverter(configurationGetter));

			var player1 = new ComplexPlayer("周麟")
			{
				Configuration = configurationGetter[1],
				TempState = "hello",
			};

			var player2 = new ComplexPlayer("林嘉炜")
			{
				Configuration = configurationGetter[2],
				TempState = "hello",
			};

			var player3 = new ComplexPlayer("李腾宇")
			{
				Configuration = configurationGetter[2],
				TempState = "hello",
				Friends = new List<ComplexPlayer> { player1, player2, player2 /* repeated */ },
			};

			var json = JsonConvert.SerializeObject(player3, settings);
			var anotherPlayer3 = JsonConvert.DeserializeObject<ComplexPlayer>(json);

			// UnitTest
			var jsonAgain = JsonConvert.SerializeObject(anotherPlayer3, settings);
			Console.WriteLine("json {0} jsonAgain", json.Equals(jsonAgain) ? "==" : "/=");

			Console.WriteLine("--- ComplexObject ---");
			Console.WriteLine(json);

		}
    }
}
