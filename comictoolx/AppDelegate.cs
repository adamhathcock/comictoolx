using System;
using System.Drawing;
using MonoMac.Foundation;
using MonoMac.AppKit;
using MonoMac.ObjCRuntime;
using MonoMac.ImageKit;
using MonoMac.CoreGraphics;
using System.Collections.Generic;
using System.Linq;

namespace comictoolx
{
	public partial class AppDelegate : NSApplicationDelegate
	{
		List<MainWindowController> controllers = new List<MainWindowController> ();

		public AppDelegate ()
		{
		}

		public override bool ApplicationShouldTerminateAfterLastWindowClosed (NSApplication sender)
		{
			return true;
		}

		partial void zoomIn (MonoMac.AppKit.NSMenuItem sender)
		{
			KeyController.ImageView.ZoomIn (this);
		}

		partial void zoomOut (MonoMac.AppKit.NSMenuItem sender)
		{
			KeyController.ImageView.ZoomOut (this);
		}

		partial void zoomActual (MonoMac.AppKit.NSMenuItem sender)
		{
			KeyController.ImageView.ZoomImageToActualSize (this);
		}

		partial void zoomFit (MonoMac.AppKit.NSMenuItem sender)
		{
			KeyController.ImageView.ZoomImageToFit (this);
		}

		partial void openComic (MonoMac.AppKit.NSMenuItem sender)
		{
			MainWindowController mwc = new MainWindowController ();
			NSOpenPanel openPanel = new NSOpenPanel ();
			openPanel.AllowedFileTypes = new string[] { "cbr" };
			openPanel.BeginSheet (mwc.Window, new NSSavePanelComplete (r => TryLoad (mwc, openPanel.Url)));
		}

		private void TryLoad (MainWindowController mwc, NSUrl url)
		{
			if (url == null) {
				mwc.Window.Close ();
			} else {
				mwc.FileToLoad (url.Path);
				mwc.LoadCurrentEntry ();
				mwc.Window.MakeKeyAndOrderFront (this);
				controllers.Add (mwc);
			}
		}

		partial void closeComic (MonoMac.AppKit.NSMenuItem sender)
		{
			var current = KeyController;
			controllers.Remove (current);
			current.Window.Close ();
		}

		private MainWindowController KeyController {
			get { return controllers.Where (c => c.Window.IsKeyWindow).Single (); }
		}

		/*partial void saveCurrentPage (MonoMac.AppKit.NSMenuItem sender)
		{
			NSSavePanel save = new NSSavePanel ();
			IKSaveOptions options = new IKSaveOptions ();
			options.AddSaveOptionsToPanel (save);
			
			save.BeginSheet (mainWindowController.Window, NSSavePanelComplete (r =>
			{
				save.Filename;
				options.ImageUTType;
				mainWindowController.ImageView.dra
			}));
		}*/

		partial void previousPage (MonoMac.AppKit.NSMenuItem sender)
		{
			KeyController.MoveToPrevious ();
		}

		partial void nextPage (MonoMac.AppKit.NSMenuItem sender)
		{
			KeyController.MoveToNext ();
		}
	}
}

