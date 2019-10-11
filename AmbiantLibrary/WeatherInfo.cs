using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace AmbiantLibrary
{
	public class WeatherDevice
	{
		public DeviceInfo Info { get; set; }
		public string MacAddress { get; set; }
		public WeatherInfo LastData { get; set; }
	}

	public class DeviceInfo
	{
		public string Name { get; set; }
		public string Location { get; set; }
	}

	public class WeatherInfo
	{
		public long DateUtc { get; set; }
		public DateTime Date { get; set; }

		[JsonProperty("WinDir")]
		public int WindDir { get; set; }
		[JsonProperty("WindSpeedMph")]
		public double WindSpeed { get; set; }
		[JsonProperty("WindGustMph")]
		public double GustSpeed { get; set; }
		public double MaxDailyGust { get; set; }
		public int WindGustDir { get; set; }
		[JsonProperty("uv")]
		public int UVIndex { get; set; }
		public double SolarRadiation { get; set; }

		[JsonProperty("TempF")]
		public double OutdoorTemp { get; set; }
		public int Humidity { get; set; }
		public double Baromrelin { get; set; }

		[JsonProperty("tempinf")]
		public double IndoorTemp { get; set; }
	}
}
