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

		public async Task<DeviceDataResponse> GetDeviceDataAsync()
		{
			try
			{
				var json = await _client.GetStringAsync("http://ambiserve.underdeveloper.com/api/");

				var lastDataJson = JsonConvert.DeserializeObject<DeviceDataResponse>(json);

				return lastDataJson;
			}
			catch (Exception e)
            {
				Console.WriteLine(e.Message);
            }

			return null;
		}

		public async Task<ForecastInfo> GetForecastDataAsync()
		{
			var json = await _client.GetStringAsync("https://api.darksky.net/forecast/e0db2fc96dbc72db3969b83c38ed0575/38.597929,-121.380819");
			var forecastData = JsonConvert.DeserializeObject<ForecastInfo>(json);

			return forecastData;
		}
	}
}
