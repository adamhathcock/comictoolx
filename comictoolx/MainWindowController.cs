
using System;
using System.Collections.Generic;
using System.Linq;
using MonoMac.Foundation;
using MonoMac.AppKit;
using NUnrar;
using System.IO;
using MonoMac.CoreGraphics;
using System.Drawing;

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

		public void FileToLoad (string file)
		{
			comic = new RarComic (file);
			Window.Title = Path.GetFileName (file);
		}

		public void LoadCurrentEntry ()
		{
			var nsImage = new NSImage (NSData.FromArray (comic.CurrentPage.Bytes));
			var image = nsImage.AsCGImage (RectangleF.Empty, null, null);
			imageView.SetImageimageProperties (image, new NSDictionary ());
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

