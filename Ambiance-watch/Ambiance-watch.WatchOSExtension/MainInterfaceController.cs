using System;
using System.Diagnostics;
using System.Threading.Tasks;
using AmbiantLibrary;
using ClockKit;
using Foundation;
using WatchKit;

namespace Ambiance_watch.WatchOSExtension
{
    public partial class MainInterfaceController : WKInterfaceController
    {
        const string outdoorTempKey = "OutdoorTemp";
        const string indoorTempKey = "IndoorTemp";
        const string windSpeedKey = "WindSpeed";
        const string humidityKey = "Humidity";
        const string uvIndexKey = "UVIndex";
        const string forecastHighKey = "ForecastHigh";
        const string forecastLowKey = "ForecastLow";
        const string lastDeviceUpdateTimeKey = "LastDeviceUpdateTime";
        const string lastForecastUpdateTimeKey = "LastForecastUpdateTime";
        const string prevUpdateSuccessfulKey = "prevUpdateSuccessful";

        bool enabled = false;
        AmbiantClient amClient;
        NSUserDefaults userStore = NSUserDefaults.StandardUserDefaults;

        public MainInterfaceController(IntPtr handle) : base(handle)
        {

        }

        public override void Awake(NSObject context)
        {
            base.Awake(context);

            amClient = new AmbiantClient();
            DetailsTable.SetNumberOfRows(3, "default");
            UpdateWatchUI();
        }

        public async override void WillActivate()
        {
            base.WillActivate();

            var lastDeviceUpdateTime = userStore.StringForKey(lastDeviceUpdateTimeKey);
            var lastForecastUpdateTime = userStore.StringForKey(lastForecastUpdateTimeKey);
            var prevUpdateSuccessful = userStore.BoolForKey(prevUpdateSuccessfulKey);
            var updatedData = false;

            if (string.IsNullOrEmpty(lastDeviceUpdateTime) || string.IsNullOrEmpty(lastForecastUpdateTime) || !prevUpdateSuccessful)
            {
                await UpdateDeviceData();
                await UpdateForecastData();
                updatedData = true;
                Debug.WriteLine("Update all data");
            }
            else
            {
                if (DateTime.TryParse(lastDeviceUpdateTime, out DateTime lastDeviceUpdate))
                {
                    if (lastDeviceUpdate.AddMinutes(1) < DateTime.UtcNow)
                    {
                        await UpdateDeviceData();
                        updatedData = true;
                        Debug.WriteLine("Updated Device Data");
                    }
                }

                if (DateTime.TryParse(lastForecastUpdateTime, out DateTime lastForecaseUpdate))
                {
                    if (lastForecaseUpdate.AddMinutes(30) < DateTime.UtcNow)
                    {
                        await UpdateForecastData();
                        updatedData = true;
                        Debug.WriteLine("Updated forecast data");
                    }
                }
            }

            if (updatedData)
            {
                UpdateWatchUI();
                

                if (CLKComplicationServer.SharedInstance.ActiveComplications.Length > 0)
                {
                    CLKComplicationServer.SharedInstance.ReloadTimeline(CLKComplicationServer.SharedInstance.ActiveComplications[0]);
                }
            }

            WKExtension.SharedExtension.ScheduleBackgroundRefresh(NSDate.FromTimeIntervalSinceNow(10), null, (error) =>
            {
                if (error != null)
                    Console.WriteLine(error);
            });
        }

        partial void MyButtonPressed()
        {
            enabled = !enabled;
            TheSwitch.SetOn(enabled);
        }

        void UpdateWatchUI()
        {
            var outdoorTemp = userStore.StringForKey(outdoorTempKey);
            var indoorTemp = userStore.StringForKey(indoorTempKey);
            var windSpeed = userStore.StringForKey(windSpeedKey);
            var humidity = userStore.StringForKey(humidityKey);
            var uvIndex = userStore.StringForKey(uvIndexKey);
            var forecastHigh = userStore.StringForKey(forecastHighKey);
            var forecastLow = userStore.StringForKey(forecastLowKey);

            OutsideTempLabel.SetText($"{outdoorTemp}°");
            IndoorTempLabel.SetText($"{indoorTemp}° Inside");
            ForcastLabel.SetText($"↑ {forecastHigh}° ↓ {forecastLow}°");

            for (int i = 0; i < 3; i++)
            {
                var row = (DetailsRowController)DetailsTable.GetRowController(i);

                if (i == 0)
                {
                    row.TitleLabel.SetText("UV Index");
                    row.DetailLabel.SetText($"{uvIndex}");
                }
                else if (i == 1)
                {
                    row.TitleLabel.SetText("Humidity");
                    row.DetailLabel.SetText($"{humidity}%");
                }
                else if (i == 2)
                {
                    row.TitleLabel.SetText("Wind");
                    row.DetailLabel.SetText($"{windSpeed}");
                }
            }
        }

        async Task UpdateDeviceData()
        {
            var deviceData = await amClient.GetDeviceDataAsync();

            if (deviceData.Count > 0)
            {
                var data = deviceData[0].LastData;

                userStore.SetString(data.OutdoorTemp.ToString("N0"), outdoorTempKey);
                userStore.SetString(data.IndoorTemp.ToString("N0"), indoorTempKey);
                userStore.SetString($"{data.WindSpeed}mph {data.WindCardinalDir}", windSpeedKey);
                userStore.SetString(data.Humidity.ToString(), humidityKey);
                userStore.SetString($"{data.UVIndex} {data.UVIndexRating}", uvIndexKey);

                userStore.SetString(DateTime.UtcNow.ToString(), lastDeviceUpdateTimeKey);
                userStore.SetBool(true, prevUpdateSuccessfulKey);
            }
            else
            {
                userStore.SetBool(false, prevUpdateSuccessfulKey);
            }
        }

        async Task UpdateForecastData()
        {
            var forecastData = await amClient.GetForecastDataAsync();

            if (forecastData.Daily != null)
            {
                var forecast = forecastData.Daily.Data[0];

                userStore.SetString(forecast.TemperatureHigh.ToString("N0"), forecastHighKey);
                userStore.SetString(forecast.TemperatureLow.ToString("N0"), forecastLowKey);

                userStore.SetString(DateTime.UtcNow.ToString(), lastForecastUpdateTimeKey);
                userStore.SetBool(true, prevUpdateSuccessfulKey);
            }
            else
            {
                userStore.SetBool(false, prevUpdateSuccessfulKey);
            }
        }
    }

}
