using System;
using System.Collections.Generic;
using AmbiantLibrary;
using ClockKit;
using Foundation;
using UIKit;

namespace Ambiance_watch.WatchOSExtension
{
    [Register("ComplicationController")]
    public class ComplicationController : CLKComplicationDataSource
    {
        const string outdoorTempKey = "OutdoorTemp";
        const string indoorTempKey = "IndoorTemp";
        const string windSpeedKey = "WindSpeed";
        const string humidityKey = "Humidity";
        const string uvIndexKey = "UVIndex";
        const string forecastHighKey = "ForecastHigh";
        const string forecastLowKey = "ForecastLow";

        NSUserDefaults userStore = NSUserDefaults.StandardUserDefaults;

        public ComplicationController()
        {
        }

        public override void GetLocalizableSampleTemplate(CLKComplication complication, Action<CLKComplicationTemplate> handler)
        {
            if (complication.Family == CLKComplicationFamily.CircularSmall)
            {
                var template = new CLKComplicationTemplateCircularSmallSimpleImage();
                //var template = new CLKComplicationTemplateCircularSmallRingText();
                template.ImageProvider = CLKImageProvider.Create(UIImage.FromBundle("Image"));
                //template.TextProvider = CLKSimpleTextProvider.FromText("69");
                Console.WriteLine("************* TEMPLATE!!!!!! **************");
                handler(template);
            }

            if (complication.Family == CLKComplicationFamily.GraphicCircular)
            {
                var template = new CLKComplicationTemplateGraphicCircularOpenGaugeRangeText();
                template.CenterTextProvider = CLKSimpleTextProvider.FromText("--");
                template.LeadingTextProvider = CLKSimpleTextProvider.FromText("");
                template.TrailingTextProvider = CLKSimpleTextProvider.FromText("");
                template.GaugeProvider = CLKSimpleGaugeProvider.Create(CLKGaugeProviderStyle.Ring, TemperatureColors.GetColorRange(40, 90), null, (float)0.5);
                Console.WriteLine("************* TEMPLATE!!!!!! **************");
                handler(template);
            }
        }

        //public override void GetPlaceholderTemplate(CLKComplication complication, Action<CLKComplicationTemplate> handler)
        //{
        //    if (complication.Family == CLKComplicationFamily.CircularSmall)
        //    {
        //        var template = new CLKComplicationTemplateCircularSmallSimpleImage();
        //        //var template = new CLKComplicationTemplateCircularSmallRingText();
        //        template.ImageProvider = CLKImageProvider.Create(UIImage.FromBundle("Image"));
        //        //template.TextProvider = CLKSimpleTextProvider.FromText("69");
        //        Console.WriteLine("************* TEMPLATE!!!!!! **************");
        //        handler(template);
        //    }

        //    if (complication.Family == CLKComplicationFamily.GraphicCircular)
        //    {
        //        var template = new CLKComplicationTemplateGraphicCircularOpenGaugeRangeText();
        //        template.CenterTextProvider = CLKSimpleTextProvider.FromText("--");
        //        template.LeadingTextProvider = CLKSimpleTextProvider.FromText("");
        //        template.TrailingTextProvider = CLKSimpleTextProvider.FromText("");
        //        template.GaugeProvider = CLKSimpleGaugeProvider.Create(CLKGaugeProviderStyle.Ring, TemperatureColors.GetColorRange(40, 90), null, (float)0.5);
        //        Console.WriteLine("************* TEMPLATE!!!!!! **************");
        //        handler(template);
        //    }
        //}

