using System;
using Foundation;
using WatchKit;

namespace Ambiance_watch.WatchOSExtension
{
    [Register("ExtensionDelegate")]
    public class ExtensionDelegate : WKExtensionDelegate
    {
        public ExtensionDelegate()
        {
        }

        public override void HandleBackgroundTasks(NSSet<WKRefreshBackgroundTask> backgroundTasks)
        {
            Console.WriteLine("Background!");

            foreach (WKRefreshBackgroundTask task in backgroundTasks)
            {
                if (task is WKApplicationRefreshBackgroundTask)
                {
                    var config = NSUrlSessionConfiguration.CreateBackgroundSessionConfiguration("AmbianceBackgroundRequest");
                    var session = NSUrlSession.FromConfiguration(config, new BackgroundSessionDelegate(), null);

                    var request = session.CreateDownloadTask(new NSUrl("https://api.darksky.net/forecast/e0db2fc96dbc72db3969b83c38ed0575/38.597929,-121.380819"));
                    request.Resume();
                    task.SetTaskCompleted(false);
                }
                else if (task is WKUrlSessionRefreshBackgroundTask)
                {
                    Console.WriteLine("Background session task hit");
                    task.SetTaskCompleted();
                }
            }
        }
    }

    internal class BackgroundSessionDelegate : NSObject, INSUrlSessionDownloadDelegate
    {
        public BackgroundSessionDelegate() { }

        public void DidFinishDownloading(NSUrlSession session, NSUrlSessionDownloadTask downloadTask, NSUrl location)
        {
            Console.WriteLine("---- Finished Downloading----");
        }
    }
}