using System;
using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;

namespace JsonSerializationDemo
{
    public class ComplexPlayer
    {
		//private string _id = "1";
		//public string Id { get { return _id; } }

		[JsonProperty("name")]
		public string Name { get; private set; }

		public ComplexPlayer(string name)
		{
			Name = name;
		}

		[JsonIgnore]
		public string TempState { get; set; }

		[JsonProperty("configuration")]
		public SomeConfiguration Configuration { get; set; }

		[JsonProperty("friends")]
		public IList<ComplexPlayer> Friends { get; set; }
    }
}

