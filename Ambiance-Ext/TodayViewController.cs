using System;

using NotificationCenter;
using Foundation;
using UIKit;
using System.Net.Http;
using Newtonsoft.Json;
using System.Collections.Generic;
using CoreGraphics;
using System.Diagnostics;
using System.Linq;
using AmbiantLibrary;
using System.Threading.Tasks;

namespace AmbianceExt
{
	public partial class TodayViewController : UIViewController, INCWidgetProviding
	{
		const string outdoorTempKey = "OutdoorTemp";
		const string indoorTempKey = "IndoorTemp";
		const string windSpeedKey = "WindSpeed";
		const string humidityKey = "Humidity";
		const string uvIndexKey = "UVIndex";
		const string forecastHighKey = "ForecastHigh";
		const string forecastLowKey = "ForecastHigh";
		const string lastDeviceUpdateTimeKey = "LastDeviceUpdateTime";
		const string lastForecastUpdateTimeKey = "LastForecastUpdateTime";
		const string prevUpdateSuccessfulKey = "prevUpdateSuccessful";

		NSUserDefaults userStore = NSUserDefaults.StandardUserDefaults;
		AmbiantClient amClient = new AmbiantClient();

		protected TodayViewController(IntPtr handle) : base(handle)
		{
			// Note: this .ctor should not contain any initialization logic.
		}

		public override void DidReceiveMemoryWarning()
		{
			// Releases the view if it doesn't have a superview.
			base.DidReceiveMemoryWarning();

			// Release any cached data, images, etc that aren't in use.
		}

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();

            //ExtensionContext.SetWidgetLargestAvailableDisplayMode(NCWidgetDisplayMode.Expanded);
            LoadingIndicator.HidesWhenStopped = true;

            //MainInfoView.Hidden = true;

            //LoadingIndicator.StartAnimating();

            

            //MainInfoView.Hidden = false;

        }

		[Export("widgetPerformUpdateWithCompletionHandler:")]
		public void WidgetPerformUpdate(Action<NCUpdateResult> completionHandler)
		{
			// If an error is encoutered, use NCUpdateResultFailed
			// If there's no update required, use NCUpdateResultNoData
			// If there's an update, use NCUpdateResultNewData

			
			// This method is called by iOS at 1) undetmerined intevals when the widget is
			// in the background and 2) when the widget is loaded on the Today View. Newer
			// data is retieved here and then saved in the app's user store.


			var lastDeviceUpdateTime = userStore.StringForKey(lastDeviceUpdateTimeKey);
			var lastForecastUpdateTime = userStore.StringForKey(lastForecastUpdateTimeKey);
			var prevUpdateSuccessful = userStore.BoolForKey(prevUpdateSuccessfulKey);
			var updatedData = false;

			if (string.IsNullOrEmpty(lastDeviceUpdateTime) || string.IsNullOrEmpty(lastForecastUpdateTime) || !prevUpdateSuccessful)
			{
				UpdateDeviceData();
				UpdateForecastData();
				updatedData = true;
				Debug.WriteLine("Update all data");
			}
            else
            {
                if (DateTime.TryParse(lastDeviceUpdateTime, out DateTime lastDeviceUpdate))
                {
					if (lastDeviceUpdate.AddMinutes(1) < DateTime.UtcNow)
					{
						UpdateDeviceData();
						updatedData = true;
						Debug.WriteLine("Updated Device Data");
					}
				}

				if(DateTime.TryParse(lastForecastUpdateTime, out DateTime lastForecaseUpdate))
                {
					if (lastForecaseUpdate.AddHours(1) < DateTime.UtcNow)
					{
						UpdateForecastData();
						updatedData = true;
						Debug.WriteLine("Updated forecast data");
					}
				}
            }

			if (updatedData)
				completionHandler(NCUpdateResult.NewData);
			else
				completionHandler(NCUpdateResult.NoData);
		}

		//[Export("widgetActiveDisplayModeDidChange:withMaximumSize:")]
		//public void WidgetActiveDisplayModeDidChange(NCWidgetDisplayMode activeDisplayMode, CoreGraphics.CGSize maxSize)
		//{
		//	if (activeDisplayMode == NCWidgetDisplayMode.Compact)
		//		PreferredContentSize = maxSize;
		//	else if (activeDisplayMode == NCWidgetDisplayMode.Expanded)
		//		PreferredContentSize = new CGSize(maxSize.Width, maxSize.Height-100);
		//}

		async void UpdateDeviceData()
        {
			var deviceData = await amClient.GetDeviceDataAsync();

			if (deviceData.Count > 0)
			{
				var data = deviceData[0].LastData;

				userStore.SetString(data.OutdoorTemp.ToString(), outdoorTempKey);
				userStore.SetString(data.IndoorTemp.ToString(), indoorTempKey);
				userStore.SetString(data.WindSpeed.ToString(), windSpeedKey);
				userStore.SetString(data.Humidity.ToString(), humidityKey);
				userStore.SetString(data.UVIndex.ToString(), uvIndexKey);

				userStore.SetString(DateTime.UtcNow.ToString(), lastDeviceUpdateTimeKey);
				userStore.SetBool(true, prevUpdateSuccessfulKey);
			}
			else
            {
				userStore.SetBool(false, prevUpdateSuccessfulKey);
			}
		}

		async void UpdateForecastData()
        {
			var forecastData = await amClient.GetForecastDataAsync();

			if (forecastData.Daily != null)
			{
				var forecast = forecastData.Daily.Data[0];

				userStore.SetString(forecast.TemperatureHigh.ToString("N0"), forecastHighKey);
				userStore.SetString(forecast.TemperatureLow.ToString("N)"), forecastLowKey);

				userStore.SetString(DateTime.UtcNow.ToString(), lastForecastUpdateTimeKey);
				userStore.SetBool(true, prevUpdateSuccessfulKey);
			}
			else
			{
				userStore.SetBool(false, prevUpdateSuccessfulKey);
			}
		}

		//void UpdateWidgetUI()
  //      {
		//	TempLabel.Text = $"{deviceData[0]?.LastData.OutdoorTemp.ToString()}°";
		//	InsideTempLabel.Text = $"{deviceData[0]?.LastData.IndoorTemp.ToString()}° Inside";
		//	WindSpeedLabel.Text = $"{deviceData[0]?.LastData.WindSpeed.ToString()}mph {GetWindDir(deviceData[0].LastData.WindDir)}";
		//	HumidityLabel.Text = $"{deviceData[0]?.LastData.Humidity}%";
		//	UVLabel.Text = $"{deviceData[0]?.LastData.UVIndex} {GetIndexRating(deviceData[0].LastData.UVIndex)}";

		//	UpdatedLabel.Text = $"Updated {deviceData[0]?.LastData.Date.ToLocalTime().ToShortTimeString()}";

		//	ForcastHighLabel.Text = $"↑ {forecast.Daily.Data[0].TemperatureHigh.ToString("N0")}°";
		//	ForcastLowLabel.Text = $"↓ {forecast.Daily.Data[0].TemperatureLow.ToString("N0")}°";

		//	LoadingIndicator.StopAnimating();
		//}

		string GetWindDir(double heading)
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
				return "Nw";
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
}