        public override void GetCurrentTimelineEntry(CLKComplication complication, Action<CLKComplicationTimelineEntry> handler)
        {
            CLKComplicationTimelineEntry entry = null;

            if (complication.Family == CLKComplicationFamily.CircularSmall)
            {
                var template = new CLKComplicationTemplateCircularSmallSimpleImage();
                //var template = new CLKComplicationTemplateCircularSmallRingText();
                template.ImageProvider = CLKImageProvider.Create(UIImage.FromBundle("Image"));
                //template.TextProvider = CLKSimpleTextProvider.FromText("69");

                entry = CLKComplicationTimelineEntry.Create(NSDate.Now, template);
            }

            if(complication.Family == CLKComplicationFamily.GraphicCircular)
            {
                var outdoorTemp = userStore.StringForKey(outdoorTempKey);
                var forecastHigh = userStore.StringForKey(forecastHighKey);
                var forecastLow = userStore.StringForKey(forecastLowKey);

                var highTemp = int.Parse(forecastHigh);
                var lowTemp = int.Parse(forecastLow);
                var outsideTemp = int.Parse(outdoorTemp);

                var template = new CLKComplicationTemplateGraphicCircularOpenGaugeRangeText();
                template.CenterTextProvider = CLKSimpleTextProvider.FromText(outdoorTemp);
                template.LeadingTextProvider = CLKSimpleTextProvider.FromText(forecastLow);
                template.TrailingTextProvider = CLKSimpleTextProvider.FromText(forecastHigh);

                var guageFill = (float)(outsideTemp - lowTemp) / (highTemp - lowTemp);

                template.GaugeProvider = CLKSimpleGaugeProvider.Create(
                    CLKGaugeProviderStyle.Ring,
                    TemperatureColors.GetColorRange(lowTemp, highTemp),
                    null,
                    guageFill
                );
                //template.GaugeProvider = CLKSimpleGaugeProvider.Create(CLKGaugeProviderStyle.Ring, TemperatureColors.GetColorRange(48, 79), null, (float)0.8);
                Console.WriteLine("************* Complicate!!!!!! **************");

                entry = CLKComplicationTimelineEntry.Create(NSDate.Now, template);
            }
            handler(entry);

        }
        public override void GetSupportedTimeTravelDirections(CLKComplication complication, Action<CLKComplicationTimeTravelDirections> handler)
        {
            handler(CLKComplicationTimeTravelDirections.None);
        }
    }

    internal class TemperatureColors
    {
        static UIColor tempMinus20 = UIColor.FromRGB(229, 0, 229);
        static UIColor tempMinus10 = UIColor.FromRGB(206, 0, 255);
        static UIColor temp0 = UIColor.FromRGB(79, 0, 255);
        static UIColor temp10 = UIColor.FromRGB(0, 63, 255);
        static UIColor temp20 = UIColor.FromRGB(0, 165, 255);
        static UIColor temp30 = UIColor.FromRGB(2, 225, 251);
        static UIColor temp40 = UIColor.FromRGB(66, 251, 123);
        static UIColor temp50 = UIColor.FromRGB(187, 251, 2);
        static UIColor temp60 = UIColor.FromRGB(251, 225, 2);
        static UIColor temp70 = UIColor.FromRGB(255, 178, 0);
        static UIColor temp80 = UIColor.FromRGB(255, 116, 0);
        static UIColor temp90 = UIColor.FromRGB(209, 41, 1);
        static UIColor temp100 = UIColor.FromRGB(176, 35, 48);
        static UIColor temp110 = UIColor.FromRGB(205, 98, 119);

        static UIColor[] colors = new UIColor[] { tempMinus20, tempMinus10, temp0, temp10, temp20, temp30, temp40, temp50, temp60, temp70, temp80, temp90, temp100, temp110 };

        public static UIColor[] GetColorRange(int minTemp, int maxTemp)
        {
            int minColorIndex = (minTemp / 10) + 2;
            int maxColorIndex = (maxTemp / 10) + 2;
            int arraySize = (maxColorIndex - minColorIndex) + 1;

            UIColor[] gaugeColors = new UIColor[arraySize];

            for (int i = 0; i < arraySize; i++)
                gaugeColors[i] = colors[minColorIndex + i];

            return gaugeColors;
        }
    }
}
