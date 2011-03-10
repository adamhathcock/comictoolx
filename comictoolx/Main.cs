using System;
using System.Drawing;
using MonoMac.Foundation;
using MonoMac.AppKit;
using MonoMac.ObjCRuntime;

namespace comictoolx
{
	class MainClass
	{
		static void Main (string[] args)
		{
			args.ForEach( a => NSAlert.WithMessage(a, "OK", null, null, null));
			NSApplication.Init ();
			NSApplication.Main (args);
		}
	}
}

