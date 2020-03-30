using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace AmbiantLibrary
{
	public class AmbiantClient
	{
		HttpClient _client;

		public AmbiantClient()
		{
			_client = new HttpClient();
		}

		public async Task<List<WeatherDevice>> GetDeviceDataAsync()
		{
			try
			{
				var json = await _client.GetStringAsync("https://api.ambientweather.net/v1/devices?applicationKey=ac5b0e1a59374538b2618a6ebcd1479ac71c7b34c69b4ea29ed3cbab29a57a67&apiKey=96ad32e3abe24089b1f509eae38fabf1c633e693816947d187521d3eae9b368a");

				var lastDataJson = JsonConvert.DeserializeObject<List<WeatherDevice>>(json);

				return lastDataJson;
			}
			catch (Exception e)
            {
				Console.WriteLine(e.Message);
            }

			return new List<WeatherDevice>();
		}

		public async Task<ForecastInfo> GetForecastDataAsync()
		{
			var json = await _client.GetStringAsync("https://api.darksky.net/forecast/e0db2fc96dbc72db3969b83c38ed0575/38.597929,-121.380819");
			var forecastData = JsonConvert.DeserializeObject<ForecastInfo>(json);

			return forecastData;
		}
	}
}
