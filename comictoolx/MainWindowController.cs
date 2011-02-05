
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
		private RarArchive archive;
		private List<RarArchiveEntry> files;
		private RarArchiveEntry currentImageEntry;
		
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
		
		internal MonoMac.ImageKit.IKImageView ImageView
		{
			get 
			{
				return imageView;
			}
		}
		
		public void FileToLoad (string file)
		{
			archive = RarArchive.Open (file);
			files = archive.Entries.Where (e => e.FilePath.EndsWith ("jpg")).OrderBy (e => e.FilePath).ToList ();
			currentImageEntry = files.First ();
			Window.Title = Path.GetFileName (file);
		}
		
		public void LoadCurrentEntry ()
		{				
			MemoryStream ms = new MemoryStream ();
			currentImageEntry.WriteTo (ms);
			
			byte[] i = ms.ToArray ();
			
			var nsImage = new NSImage (NSData.FromArray (i));
			var image = nsImage.AsCGImage (RectangleF.Empty, null, null);
			imageView.SetImageimageProperties (image, new NSDictionary ());
		}

		public override void AwakeFromNib ()
		{
			base.AwakeFromNib ();
			if (currentImageEntry != null)
			{
				LoadCurrentEntry ();
				imageView.ZoomImageToFit (this);
			} else {
				imageView.SetImageimageProperties (new NSImage ().AsCGImage (RectangleF.Empty, null, null), new NSDictionary ());
			}
			
		}
		
		public void MoveToNext ()
		{
			int index = files.IndexOf (currentImageEntry);
			if (index < files.Count)
			{
				index++;
				currentImageEntry = files[index];
				LoadCurrentEntry ();
			}
		}
		
		public void MoveToPrevious ()
		{
			int index = files.IndexOf (currentImageEntry);
			if (index > 0) {
				index--;
				currentImageEntry = files[index];
				LoadCurrentEntry ();
			}
		}
	}
}

