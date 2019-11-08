// WARNING
//
// This file has been generated automatically by Visual Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using Foundation;
using System.CodeDom.Compiler;

namespace Ambiance_watch.WatchOSApp
{
	[Register ("MainInterfaceController")]
	partial class MainInterfaceController
	{
		[Outlet]
		WatchKit.WKInterfaceLabel IndoorTempLabel { get; set; }

		[Outlet]
		WatchKit.WKInterfaceLabel MyLabel { get; set; }

		[Outlet]
		WatchKit.WKInterfaceLabel OutsideTempLabel { get; set; }

		[Outlet]
		WatchKit.WKInterfaceButton TestButton { get; set; }

		[Outlet]
		WatchKit.WKInterfaceSwitch TheSwitch { get; set; }

		[Action ("MyButtonPressed")]
		partial void MyButtonPressed ();
		
		void ReleaseDesignerOutlets ()
		{
			if (MyLabel != null) {
				MyLabel.Dispose ();
				MyLabel = null;
			}

			if (TestButton != null) {
				TestButton.Dispose ();
				TestButton = null;
			}

			if (OutsideTempLabel != null) {
				OutsideTempLabel.Dispose ();
				OutsideTempLabel = null;
			}

			if (IndoorTempLabel != null) {
				IndoorTempLabel.Dispose ();
				IndoorTempLabel = null;
			}

			if (TheSwitch != null) {
				TheSwitch.Dispose ();
				TheSwitch = null;
			}
		}
	}
}
