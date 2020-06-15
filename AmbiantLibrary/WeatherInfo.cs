using System;
using System.Collections.Generic;
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

        public string WindCardinalDir { get => ConvertToCardinalDir(WindDir); }

        [JsonProperty("WindSpeedMph")]
		public double WindSpeed { get; set; }

        [JsonProperty("WindGustMph")]
		public double GustSpeed { get; set; }

        public double MaxDailyGust { get; set; }

        public int WindGustDir { get; set; }

        [JsonProperty("uv")]
		public int UVIndex { get; set; }

        public string UVIndexRating { get => GetIndexRating(UVIndex); }

        public double SolarRadiation { get; set; }

		[JsonProperty("TempF")]
		public double OutdoorTemp { get; set; }

        public int Humidity { get; set; }

        public double Baromrelin { get; set; }

		[JsonProperty("tempinf")]
		public double IndoorTemp { get; set; }


        string ConvertToCardinalDir(int heading)
        {
            if ((heading >= 330 && heading <= 359) || (heading >= 0 && heading < 30))
                return "N";
            else if (heading >= 30 && heading < 60)
                return "NE";
            else if (heading >= 60 && heading < 120)
                return "E";
            else if (heading >= 120 && heading < 150)
                return "SE";
            else if (heading >= 150 && heading < 210)
                return "S";
            else if (heading >= 210 && heading < 240)
                return "SW";
            else if (heading >= 240 && heading < 300)
                return "W";
            else
                return "NW";
        }

        string GetIndexRating(int index)
        {
            if (index == 1 || index == 2)
                return "Low";
            else if (index >= 3 && index <= 5)
                return "Moderate";
            else if (index == 6 || index == 7)
                return "High";
            else if (index >= 8 && index <= 10)
                return "Very High";
            else if (index >= 11)
                return "Extreme";
            else
                return "None";
        }
    }

    public class DeviceDataResponse
    {
        [JsonProperty("weatherData")]
        public List<WeatherDevice> Devices { get; set; }


        public ForecastInfo ForecastInfo { get; set; }
        
    }
}
