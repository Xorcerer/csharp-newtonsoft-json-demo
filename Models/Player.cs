using System;

namespace JsonSerializationDemo
{
	/// <summary>
	/// A pojo to domonstrate serialization with embedded object.
	/// </summary>
	public class PlayerInfo
	{
		public string Name { get; set; }
		public long Id { get; set; }
	}

    public class Player
    {
		public PlayerInfo Info { get; set; }

		// Other methods.
    }
}

