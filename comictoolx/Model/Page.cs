using System;

namespace comictoolx
{
	public abstract class Page
	{
		private byte[] bytes;
		public byte[] Bytes {
			get {
				if (bytes == null) {
					bytes = ExtractPage ();
				}
				return bytes;
			}
		}

		public bool IsExtracted {
			get { return bytes != null; }
		}

		public abstract byte[] ExtractPage ();
	}
}

