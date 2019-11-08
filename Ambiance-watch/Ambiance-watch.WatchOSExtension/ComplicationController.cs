using System;
using AmbiantLibrary;
using ClockKit;
using Foundation;
using UIKit;

namespace Ambiance_watch.WatchOSExtension
{
    [Register("ComplicationController")]
    public class ComplicationController : CLKComplicationDataSource
    {
        public ComplicationController()
        {
        }

        public override void GetPlaceholderTemplate(CLKComplication complication, Action<CLKComplicationTemplate> handler)
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
                var template = new CLKComplicationTemplateGraphicCircularImage();
                template.ImageProvider = CLKFullColorImageProvider.Create(UIImage.FromBundle("Image"));
                //template.TextProvider = CLKSimpleTextProvider.FromText("69");
                Console.WriteLine("************* TEMPLATE!!!!!! **************");
                handler(template);
            }

            
        }

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
                var template = new CLKComplicationTemplateGraphicCircularImage();
                template.ImageProvider = CLKFullColorImageProvider.Create(UIImage.FromBundle("Image"));
                //template.TextProvider = CLKSimpleTextProvider.FromText("69");

                entry = CLKComplicationTimelineEntry.Create(NSDate.Now, template);
            }

            Console.WriteLine("************* ENTRYY!!!!!! **************");

            handler(entry);

        }
        public override void GetSupportedTimeTravelDirections(CLKComplication complication, Action<CLKComplicationTimeTravelDirections> handler)
        {
            handler(CLKComplicationTimeTravelDirections.None);
        }
    }
}
