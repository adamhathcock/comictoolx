using System;
using System.Drawing;
using System.IO;
using MonoMac.AppKit;
using MonoMac.Foundation;

namespace comictoolx
{
	public partial class MainWindowController : MonoMac.AppKit.NSWindowController
	{
		private Comic comic;

		#region Constructors

		// Called when created from unmanaged code
		public MainWindowController (IntPtr handle) : base(handle)
		{
			Initialize ();
		}

		// Called when created directly from a XIB file
		[Export("initWithCoder:")]
		public MainWindowController (NSCoder coder) : base(coder)
		{
			Initialize ();
		}

		// Call to load from the XIB/NIB file
		public MainWindowController () : base("MainWindow")
		{
			Initialize ();
		}

		// Shared initialization code
		void Initialize ()
		{
			ShouldCascadeWindows = false;
		}

		#endregion

		//strongly typed window accessor
		public new MainWindow Window {
			get { return (MainWindow)base.Window; }
		}

		internal MonoMac.ImageKit.IKImageView ImageView {
			get { return imageView; }
		}
		
		#region Open
		internal void OpenComic (AppDelegate d)
		{
			NSOpenPanel openPanel = new NSOpenPanel ();
			openPanel.AllowedFileTypes = new string[] { "cbr" };
			openPanel.BeginSheet (Window, new NSSavePanelComplete (r => TryLoad (d, openPanel.Url)));
		}

		private void TryLoad (AppDelegate d, NSUrl url)
		{
			if (url == null) {
				Window.Close ();
			} else {
				FileToLoad (url.Path);
				LoadCurrentEntry ();
				Window.MakeKeyAndOrderFront (this);
				d.AddWindowController (this);
			}
		}
		#endregion

		public void FileToLoad (string file)
		{
			switch (Path.GetExtension (file).ToLowerInvariant ()) {
			case ".cbr":
				
				{
					comic = new RarComic (file);
				}

				break;
			case ".cbz":
				
				
				{
					comic = new ZipComic (file);
				}

				break;
			default:
				
				{
					throw new InvalidOperationException ("Unknown extension on file: " + file);
				}

			}
			Window.Title = Path.GetFileName (file);
		}

		public void LoadCurrentEntry ()
		{
			var nsImage = new NSImage (NSData.FromArray (comic.CurrentPage.Bytes));
			var image = nsImage.AsCGImage (RectangleF.Empty, null, null);
			imageView.SetImageimageProperties (image, new NSDictionary ());
			
			imageView.ZoomImageToFit (this);
			CalculateAndSet();
			imageView.ZoomImageToFit (this);
		}
		
		public void CalculateAndSet()
		{
			PointF location = Window.Frame.Location;
			var rect = new RectangleF (location, imageView.ImageSize);
			Console.WriteLine(rect.Height +  " " + rect.Width);
			//Window.SetFrame (rect, true, true);
			float percent = Window.Frame.Height/rect.Height;
			Console.WriteLine(percent + " " + Window.Frame.Height + " " + rect.Height);
			rect = new RectangleF (location, new SizeF(rect.Width * percent, rect.Height * percent));
			Console.WriteLine(rect.Height +  " " + rect.Width);
			Window.SetFrame (rect, true, true);
		}

		public override void AwakeFromNib ()
		{
			base.AwakeFromNib ();
			if (comic != null) {
				LoadCurrentEntry ();
				imageView.ZoomImageToFit (this);
			} else {
				imageView.SetImageimageProperties (new NSImage ().AsCGImage (RectangleF.Empty, null, null), new NSDictionary ());
			}
			
		}

		public void MoveToNext ()
		{
			if (comic.MoveToNext ()) {
				LoadCurrentEntry ();
			}
		}

		public void MoveToPrevious ()
		{
			if (comic.MoveToPrevious ()) {
				LoadCurrentEntry ();
			}
		}
	}
}

