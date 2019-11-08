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
	[Register ("DetailsRowController")]
	partial class DetailsRowController
	{
		[Outlet]
		public WatchKit.WKInterfaceLabel DetailLabel { get; set; }

		[Outlet]
		public WatchKit.WKInterfaceLabel TitleLabel { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (TitleLabel != null) {
				TitleLabel.Dispose ();
				TitleLabel = null;
			}

			if (DetailLabel != null) {
				DetailLabel.Dispose ();
				DetailLabel = null;
			}
		}
	}
}
