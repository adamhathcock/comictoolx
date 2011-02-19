using System.Linq;
using Ionic.Zip;

namespace comictoolx
{
	public class ZipComic : Comic
	{
		private ZipFile zf;
		
		public ZipComic (string file)
			: base(file)
		{
		}
		
		public override void Open (string file)
		{
			zf = ZipFile.Read (file);
			Pages = zf.Where (entry => (!entry.IsDirectory && IsValidImage (entry.FileName)))
				.OrderBy (e => e.FileName)
				.Select (e => new ZipPage (e)).ToList<Page> ();
		}
		
		public override void Dispose ()
		{
			zf.Dispose ();
		}
	}
}

