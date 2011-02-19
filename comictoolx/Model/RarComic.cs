using System;
using NUnrar;
using System.Linq;

namespace comictoolx
{
	public class RarComic : Comic
	{
		private RarArchive archive;
		
		public RarComic (string file) : base(file)
		{
		}

		public override void Open (string file)
		{
			archive = RarArchive.Open (file);
			Pages = archive.Entries.Where (e => IsValidImage (e.FilePath)).OrderBy (ex => ex.FilePath).Select (f => new RarPage (f)).ToList<Page> ();
		}
		
		public override void Dispose ()
		{			
		}
	}
}

