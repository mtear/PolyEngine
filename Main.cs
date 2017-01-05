#region Using Statements
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using MonoMac.AppKit;
using MonoMac.Foundation;
using PolyEngine;
#endregion

namespace TestXNA4
{
	static class Program
	{
		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		static void Main(string[] args)
		{
			NSApplication.Init();

			using (var p = new NSAutoreleasePool())
			{
				NSApplication.SharedApplication.Delegate = new AppDelegate();
				NSApplication.Main(args);
			}
		}
	}

	class AppDelegate : NSApplicationDelegate
	{
		//private static Game1 game;
		private static Game game;

		public override void FinishedLaunching(NSObject notification)
		{
			game = new XNABridge();
			game.Run();
		}

		public override bool ApplicationShouldTerminateAfterLastWindowClosed(NSApplication sender)
		{
			return true;
		}
	}

}


