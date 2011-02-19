using System.IO;
using NUnrar;

namespace comictoolx
{
	public class RarPage : Page
	{
		private RarArchiveEntry entry;

		public RarPage (RarArchiveEntry entry)
		{
			this.entry = entry;
		}

		public override byte[] ExtractPage ()
		{
			MemoryStream ms = new MemoryStream ();
			entry.WriteTo (ms);
			return ms.ToArray ();
		}
	}
}

