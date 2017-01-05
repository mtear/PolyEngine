using System;
namespace PolyEngine
{
	public class Screen
	{


		#region Static Variables

		//autorotateToLandscapeLeft Allow auto-rotation to landscape left?
		//autorotateToLandscapeRight Allow auto-rotation to landscape right?
		//autorotateToPortrait Allow auto-rotation to portrait?
		//autorotateToPortraitUpsideDown  Allow auto-rotation to portrait, upside down?
		//currentResolution The current screen resolution(Read Only).
		public static Resolution currentResolution
		{
			get
			{
				return mCurrentResolution;
			}
		}

		//dpi The current DPI of the screen / device(Read Only).
		//fullScreen Is the game running fullscreen?
		//height The current height of the screen window in pixels(Read Only).
		public static int height
		{
			get
			{
				return mCurrentResolution.height;
			}
		}

		//orientation Specifies logical orientation of the screen.
		//resolutions All fullscreen resolutions supported by the monitor(Read Only).
		//sleepTimeout A power saving setting, allowing the screen to dim some time after the last active user interaction.
		//width The current width of the screen window in pixels(Read Only).
		public static int width
		{
			get
			{
				return mCurrentResolution.width;
			}
		}

		#endregion Static Variables


		#region Private Static Variables

		private static Resolution mCurrentResolution;

		#endregion Private Static Variables


		#region Static Methods



		#endregion Static Methods


		#region Internal Methods

		internal static void SetCurrentResolution(Resolution r)
		{
			mCurrentResolution = r;
		}

		#endregion Internal Methods


	}
}

