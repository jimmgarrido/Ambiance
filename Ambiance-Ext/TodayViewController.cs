using System;

using NotificationCenter;
using Foundation;
using UIKit;
using System.Net.Http;
using Newtonsoft.Json;
using System.Collections.Generic;
using CoreGraphics;
using Xamarin.Essentials;
using System.Diagnostics;
using Microcharts.iOS;
using SkiaSharp;
using Microcharts;
using System.Linq;
using AmbiantLibrary;

namespace AmbianceExt
{
	public partial class TodayViewController : UIViewController, INCWidgetProviding
	{
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

		public async override void ViewDidLoad()
		{
			base.ViewDidLoad();

			//ExtensionContext.SetWidgetLargestAvailableDisplayMode(NCWidgetDisplayMode.Expanded);
			LoadingIndicator.HidesWhenStopped = true;

			TempLabel.Hidden = true;
			InsideTempLabel.Hidden = true;
			WindSpeedLabel.Hidden = true;
			HumidityLabel.Hidden = true;
			UVLabel.Hidden = true;
			UpdatedLabel.Hidden = true;
			ForcastHighLabel.Hidden = true;
			ForcastLowLabel.Hidden = true;

			LoadingIndicator.StartAnimating();

			var ambiant = new AmbiantClient();
			var deviceData = await ambiant.GetDeviceDataAsync();

			TempLabel.Text = $"{deviceData[0]?.LastData.OutdoorTemp.ToString()}°";
			InsideTempLabel.Text = $"{deviceData[0]?.LastData.IndoorTemp.ToString()}° Inside";
			WindSpeedLabel.Text = $"{deviceData[0]?.LastData.WindSpeed.ToString()}mph {GetWindDir(deviceData[0].LastData.WindDir)}";
			HumidityLabel.Text = $"{deviceData[0]?.LastData.Humidity}%";
			UVLabel.Text = $"{deviceData[0]?.LastData.UVIndex} {GetIndexRating(deviceData[0].LastData.UVIndex)}";

			UpdatedLabel.Text = $"Updated {deviceData[0]?.LastData.Date.ToLocalTime().ToShortTimeString()}";

			var forecast = await ambiant.GetForecastDataAsync();

			ForcastHighLabel.Text = $"↑ {forecast.Daily.Data[0].TemperatureHigh.ToString("N0")}°";
			ForcastLowLabel.Text = $"↓ {forecast.Daily.Data[0].TemperatureLow.ToString("N0")}°";

			LoadingIndicator.StopAnimating();

			TempLabel.Hidden = false;
			InsideTempLabel.Hidden = false;
			WindSpeedLabel.Hidden = false;
			HumidityLabel.Hidden = false;
			UVLabel.Hidden = false;
			ForcastHighLabel.Hidden = false;
			ForcastLowLabel.Hidden = false;

			//var settings = Foundation.NSUserDefaults.StandardUserDefaults;
			//settings.SetString("Test1","LastUpdate");
			//Debug.WriteLine("Data");

			//var newJson = await client.GetStringAsync("https://api.ambientweather.net/v1/devices/EC:FA:BC:07:CE:60?apiKey=96ad32e3abe24089b1f509eae38fabf1c633e693816947d187521d3eae9b368a&applicationKey=ac5b0e1a59374538b2618a6ebcd1479ac71c7b34c69b4ea29ed3cbab29a57a67&limit=50");

			//var historyJson = JsonConvert.DeserializeObject<List<WeatherInfo>>(newJson);

			//var outdoorTemps = new List<Entry>();
			////var outdoorTemps = historyJson.Select(t => t.OutdoorTemp);

			//foreach(var d in historyJson)
			//{
			//	outdoorTemps.Add(new Entry((float)d.OutdoorTemp));
			//}


			//var entries = new[]
			// {
			//	 new Entry(212)
			//	 {
			//		 Label = "UWP",
			//		 ValueLabel = "212",
			//		 Color = SKColor.Parse("#2c3e50")
			//	 },
			//	 new Entry(248)
			//	 {
			//		 Label = "Android",
			//		 ValueLabel = "248",
			//		 Color = SKColor.Parse("#77d065")
			//	 },
			//	 new Entry(128)
			//	 {
			//		 Label = "iOS",
			//		 ValueLabel = "128",
			//		 Color = SKColor.Parse("#b455b6")
			//	 },
			//	 new Entry(514)
			//	 {
			//		 Label = "Shared",
			//		 ValueLabel = "514",
			//		 Color = SKColor.Parse("#3498db")
			//} };

			//var test = new LineChart() {
			//	Entries = outdoorTemps,
			//	MinValue = 80
			//};
			//var chart = new ChartView()
			//{
			//	Frame = new CGRect(0, 115, View.Bounds.Width, 140)
			//};
			//View.AddSubview(chart);
		}

		[Export("widgetPerformUpdateWithCompletionHandler:")]
		public void WidgetPerformUpdate(Action<NCUpdateResult> completionHandler)
		{
			// Perform any setup necessary in order to update the view.

			// If an error is encoutered, use NCUpdateResultFailed
			// If there's no update required, use NCUpdateResultNoData
			// If there's an update, use NCUpdateResultNewData

			var setting = NSUserDefaults.StandardUserDefaults.StringForKey("LastUpdate");
			Debug.WriteLine($"*****{setting}*****");
			//DateTime lastTime;

			//if(setting != null)
			//{
			//	if(DateTime.TryParse(setting, out lastTime))
			//	{
			//		var test = lastTime.AddMinutes(2);
			//		if(lastTime.AddMinutes(2) >= DateTime.UtcNow)
			//		{
			//			completionHandler(NCUpdateResult.NoData);
			//			Debug.WriteLine("NoData");
			//		}
			//		else
			//		{
			//			completionHandler(NCUpdateResult.NewData);
			//		}
			//	}
			//}
			//else
			//{
			//	completionHandler(NCUpdateResult.NewData);
			//	Debug.WriteLine("Refresh");
			//}

			completionHandler(NCUpdateResult.NewData);
		}

		//[Export("widgetActiveDisplayModeDidChange:withMaximumSize:")]
		//public void WidgetActiveDisplayModeDidChange(NCWidgetDisplayMode activeDisplayMode, CoreGraphics.CGSize maxSize)
		//{
		//	if (activeDisplayMode == NCWidgetDisplayMode.Compact)
		//		PreferredContentSize = maxSize;
		//	else if (activeDisplayMode == NCWidgetDisplayMode.Expanded)
		//		PreferredContentSize = new CGSize(maxSize.Width, maxSize.Height-100);
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
