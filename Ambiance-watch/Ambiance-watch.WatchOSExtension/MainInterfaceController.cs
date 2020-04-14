using System;
using AmbiantLibrary;
using ClockKit;
using Foundation;
using WatchKit;

namespace Ambiance_watch.WatchOSExtension
{
	public partial class MainInterfaceController : WKInterfaceController
	{
        bool enabled = false;
        AmbiantClient client;

		public MainInterfaceController (IntPtr handle) : base (handle)
		{

		}

        public override void Awake(NSObject context)
        {
            base.Awake(context);

            client = new AmbiantClient();
            DetailsTable.SetNumberOfRows(3, "default");

            UpdatedAtLabel.SetText("AWAKE");
        }

        public async override void WillActivate()
        {
            base.WillActivate();

            var deviceData = await client.GetDeviceDataAsync();
            var forecast = await client.GetForecastDataAsync();

            OutsideTempLabel.SetText($"{deviceData[0]?.LastData.OutdoorTemp.ToString()}°");
            IndoorTempLabel.SetText($"{deviceData[0]?.LastData.IndoorTemp.ToString()}° Inside");
            ForcastLabel.SetText($"↑ {forecast.Daily.Data[0].TemperatureHigh.ToString("N0")}° ↓ {forecast.Daily.Data[0].TemperatureLow.ToString("N0")}°");

            for(int i=0;i<3;i++)
            {
                var row = (DetailsRowController)DetailsTable.GetRowController(i);

                if(i==0)
                {
                    row.TitleLabel.SetText("UV Index");
                    row.DetailLabel.SetText($"{deviceData[0]?.LastData.UVIndex}");
                }
                else if (i == 1)
                {
                    row.TitleLabel.SetText("Humidity");
                    row.DetailLabel.SetText($"{deviceData[0]?.LastData.Humidity}%");
                }
                else if (i == 2)
                {
                    row.TitleLabel.SetText("Wind");
                    row.DetailLabel.SetText($"{deviceData[0]?.LastData.WindSpeed.ToString()}mph {deviceData[0].LastData.WindCardinalDir}");
                }
            }

            UpdatedAtLabel.SetText("***");
            UpdatedAtLabel.SetText(DateTime.Now.ToLongTimeString());

            if (CLKComplicationServer.SharedInstance.ActiveComplications.Length > 0)
            {
                CLKComplicationServer.SharedInstance.ReloadTimeline(CLKComplicationServer.SharedInstance.ActiveComplications[0]);
            }

        }

        partial void MyButtonPressed()
        {
            enabled = !enabled;
            TheSwitch.SetOn(enabled);
        }
    }
}
