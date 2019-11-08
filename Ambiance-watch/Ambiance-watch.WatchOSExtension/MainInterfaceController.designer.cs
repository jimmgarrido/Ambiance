// WARNING
//
// This file has been generated automatically by Visual Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using Foundation;
using System.CodeDom.Compiler;

namespace Ambiance_watch.WatchOSExtension
{
	[Register ("MainInterfaceController")]
	public partial class MainInterfaceController
	{
		[Outlet]
		WatchKit.WKInterfaceTable DetailsTable { get; set; }

		[Outlet]
		WatchKit.WKInterfaceLabel ForcastLabel { get; set; }

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

		[Outlet]
		WatchKit.WKInterfaceLabel UpdatedAtLabel { get; set; }

		[Action ("MyButtonPressed")]
		partial void MyButtonPressed ();
		
		void ReleaseDesignerOutlets ()
		{
			if (DetailsTable != null) {
				DetailsTable.Dispose ();
				DetailsTable = null;
			}

			if (UpdatedAtLabel != null) {
				UpdatedAtLabel.Dispose ();
				UpdatedAtLabel = null;
			}

			if (ForcastLabel != null) {
				ForcastLabel.Dispose ();
				ForcastLabel = null;
			}

			if (IndoorTempLabel != null) {
				IndoorTempLabel.Dispose ();
				IndoorTempLabel = null;
			}

			if (MyLabel != null) {
				MyLabel.Dispose ();
				MyLabel = null;
			}

			if (OutsideTempLabel != null) {
				OutsideTempLabel.Dispose ();
				OutsideTempLabel = null;
			}

			if (TestButton != null) {
				TestButton.Dispose ();
				TestButton = null;
			}

			if (TheSwitch != null) {
				TheSwitch.Dispose ();
				TheSwitch = null;
			}
		}
	}
}
