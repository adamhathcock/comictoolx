using System.Collections.Generic;
using System.Linq;
using MonoMac.AppKit;
using MonoMac.Foundation;

namespace comictoolx
{
	public partial class AppDelegate : NSApplicationDelegate
	{
		private List<MainWindowController> controllers = new List<MainWindowController> ();

		public AppDelegate ()
		{
		}

		public override bool ApplicationShouldTerminateAfterLastWindowClosed (NSApplication sender)
		{
			return true;
		}
		
		public override void FinishedLaunching (NSObject notification)
		{
			MainWindowController mwc = new MainWindowController ();
			mwc.OpenComic (this);
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
			mwc.OpenComic (this);
		}

		internal void AddWindowController (MainWindowController mwc)
		{
			controllers.Add (mwc);
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

