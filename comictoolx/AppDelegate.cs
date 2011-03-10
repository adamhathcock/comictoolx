using System.Collections.Generic;
using System.Linq;
using MonoMac.AppKit;
using MonoMac.Foundation;
using System.Drawing;
using System;

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
			SizeF size = KeyController.Window.Frame.Size;
			KeyController.ImageView.ZoomIn (this);
			ResetWindowFromPreviousSize (size);
		}

		partial void zoomOut (MonoMac.AppKit.NSMenuItem sender)
		{
			SizeF size = KeyController.Window.Frame.Size;
			KeyController.ImageView.ZoomOut (this);
			ResetWindowFromPreviousSize (size);
		}
		
		private void ResetWindowFromPreviousSize (SizeF oldSize)
		{
			PointF oldLocation = KeyController.Window.Frame.Location;
			SizeF newSize =	oldSize;//new SizeF (oldSize.Width * ( 1- KeyController.ImageView.ZoomFactor), 				oldSize.Height * (1 - KeyController.ImageView.ZoomFactor));
			
			PointF newLocation = new PointF(oldLocation.X + (newSize.Width - oldSize.Width)/ 2, oldLocation.Y);
			
			Console.WriteLine ("oldSize " + oldSize);
			Console.WriteLine("oldLocation " + oldLocation);
			Console.WriteLine ("newSize " + newSize);
			Console.WriteLine ("newLocation " + newLocation);
			KeyController.Window.SetFrame (new RectangleF (newLocation, newSize), true, true);
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

