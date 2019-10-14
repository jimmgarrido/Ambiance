// WARNING
//
// This file has been generated automatically by Visual Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using Foundation;
using System.CodeDom.Compiler;

namespace AmbianceExt
{
	[Register ("TodayViewController")]
	partial class TodayViewController
	{
		[Outlet]
		UIKit.UIView ChartContainer { get; set; }

		[Outlet]
		UIKit.UILabel ForcastHighLabel { get; set; }

		[Outlet]
		UIKit.UILabel ForcastLowLabel { get; set; }

		[Outlet]
		UIKit.UILabel HumidityLabel { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UIKit.UILabel InsideTempLabel { get; set; }

		[Outlet]
		UIKit.UIActivityIndicatorView LoadingIndicator { get; set; }

		[Outlet]
		UIKit.UIView MainInfoView { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UIKit.UILabel TempLabel { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UIKit.UILabel UpdatedLabel { get; set; }

		[Outlet]
		UIKit.UILabel UVLabel { get; set; }

		[Outlet]
		UIKit.UILabel WindSpeedLabel { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (MainInfoView != null) {
				MainInfoView.Dispose ();
				MainInfoView = null;
			}

			if (ChartContainer != null) {
				ChartContainer.Dispose ();
				ChartContainer = null;
			}

			if (ForcastHighLabel != null) {
				ForcastHighLabel.Dispose ();
				ForcastHighLabel = null;
			}

			if (ForcastLowLabel != null) {
				ForcastLowLabel.Dispose ();
				ForcastLowLabel = null;
			}

			if (HumidityLabel != null) {
				HumidityLabel.Dispose ();
				HumidityLabel = null;
			}

			if (LoadingIndicator != null) {
				LoadingIndicator.Dispose ();
				LoadingIndicator = null;
			}

			if (UVLabel != null) {
				UVLabel.Dispose ();
				UVLabel = null;
			}

			if (WindSpeedLabel != null) {
				WindSpeedLabel.Dispose ();
				WindSpeedLabel = null;
			}

			if (InsideTempLabel != null) {
				InsideTempLabel.Dispose ();
				InsideTempLabel = null;
			}

			if (TempLabel != null) {
				TempLabel.Dispose ();
				TempLabel = null;
			}

			if (UpdatedLabel != null) {
				UpdatedLabel.Dispose ();
				UpdatedLabel = null;
			}
		}
	}
}
