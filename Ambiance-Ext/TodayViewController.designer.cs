// WARNING
//
// This file has been generated automatically by Visual Studio from the outlets and
// actions declared in your storyboard file.
// Manual changes to this file will not be maintained.
//
using Foundation;
using System;
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
        UIKit.UIActivityIndicatorView LoadingIndicator { get; set; }


        [Outlet]
        UIKit.UILabel UVLabel { get; set; }


        [Outlet]
        UIKit.UILabel WindSpeedLabel { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel InsideTempLabel { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel TempLabel { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel UpdatedLabel { get; set; }

        void ReleaseDesignerOutlets ()
        {
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