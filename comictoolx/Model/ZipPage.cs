using System;
using Ionic.Zip;
using System.IO;

namespace comictoolx
{
	public class ZipPage : Page
	{
		private ZipEntry entry;
		
		public ZipPage (ZipEntry entry)
		{
			this.entry = entry;
		}
		
		public override byte[] ExtractPage ()
		{
			MemoryStream stream = new MemoryStream ();
			entry.Extract (stream);
			return stream.ToArray ();
		}
	}
}

