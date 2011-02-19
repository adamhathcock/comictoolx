using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace comictoolx
{
	public abstract class Comic : IDisposable
	{
		public Comic (string file)
		{
			FileName = file;
			Open (file);
			CurrentPage = Pages.First ();
		}

		public string FileName { get; private set; }

		public abstract void Open (string file);

		protected IList<Page> Pages { get; set; }

		public Page CurrentPage { get; protected set; }

		public bool MoveToNext ()
		{
			int index = Pages.IndexOf (CurrentPage);
			if (index < Pages.Count) {
				index++;
				CurrentPage = Pages[index];
				return true;
			}
			return false;
		}

		public bool MoveToPrevious ()
		{
			int index = Pages.IndexOf (CurrentPage);
			if (index > 0) {
				index--;
				CurrentPage = Pages[index];
				return true;
			}
			return false;
		}

		protected static bool IsValidImage (string file)
		{
			switch (Path.GetExtension (file).ToLowerInvariant ()) {
			case ".jpg":
			case ".jpeg":
			case ".gif":
			case ".png":
				return true;
			}
			return false;
		}
		
		public abstract void Dispose ();
	}	
}

