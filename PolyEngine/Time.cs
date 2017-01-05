using System;
namespace PolyEngine
{
	public class Time
	{


		#region Static Variables

		//captureFramerate Slows game playback time to allow screenshots to be saved between frames.
		//deltaTime The time in seconds it took to complete the last frame(Read Only).
		public static float deltaTime = 0;

		//fixedDeltaTime The interval in seconds at which physics and other fixed frame rate updates(like MonoBehaviour's FixedUpdate) are performed.
		public static float fixedDeltaTime = 1;

		//fixedTime   The time the latest FixedUpdate has started(Read Only).This is the time in seconds since the start of the game.
		public static float fixedTime = 0;

		//frameCount  The total number of frames that have passed(Read Only).
		//maximumDeltaTime    The maximum time a frame can take.Physics and other fixed frame rate updates(like MonoBehaviour's FixedUpdate).
		//realtimeSinceStartup    The real time in seconds since the game started(Read Only).
		public float realtimeSinceStartup
		{
			get
			{
				return realTime;
			}
		}

		//smoothDeltaTime A smoothed out Time.deltaTime(Read Only).
		//time    The time at the beginning of this frame(Read Only).This is the time in seconds since the start of the game
		public static float time = 0;
			
		//timeScale   The scale at which the time is passing.This can be used for slow motion effects.
		public static float timeScale = 1;

		//timeSinceLevelLoad  The time this frame has started(Read Only).This is the time in seconds since the last level has been loaded.
		//unscaledDeltaTime   The timeScale - independent time in seconds it took to complete the last frame(Read Only).
		//unscaledTime    The timeScale - independant time at the beginning of this frame(Read Only).This is the time in seconds since the start of the game.

		#endregion Static Variables


		internal static float realTime = 0;

	}
}

