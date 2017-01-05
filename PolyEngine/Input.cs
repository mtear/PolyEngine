using System.Collections.Generic;
using Microsoft.Xna.Framework.Input;

namespace PolyEngine
{
	public class Input
	{


		#region Static Variables

		//acceleration Last measured linear acceleration of a device in three-dimensional space. (Read Only)
		//accelerationEventCount Number of acceleration measurements which occurred during last frame.
		//accelerationEvents  Returns list of acceleration measurements which occurred during the last frame. (Read Only) (Allocates temporary variables).
		//anyKey Is any key or mouse button currently held down? (Read Only)
		public static bool anyKey
		{
			get
			{
				return keyHolds.Count > 0;
			}
		}

		//anyKeyDown Returns true the first frame the user hits any key or mouse button. (Read Only)
		public static bool anyKeyDown
		{
			get
			{
				return keyDowns.Count > 0;
			}
		}

		//backButtonLeavesApp Should Back button quit the application? Only usable on Android, Windows Phone or Windows Tablets.
		//compass Property for accessing compass(handheld devices only). (Read Only)
		//compensateSensors This property controls if input sensors should be compensated for screen orientation.
		//compositionCursorPos    The current text input position used by IMEs to open windows.
		//compositionString The current IME composition string being typed by the user.
		//deviceOrientation Device physical orientation as reported by OS. (Read Only)
		//gyro Returns default gyroscope.
		//imeCompositionMode Controls enabling and disabling of IME input composition.
		//imeIsSelected Does the user have an IME keyboard input source selected?
		//inputString Returns the keyboard input entered this frame. (Read Only)
		//location Property for accessing device location (handheld devices only). (Read Only)
		//mousePosition The current mouse position in pixel coordinates. (Read Only)
		public static Vector3 mousePosition
		{
			get
			{
				return mMousePos;
			}
		}

		//mousePresent Indicates if a mouse device is detected.
		//mouseScrollDelta The current mouse scroll delta. (Read Only)
		//multiTouchEnabled Property indicating whether the system handles multiple touches.
		//simulateMouseWithTouches Enables/Disables mouse simulation with touches. By default this option is enabled.
		//stylusTouchSupported Returns true when Stylus Touch is supported by a device or platform.
		//touchCount Number of touches. Guaranteed not to change throughout the frame. (Read Only)
		//touches Returns list of objects representing status of all touches during last frame. (Read Only) (Allocates temporary variables).
		//touchPressureSupported Bool value which let's users check if touch pressure is supported.
		//touchSupported Returns whether the device on which application is currently running supports touch input.

		#endregion Static Variables


		#region Private Static Variables

		private static KeyboardState xnaKeyboard = Keyboard.GetState();
		private static GamePadState xnaGamepad1 = GamePad.GetState(Microsoft.Xna.Framework.PlayerIndex.One);
		private static GamePadState xnaGamepad2 = GamePad.GetState(Microsoft.Xna.Framework.PlayerIndex.Two);
		private static GamePadState xnaGamepad3 = GamePad.GetState(Microsoft.Xna.Framework.PlayerIndex.Three);
		private static GamePadState xnaGamepad4 = GamePad.GetState(Microsoft.Xna.Framework.PlayerIndex.Four);
		private static MouseState xnaMouse = Mouse.GetState();

		private static List<KeyCode> keyDowns = new List<KeyCode>();
		private static List<KeyCode> keyUps = new List<KeyCode>();
		private static List<KeyCode> keyHolds = new List<KeyCode>();

		private static Vector3 mMousePos = Vector3.zero;

		#endregion Private Static Variables


		#region Static Methods

		//GetAccelerationEvent Returns specific acceleration measurement which occurred during last frame. (Does not allocate temporary variables).
		//GetAxis Returns the value of the virtual axis identified by axisName.
		public static float GetAxis(string name)
		{
			if (name == "Horizontal")
				return xnaGamepad1.ThumbSticks.Left.X;
			else if (name == "Vertical")
				return xnaGamepad1.ThumbSticks.Left.Y;
			else if (name == "Right Horizontal")
				return xnaGamepad1.ThumbSticks.Right.X;
			else if (name == "Right Vertical")
				return xnaGamepad1.ThumbSticks.Right.Y;
			else return 0;
		}

		//GetAxisRaw  Returns the value of the virtual axis identified by axisName with no smoothing filtering applied.
		//GetButton Returns true while the virtual button identified by buttonName is held down.
		//GetButtonDown   Returns true during the frame the user pressed down the virtual button identified by buttonName.
		//GetButtonUp Returns true the first frame the user releases the virtual button identified by buttonName.
		//GetJoystickNames    Returns an array of strings describing the connected joysticks.
		//GetKey Returns true while the user holds down the key identified by name.Think auto fire.
		public static bool GetKey(string key)
		{
			return GetKey(StringtoKeyCode(key));
		}

		public static bool GetKey(KeyCode key)
		{
			return keyHolds.Contains(key);
		}

		//GetKeyDown Returns true during the frame the user starts pressing down the key identified by name.
		public static bool GetKeyDown(string key)
		{
			return GetKeyDown(StringtoKeyCode(key));
		}

		public static bool GetKeyDown(KeyCode key)
		{
			return keyDowns.Contains(key);
		}

		//GetKeyUp Returns true during the frame the user releases the key identified by name.
		public static bool GetKeyUp(string key)
		{
			return GetKeyUp(StringtoKeyCode(key));
		}

		public static bool GetKeyUp(KeyCode key)
		{
			return keyUps.Contains(key);
		}

		//GetMouseButton Returns whether the given mouse button is held down.
		public static bool GetMouseButton(int button)
		{
			if (button == 0) return GetKey(KeyCode.Mouse0);
			if (button == 1) return GetKey(KeyCode.Mouse1);
			if (button == 2) return GetKey(KeyCode.Mouse2);
			if (button == 3) return GetKey(KeyCode.Mouse3);
			if (button == 4) return GetKey(KeyCode.Mouse4);
			return false;
		}

		//GetMouseButtonDown Returns true during the frame the user pressed the given mouse button.
		public static bool GetMouseButtonDown(int button)
		{
			if (button == 0) return GetKeyDown(KeyCode.Mouse0);
			if (button == 1) return GetKeyDown(KeyCode.Mouse1);
			if (button == 2) return GetKeyDown(KeyCode.Mouse2);
			if (button == 3) return GetKeyDown(KeyCode.Mouse3);
			if (button == 4) return GetKeyDown(KeyCode.Mouse4);
			return false;
		}

		//GetMouseButtonUp Returns true during the frame the user releases the given mouse button.
		public static bool GetMouseButtonUp(int button)
		{
			if (button == 0) return GetKeyUp(KeyCode.Mouse0);
			if (button == 1) return GetKeyUp(KeyCode.Mouse1);
			if (button == 2) return GetKeyUp(KeyCode.Mouse2);
			if (button == 3) return GetKeyUp(KeyCode.Mouse3);
			if (button == 4) return GetKeyUp(KeyCode.Mouse4);
			return false;
		}

		//GetTouch Returns object representing status of a specific touch. (Does not allocate temporary variables).
		//IsJoystickPreconfigured Determine whether a particular joystick model has been preconfigured by Unity. (Linux-only).
		//ResetInputAxes Resets all input.After ResetInputAxes all axes return to 0 and all buttons return to 0 for one frame.

		#endregion Static Methods


		#region Internal Methods

		internal static void UpdateInputs()
		{
			keyDowns.Clear();
			keyUps.Clear();
			keyHolds.Clear();

			UpdateKeyboard();
			UpdateGamepads();
			UpdateMouse();
		}

		internal static void UpdateGamepads()
		{//TODO isconnected
			UpdateGamepad(xnaGamepad1, 1);
			UpdateGamepad(xnaGamepad2, 2);
			UpdateGamepad(xnaGamepad3, 3);
			UpdateGamepad(xnaGamepad4, 4);

			for (int i = 0; i <= 15; i++)
			{
				for (int g = 1; g <= 4; g++)
				{
					KeyCode k = IntButtonToKeyCode(g, i);
					if (keyDowns.Contains(k)) keyDowns.Add(IntButtonToKeyCode(5, i));
					if (keyUps.Contains(k)) keyUps.Add(IntButtonToKeyCode(5, i));
					if (keyHolds.Contains(k)) keyHolds.Add(IntButtonToKeyCode(5, i));
				}
			}
		}

		internal static void UpdateGamepad(GamePadState mState, int pad)
		{
			GamePadState state = GamePad.GetState(Microsoft.Xna.Framework.PlayerIndex.One);
			if(pad == 2) state = GamePad.GetState(Microsoft.Xna.Framework.PlayerIndex.Two);
			else if (pad == 3) state = GamePad.GetState(Microsoft.Xna.Framework.PlayerIndex.Three);
			else if (pad == 4) state = GamePad.GetState(Microsoft.Xna.Framework.PlayerIndex.Four);

			if (state.Buttons.A == ButtonState.Pressed && mState.Buttons.A == ButtonState.Released) keyDowns.Add(IntButtonToKeyCode(pad, 0));
			else if (state.Buttons.A == ButtonState.Released && mState.Buttons.A == ButtonState.Pressed) keyUps.Add(IntButtonToKeyCode(pad, 0));
			else if (state.Buttons.A == ButtonState.Pressed && mState.Buttons.A == ButtonState.Pressed) keyHolds.Add(IntButtonToKeyCode(pad, 0));

			if (state.Buttons.B == ButtonState.Pressed && mState.Buttons.B == ButtonState.Released) keyDowns.Add(IntButtonToKeyCode(pad, 1));
			else if (state.Buttons.B == ButtonState.Released && mState.Buttons.B == ButtonState.Pressed) keyUps.Add(IntButtonToKeyCode(pad, 1));
			else if (state.Buttons.B == ButtonState.Pressed && mState.Buttons.B == ButtonState.Pressed) keyHolds.Add(IntButtonToKeyCode(pad, 1));

			if (state.Buttons.X == ButtonState.Pressed && mState.Buttons.X == ButtonState.Released) keyDowns.Add(IntButtonToKeyCode(pad, 2));
			else if (state.Buttons.X == ButtonState.Released && mState.Buttons.X == ButtonState.Pressed) keyUps.Add(IntButtonToKeyCode(pad, 2));
			else if (state.Buttons.X == ButtonState.Pressed && mState.Buttons.X == ButtonState.Pressed) keyHolds.Add(IntButtonToKeyCode(pad, 2));

			if (state.Buttons.Y == ButtonState.Pressed && mState.Buttons.Y == ButtonState.Released) keyDowns.Add(IntButtonToKeyCode(pad, 3));
			else if (state.Buttons.Y == ButtonState.Released && mState.Buttons.Y == ButtonState.Pressed) keyUps.Add(IntButtonToKeyCode(pad, 3));
			else if (state.Buttons.Y == ButtonState.Pressed && mState.Buttons.Y == ButtonState.Pressed) keyHolds.Add(IntButtonToKeyCode(pad, 3));

			if (state.Buttons.LeftShoulder == ButtonState.Pressed && mState.Buttons.LeftShoulder == ButtonState.Released) keyDowns.Add(IntButtonToKeyCode(pad, 4));
			else if (state.Buttons.LeftShoulder == ButtonState.Released && mState.Buttons.LeftShoulder == ButtonState.Pressed) keyUps.Add(IntButtonToKeyCode(pad, 4));
			else if (state.Buttons.LeftShoulder == ButtonState.Pressed && mState.Buttons.LeftShoulder == ButtonState.Pressed) keyHolds.Add(IntButtonToKeyCode(pad, 4));

			if (state.Buttons.RightShoulder == ButtonState.Pressed && mState.Buttons.RightShoulder == ButtonState.Released) keyDowns.Add(IntButtonToKeyCode(pad, 5));
			else if (state.Buttons.RightShoulder == ButtonState.Released && mState.Buttons.RightShoulder == ButtonState.Pressed) keyUps.Add(IntButtonToKeyCode(pad, 5));
			else if (state.Buttons.RightShoulder == ButtonState.Pressed && mState.Buttons.RightShoulder == ButtonState.Pressed) keyHolds.Add(IntButtonToKeyCode(pad, 5));

			if (state.Buttons.Back == ButtonState.Pressed && mState.Buttons.Back == ButtonState.Released) keyDowns.Add(IntButtonToKeyCode(pad, 6));
			else if (state.Buttons.Back == ButtonState.Released && mState.Buttons.Back == ButtonState.Pressed) keyUps.Add(IntButtonToKeyCode(pad, 6));
			else if (state.Buttons.Back == ButtonState.Pressed && mState.Buttons.Back == ButtonState.Pressed) keyHolds.Add(IntButtonToKeyCode(pad, 6));

			if (state.Buttons.Start == ButtonState.Pressed && mState.Buttons.Start == ButtonState.Released) keyDowns.Add(IntButtonToKeyCode(pad, 7));
			else if (state.Buttons.Start == ButtonState.Released && mState.Buttons.Start == ButtonState.Pressed) keyUps.Add(IntButtonToKeyCode(pad, 7));
			else if (state.Buttons.Start == ButtonState.Pressed && mState.Buttons.Start == ButtonState.Pressed) keyHolds.Add(IntButtonToKeyCode(pad, 7));

			if (state.Buttons.LeftStick == ButtonState.Pressed && mState.Buttons.LeftStick == ButtonState.Released) keyDowns.Add(IntButtonToKeyCode(pad, 8));
			else if (state.Buttons.LeftStick == ButtonState.Released && mState.Buttons.LeftStick == ButtonState.Pressed) keyUps.Add(IntButtonToKeyCode(pad, 8));
			else if (state.Buttons.LeftStick == ButtonState.Pressed && mState.Buttons.LeftStick == ButtonState.Pressed) keyHolds.Add(IntButtonToKeyCode(pad, 8));

			if (state.Buttons.RightStick == ButtonState.Pressed && mState.Buttons.RightStick == ButtonState.Released) keyDowns.Add(IntButtonToKeyCode(pad, 9));
			else if (state.Buttons.RightStick == ButtonState.Released && mState.Buttons.RightStick == ButtonState.Pressed) keyUps.Add(IntButtonToKeyCode(pad, 9));
			else if (state.Buttons.RightStick == ButtonState.Pressed && mState.Buttons.RightStick == ButtonState.Pressed) keyHolds.Add(IntButtonToKeyCode(pad, 9));

			if (state.DPad.Up == ButtonState.Pressed && mState.DPad.Up == ButtonState.Released) keyDowns.Add(IntButtonToKeyCode(pad, 10));
			else if (state.DPad.Up == ButtonState.Released && mState.DPad.Up == ButtonState.Pressed) keyUps.Add(IntButtonToKeyCode(pad, 10));
			else if (state.DPad.Up == ButtonState.Pressed && mState.DPad.Up == ButtonState.Pressed) keyHolds.Add(IntButtonToKeyCode(pad, 10));

			if (state.DPad.Down == ButtonState.Pressed && mState.DPad.Down == ButtonState.Released) keyDowns.Add(IntButtonToKeyCode(pad, 11));
			else if (state.DPad.Down == ButtonState.Released && mState.DPad.Down == ButtonState.Pressed) keyUps.Add(IntButtonToKeyCode(pad, 11));
			else if (state.DPad.Down == ButtonState.Pressed && mState.DPad.Down == ButtonState.Pressed) keyHolds.Add(IntButtonToKeyCode(pad, 11));

			if (state.DPad.Left == ButtonState.Pressed && mState.DPad.Left == ButtonState.Released) keyDowns.Add(IntButtonToKeyCode(pad, 12));
			else if (state.DPad.Left == ButtonState.Released && mState.DPad.Left == ButtonState.Pressed) keyUps.Add(IntButtonToKeyCode(pad, 12));
			else if (state.DPad.Left == ButtonState.Pressed && mState.DPad.Left == ButtonState.Pressed) keyHolds.Add(IntButtonToKeyCode(pad, 12));

			if (state.DPad.Right == ButtonState.Pressed && mState.DPad.Right == ButtonState.Released) keyDowns.Add(IntButtonToKeyCode(pad, 13));
			else if (state.DPad.Right == ButtonState.Released && mState.DPad.Right == ButtonState.Pressed) keyUps.Add(IntButtonToKeyCode(pad, 13));
			else if (state.DPad.Right == ButtonState.Pressed && mState.DPad.Right == ButtonState.Pressed) keyHolds.Add(IntButtonToKeyCode(pad, 13));

			if (state.Buttons.BigButton == ButtonState.Pressed && mState.Buttons.BigButton == ButtonState.Released) keyDowns.Add(IntButtonToKeyCode(pad, 14));
			else if (state.Buttons.BigButton == ButtonState.Released && mState.Buttons.BigButton == ButtonState.Pressed) keyUps.Add(IntButtonToKeyCode(pad, 14));
			else if (state.Buttons.BigButton == ButtonState.Pressed && mState.Buttons.BigButton == ButtonState.Pressed) keyHolds.Add(IntButtonToKeyCode(pad, 14));

			if (pad == 1) xnaGamepad1 = state;
			else if (pad == 2) xnaGamepad2 = state;
			else if (pad == 3) xnaGamepad3 = state;
			else if (pad == 4) xnaGamepad4 = state;
		}

		internal static KeyCode IntButtonToKeyCode(int pad, int button)
		{
			switch (pad)
			{
				case 1: 
					switch (button)
					{
						case 0: return KeyCode.Joystick1Button0;
						case 1: return KeyCode.Joystick1Button1;
						case 2: return KeyCode.Joystick1Button2;
						case 3: return KeyCode.Joystick1Button3;
						case 4: return KeyCode.Joystick1Button4;
						case 5: return KeyCode.Joystick1Button5;
						case 6: return KeyCode.Joystick1Button6;
						case 7: return KeyCode.Joystick1Button7;
						case 8: return KeyCode.Joystick1Button8;
						case 9: return KeyCode.Joystick1Button9;
						case 10: return KeyCode.Joystick1Button10;
						case 11: return KeyCode.Joystick1Button11;
						case 12: return KeyCode.Joystick1Button12;
						case 13: return KeyCode.Joystick1Button13;
						case 14: return KeyCode.Joystick1Button14;
						case 15: return KeyCode.Joystick1Button15;
					}
					break;
				case 2:
					switch (button)
					{
						case 0: return KeyCode.Joystick2Button0;
						case 1: return KeyCode.Joystick2Button1;
						case 2: return KeyCode.Joystick2Button2;
						case 3: return KeyCode.Joystick2Button3;
						case 4: return KeyCode.Joystick2Button4;
						case 5: return KeyCode.Joystick2Button5;
						case 6: return KeyCode.Joystick2Button6;
						case 7: return KeyCode.Joystick2Button7;
						case 8: return KeyCode.Joystick2Button8;
						case 9: return KeyCode.Joystick2Button9;
						case 10: return KeyCode.Joystick2Button10;
						case 11: return KeyCode.Joystick2Button11;
						case 12: return KeyCode.Joystick2Button12;
						case 13: return KeyCode.Joystick2Button13;
						case 14: return KeyCode.Joystick2Button14;
						case 15: return KeyCode.Joystick2Button15;
					}
					break;
				case 3:
					switch (button)
					{
						case 0: return KeyCode.Joystick3Button0;
						case 1: return KeyCode.Joystick3Button1;
						case 2: return KeyCode.Joystick3Button2;
						case 3: return KeyCode.Joystick3Button3;
						case 4: return KeyCode.Joystick3Button4;
						case 5: return KeyCode.Joystick3Button5;
						case 6: return KeyCode.Joystick3Button6;
						case 7: return KeyCode.Joystick3Button7;
						case 8: return KeyCode.Joystick3Button8;
						case 9: return KeyCode.Joystick3Button9;
						case 10: return KeyCode.Joystick3Button10;
						case 11: return KeyCode.Joystick3Button11;
						case 12: return KeyCode.Joystick3Button12;
						case 13: return KeyCode.Joystick3Button13;
						case 14: return KeyCode.Joystick3Button14;
						case 15: return KeyCode.Joystick3Button15;
					}
					break;
				case 4:
					switch (button)
					{
						case 0: return KeyCode.Joystick4Button0;
						case 1: return KeyCode.Joystick4Button1;
						case 2: return KeyCode.Joystick4Button2;
						case 3: return KeyCode.Joystick4Button3;
						case 4: return KeyCode.Joystick4Button4;
						case 5: return KeyCode.Joystick4Button5;
						case 6: return KeyCode.Joystick4Button6;
						case 7: return KeyCode.Joystick4Button7;
						case 8: return KeyCode.Joystick4Button8;
						case 9: return KeyCode.Joystick4Button9;
						case 10: return KeyCode.Joystick4Button10;
						case 11: return KeyCode.Joystick4Button11;
						case 12: return KeyCode.Joystick4Button12;
						case 13: return KeyCode.Joystick4Button13;
						case 14: return KeyCode.Joystick4Button14;
						case 15: return KeyCode.Joystick4Button15;
					}
					break;
				case 5:
					switch (button)
					{
						case 0: return KeyCode.JoystickButton0;
						case 1: return KeyCode.JoystickButton1;
						case 2: return KeyCode.JoystickButton2;
						case 3: return KeyCode.JoystickButton3;
						case 4: return KeyCode.JoystickButton4;
						case 5: return KeyCode.JoystickButton5;
						case 6: return KeyCode.JoystickButton6;
						case 7: return KeyCode.JoystickButton7;
						case 8: return KeyCode.JoystickButton8;
						case 9: return KeyCode.JoystickButton9;
						case 10: return KeyCode.JoystickButton10;
						case 11: return KeyCode.JoystickButton11;
						case 12: return KeyCode.JoystickButton12;
						case 13: return KeyCode.JoystickButton13;
						case 14: return KeyCode.JoystickButton14;
						case 15: return KeyCode.JoystickButton15;
					}
					break;
			}
			return KeyCode.None;
		}

		internal static void UpdateKeyboard()
		{
			KeyboardState state = Keyboard.GetState();

			//Letters

			if (state.IsKeyDown(Keys.A) && !xnaKeyboard.IsKeyDown(Keys.A)) keyDowns.Add(KeyCode.A);
			else if (!state.IsKeyDown(Keys.A) && xnaKeyboard.IsKeyDown(Keys.A)) keyUps.Add(KeyCode.A);
			else if (state.IsKeyDown(Keys.A) && xnaKeyboard.IsKeyDown(Keys.A)) keyHolds.Add(KeyCode.A);

			if (state.IsKeyDown(Keys.B) && !xnaKeyboard.IsKeyDown(Keys.B)) keyDowns.Add(KeyCode.B);
			else if (!state.IsKeyDown(Keys.B) && xnaKeyboard.IsKeyDown(Keys.B)) keyUps.Add(KeyCode.B);
			else if (state.IsKeyDown(Keys.B) && xnaKeyboard.IsKeyDown(Keys.B)) keyHolds.Add(KeyCode.B);

			if (state.IsKeyDown(Keys.C) && !xnaKeyboard.IsKeyDown(Keys.C)) keyDowns.Add(KeyCode.C);
			else if (!state.IsKeyDown(Keys.C) && xnaKeyboard.IsKeyDown(Keys.C)) keyUps.Add(KeyCode.C);
			else if (state.IsKeyDown(Keys.C) && xnaKeyboard.IsKeyDown(Keys.C)) keyHolds.Add(KeyCode.C);

			if (state.IsKeyDown(Keys.D) && !xnaKeyboard.IsKeyDown(Keys.D)) keyDowns.Add(KeyCode.D);
			else if (!state.IsKeyDown(Keys.D) && xnaKeyboard.IsKeyDown(Keys.D)) keyUps.Add(KeyCode.D);
			else if (state.IsKeyDown(Keys.D) && xnaKeyboard.IsKeyDown(Keys.D)) keyHolds.Add(KeyCode.D);

			if (state.IsKeyDown(Keys.E) && !xnaKeyboard.IsKeyDown(Keys.E)) keyDowns.Add(KeyCode.E);
			else if (!state.IsKeyDown(Keys.E) && xnaKeyboard.IsKeyDown(Keys.E)) keyUps.Add(KeyCode.E);
			else if (state.IsKeyDown(Keys.E) && xnaKeyboard.IsKeyDown(Keys.E)) keyHolds.Add(KeyCode.E);

			if (state.IsKeyDown(Keys.F) && !xnaKeyboard.IsKeyDown(Keys.F)) keyDowns.Add(KeyCode.F);
			else if (!state.IsKeyDown(Keys.F) && xnaKeyboard.IsKeyDown(Keys.F)) keyUps.Add(KeyCode.F);
			else if (state.IsKeyDown(Keys.F) && xnaKeyboard.IsKeyDown(Keys.F)) keyHolds.Add(KeyCode.F);

			if (state.IsKeyDown(Keys.G) && !xnaKeyboard.IsKeyDown(Keys.G)) keyDowns.Add(KeyCode.G);
			else if (!state.IsKeyDown(Keys.G) && xnaKeyboard.IsKeyDown(Keys.G)) keyUps.Add(KeyCode.G);
			else if (state.IsKeyDown(Keys.G) && xnaKeyboard.IsKeyDown(Keys.G)) keyHolds.Add(KeyCode.G);

			if (state.IsKeyDown(Keys.H) && !xnaKeyboard.IsKeyDown(Keys.H)) keyDowns.Add(KeyCode.H);
			else if (!state.IsKeyDown(Keys.H) && xnaKeyboard.IsKeyDown(Keys.H)) keyUps.Add(KeyCode.H);
			else if (state.IsKeyDown(Keys.H) && xnaKeyboard.IsKeyDown(Keys.H)) keyHolds.Add(KeyCode.H);

			if (state.IsKeyDown(Keys.I) && !xnaKeyboard.IsKeyDown(Keys.I)) keyDowns.Add(KeyCode.I);
			else if (!state.IsKeyDown(Keys.I) && xnaKeyboard.IsKeyDown(Keys.I)) keyUps.Add(KeyCode.I);
			else if (state.IsKeyDown(Keys.I) && xnaKeyboard.IsKeyDown(Keys.I)) keyHolds.Add(KeyCode.I);

			if (state.IsKeyDown(Keys.J) && !xnaKeyboard.IsKeyDown(Keys.J)) keyDowns.Add(KeyCode.J);
			else if (!state.IsKeyDown(Keys.J) && xnaKeyboard.IsKeyDown(Keys.J)) keyUps.Add(KeyCode.J);
			else if (state.IsKeyDown(Keys.J) && xnaKeyboard.IsKeyDown(Keys.J)) keyHolds.Add(KeyCode.J);

			if (state.IsKeyDown(Keys.K) && !xnaKeyboard.IsKeyDown(Keys.K)) keyDowns.Add(KeyCode.K);
			else if (!state.IsKeyDown(Keys.K) && xnaKeyboard.IsKeyDown(Keys.K)) keyUps.Add(KeyCode.K);
			else if (state.IsKeyDown(Keys.K) && xnaKeyboard.IsKeyDown(Keys.K)) keyHolds.Add(KeyCode.K);

			if (state.IsKeyDown(Keys.L) && !xnaKeyboard.IsKeyDown(Keys.L)) keyDowns.Add(KeyCode.L);
			else if (!state.IsKeyDown(Keys.L) && xnaKeyboard.IsKeyDown(Keys.L)) keyUps.Add(KeyCode.L);
			else if (state.IsKeyDown(Keys.L) && xnaKeyboard.IsKeyDown(Keys.L)) keyHolds.Add(KeyCode.L);

			if (state.IsKeyDown(Keys.M) && !xnaKeyboard.IsKeyDown(Keys.M)) keyDowns.Add(KeyCode.M);
			else if (!state.IsKeyDown(Keys.M) && xnaKeyboard.IsKeyDown(Keys.M)) keyUps.Add(KeyCode.M);
			else if (state.IsKeyDown(Keys.M) && xnaKeyboard.IsKeyDown(Keys.M)) keyHolds.Add(KeyCode.M);

			if (state.IsKeyDown(Keys.N) && !xnaKeyboard.IsKeyDown(Keys.N)) keyDowns.Add(KeyCode.N);
			else if (!state.IsKeyDown(Keys.N) && xnaKeyboard.IsKeyDown(Keys.N)) keyUps.Add(KeyCode.N);
			else if (state.IsKeyDown(Keys.N) && xnaKeyboard.IsKeyDown(Keys.N)) keyHolds.Add(KeyCode.N);

			if (state.IsKeyDown(Keys.O) && !xnaKeyboard.IsKeyDown(Keys.O)) keyDowns.Add(KeyCode.O);
			else if (!state.IsKeyDown(Keys.O) && xnaKeyboard.IsKeyDown(Keys.O)) keyUps.Add(KeyCode.O);
			else if (state.IsKeyDown(Keys.O) && xnaKeyboard.IsKeyDown(Keys.O)) keyHolds.Add(KeyCode.O);

			if (state.IsKeyDown(Keys.P) && !xnaKeyboard.IsKeyDown(Keys.P)) keyDowns.Add(KeyCode.P);
			else if (!state.IsKeyDown(Keys.P) && xnaKeyboard.IsKeyDown(Keys.P)) keyUps.Add(KeyCode.P);
			else if (state.IsKeyDown(Keys.P) && xnaKeyboard.IsKeyDown(Keys.P)) keyHolds.Add(KeyCode.P);

			if (state.IsKeyDown(Keys.Q) && !xnaKeyboard.IsKeyDown(Keys.Q)) keyDowns.Add(KeyCode.Q);
			else if (!state.IsKeyDown(Keys.Q) && xnaKeyboard.IsKeyDown(Keys.Q)) keyUps.Add(KeyCode.Q);
			else if (state.IsKeyDown(Keys.Q) && xnaKeyboard.IsKeyDown(Keys.Q)) keyHolds.Add(KeyCode.Q);

			if (state.IsKeyDown(Keys.R) && !xnaKeyboard.IsKeyDown(Keys.R)) keyDowns.Add(KeyCode.R);
			else if (!state.IsKeyDown(Keys.R) && xnaKeyboard.IsKeyDown(Keys.R)) keyUps.Add(KeyCode.R);
			else if (state.IsKeyDown(Keys.R) && xnaKeyboard.IsKeyDown(Keys.R)) keyHolds.Add(KeyCode.R);

			if (state.IsKeyDown(Keys.S) && !xnaKeyboard.IsKeyDown(Keys.S)) keyDowns.Add(KeyCode.S);
			else if (!state.IsKeyDown(Keys.S) && xnaKeyboard.IsKeyDown(Keys.S)) keyUps.Add(KeyCode.S);
			else if (state.IsKeyDown(Keys.S) && xnaKeyboard.IsKeyDown(Keys.S)) keyHolds.Add(KeyCode.S);

			if (state.IsKeyDown(Keys.T) && !xnaKeyboard.IsKeyDown(Keys.T)) keyDowns.Add(KeyCode.T);
			else if (!state.IsKeyDown(Keys.T) && xnaKeyboard.IsKeyDown(Keys.T)) keyUps.Add(KeyCode.T);
			else if (state.IsKeyDown(Keys.T) && xnaKeyboard.IsKeyDown(Keys.T)) keyHolds.Add(KeyCode.T);

			if (state.IsKeyDown(Keys.U) && !xnaKeyboard.IsKeyDown(Keys.U)) keyDowns.Add(KeyCode.U);
			else if (!state.IsKeyDown(Keys.U) && xnaKeyboard.IsKeyDown(Keys.U)) keyUps.Add(KeyCode.U);
			else if (state.IsKeyDown(Keys.U) && xnaKeyboard.IsKeyDown(Keys.U)) keyHolds.Add(KeyCode.U);

			if (state.IsKeyDown(Keys.V) && !xnaKeyboard.IsKeyDown(Keys.V)) keyDowns.Add(KeyCode.V);
			else if (!state.IsKeyDown(Keys.V) && xnaKeyboard.IsKeyDown(Keys.V)) keyUps.Add(KeyCode.V);
			else if (state.IsKeyDown(Keys.V) && xnaKeyboard.IsKeyDown(Keys.V)) keyHolds.Add(KeyCode.V);

			if (state.IsKeyDown(Keys.W) && !xnaKeyboard.IsKeyDown(Keys.W)) keyDowns.Add(KeyCode.W);
			else if (!state.IsKeyDown(Keys.W) && xnaKeyboard.IsKeyDown(Keys.W)) keyUps.Add(KeyCode.W);
			else if (state.IsKeyDown(Keys.W) && xnaKeyboard.IsKeyDown(Keys.W)) keyHolds.Add(KeyCode.W);

			if (state.IsKeyDown(Keys.X) && !xnaKeyboard.IsKeyDown(Keys.X)) keyDowns.Add(KeyCode.X);
			else if (!state.IsKeyDown(Keys.X) && xnaKeyboard.IsKeyDown(Keys.X)) keyUps.Add(KeyCode.X);
			else if (state.IsKeyDown(Keys.X) && xnaKeyboard.IsKeyDown(Keys.X)) keyHolds.Add(KeyCode.X);

			if (state.IsKeyDown(Keys.Y) && !xnaKeyboard.IsKeyDown(Keys.Y)) keyDowns.Add(KeyCode.Y);
			else if (!state.IsKeyDown(Keys.Y) && xnaKeyboard.IsKeyDown(Keys.Y)) keyUps.Add(KeyCode.Y);
			else if (state.IsKeyDown(Keys.Y) && xnaKeyboard.IsKeyDown(Keys.Y)) keyHolds.Add(KeyCode.Y);

			if (state.IsKeyDown(Keys.Z) && !xnaKeyboard.IsKeyDown(Keys.Z)) keyDowns.Add(KeyCode.Z);
			else if (!state.IsKeyDown(Keys.Z) && xnaKeyboard.IsKeyDown(Keys.Z)) keyUps.Add(KeyCode.Z);
			else if (state.IsKeyDown(Keys.Z) && xnaKeyboard.IsKeyDown(Keys.Z)) keyHolds.Add(KeyCode.Z);

			//Keyboard Numbers

			if (state.IsKeyDown(Keys.D0) && !xnaKeyboard.IsKeyDown(Keys.D0)) keyDowns.Add(KeyCode.Alpha0);
			else if (!state.IsKeyDown(Keys.D0) && xnaKeyboard.IsKeyDown(Keys.D0)) keyUps.Add(KeyCode.Alpha0);
			else if (state.IsKeyDown(Keys.D0) && xnaKeyboard.IsKeyDown(Keys.D0)) keyHolds.Add(KeyCode.Alpha0);

			if (state.IsKeyDown(Keys.D1) && !xnaKeyboard.IsKeyDown(Keys.D1)) keyDowns.Add(KeyCode.Alpha1);
			else if (!state.IsKeyDown(Keys.D1) && xnaKeyboard.IsKeyDown(Keys.D1)) keyUps.Add(KeyCode.Alpha1);
			else if (state.IsKeyDown(Keys.D1) && xnaKeyboard.IsKeyDown(Keys.D1)) keyHolds.Add(KeyCode.Alpha1);

			if (state.IsKeyDown(Keys.D2) && !xnaKeyboard.IsKeyDown(Keys.D2)) keyDowns.Add(KeyCode.Alpha2);
			else if (!state.IsKeyDown(Keys.D2) && xnaKeyboard.IsKeyDown(Keys.D2)) keyUps.Add(KeyCode.Alpha2);
			else if (state.IsKeyDown(Keys.D2) && xnaKeyboard.IsKeyDown(Keys.D2)) keyHolds.Add(KeyCode.Alpha2);

			if (state.IsKeyDown(Keys.D3) && !xnaKeyboard.IsKeyDown(Keys.D3)) keyDowns.Add(KeyCode.Alpha3);
			else if (!state.IsKeyDown(Keys.D3) && xnaKeyboard.IsKeyDown(Keys.D3)) keyUps.Add(KeyCode.Alpha3);
			else if (state.IsKeyDown(Keys.D3) && xnaKeyboard.IsKeyDown(Keys.D3)) keyHolds.Add(KeyCode.Alpha3);

			if (state.IsKeyDown(Keys.D4) && !xnaKeyboard.IsKeyDown(Keys.D4)) keyDowns.Add(KeyCode.Alpha4);
			else if (!state.IsKeyDown(Keys.D4) && xnaKeyboard.IsKeyDown(Keys.D4)) keyUps.Add(KeyCode.Alpha4);
			else if (state.IsKeyDown(Keys.D4) && xnaKeyboard.IsKeyDown(Keys.D4)) keyHolds.Add(KeyCode.Alpha4);

			if (state.IsKeyDown(Keys.D5) && !xnaKeyboard.IsKeyDown(Keys.D5)) keyDowns.Add(KeyCode.Alpha5);
			else if (!state.IsKeyDown(Keys.D5) && xnaKeyboard.IsKeyDown(Keys.D5)) keyUps.Add(KeyCode.Alpha5);
			else if (state.IsKeyDown(Keys.D5) && xnaKeyboard.IsKeyDown(Keys.D5)) keyHolds.Add(KeyCode.Alpha5);

			if (state.IsKeyDown(Keys.D6) && !xnaKeyboard.IsKeyDown(Keys.D6)) keyDowns.Add(KeyCode.Alpha6);
			else if (!state.IsKeyDown(Keys.D6) && xnaKeyboard.IsKeyDown(Keys.D6)) keyUps.Add(KeyCode.Alpha6);
			else if (state.IsKeyDown(Keys.D6) && xnaKeyboard.IsKeyDown(Keys.D6)) keyHolds.Add(KeyCode.Alpha6);

			if (state.IsKeyDown(Keys.D7) && !xnaKeyboard.IsKeyDown(Keys.D7)) keyDowns.Add(KeyCode.Alpha7);
			else if (!state.IsKeyDown(Keys.D7) && xnaKeyboard.IsKeyDown(Keys.D7)) keyUps.Add(KeyCode.Alpha7);
			else if (state.IsKeyDown(Keys.D7) && xnaKeyboard.IsKeyDown(Keys.D7)) keyHolds.Add(KeyCode.Alpha7);

			if (state.IsKeyDown(Keys.D8) && !xnaKeyboard.IsKeyDown(Keys.D8)) keyDowns.Add(KeyCode.Alpha8);
			else if (!state.IsKeyDown(Keys.D8) && xnaKeyboard.IsKeyDown(Keys.D8)) keyUps.Add(KeyCode.Alpha8);
			else if (state.IsKeyDown(Keys.D8) && xnaKeyboard.IsKeyDown(Keys.D8)) keyHolds.Add(KeyCode.Alpha8);

			if (state.IsKeyDown(Keys.D9) && !xnaKeyboard.IsKeyDown(Keys.D9)) keyDowns.Add(KeyCode.Alpha9);
			else if (!state.IsKeyDown(Keys.D9) && xnaKeyboard.IsKeyDown(Keys.D9)) keyUps.Add(KeyCode.Alpha9);
			else if (state.IsKeyDown(Keys.D9) && xnaKeyboard.IsKeyDown(Keys.D9)) keyHolds.Add(KeyCode.Alpha9);

			//Numpad

			if (state.IsKeyDown(Keys.NumPad0) && !xnaKeyboard.IsKeyDown(Keys.NumPad0)) keyDowns.Add(KeyCode.Keypad0);
			else if (!state.IsKeyDown(Keys.NumPad0) && xnaKeyboard.IsKeyDown(Keys.NumPad0)) keyUps.Add(KeyCode.Keypad0);
			else if (state.IsKeyDown(Keys.NumPad0) && xnaKeyboard.IsKeyDown(Keys.NumPad0)) keyHolds.Add(KeyCode.Keypad0);

			if (state.IsKeyDown(Keys.NumPad1) && !xnaKeyboard.IsKeyDown(Keys.NumPad1)) keyDowns.Add(KeyCode.Keypad1);
			else if (!state.IsKeyDown(Keys.NumPad1) && xnaKeyboard.IsKeyDown(Keys.NumPad1)) keyUps.Add(KeyCode.Keypad1);
			else if (state.IsKeyDown(Keys.NumPad1) && xnaKeyboard.IsKeyDown(Keys.NumPad1)) keyHolds.Add(KeyCode.Keypad1);

			if (state.IsKeyDown(Keys.NumPad2) && !xnaKeyboard.IsKeyDown(Keys.NumPad2)) keyDowns.Add(KeyCode.Keypad2);
			else if (!state.IsKeyDown(Keys.NumPad2) && xnaKeyboard.IsKeyDown(Keys.NumPad2)) keyUps.Add(KeyCode.Keypad2);
			else if (state.IsKeyDown(Keys.NumPad2) && xnaKeyboard.IsKeyDown(Keys.NumPad2)) keyHolds.Add(KeyCode.Keypad2);

			if (state.IsKeyDown(Keys.NumPad3) && !xnaKeyboard.IsKeyDown(Keys.NumPad3)) keyDowns.Add(KeyCode.Keypad3);
			else if (!state.IsKeyDown(Keys.NumPad3) && xnaKeyboard.IsKeyDown(Keys.NumPad3)) keyUps.Add(KeyCode.Keypad3);
			else if (state.IsKeyDown(Keys.NumPad3) && xnaKeyboard.IsKeyDown(Keys.NumPad3)) keyHolds.Add(KeyCode.Keypad3);

			if (state.IsKeyDown(Keys.NumPad4) && !xnaKeyboard.IsKeyDown(Keys.NumPad4)) keyDowns.Add(KeyCode.Keypad4);
			else if (!state.IsKeyDown(Keys.NumPad4) && xnaKeyboard.IsKeyDown(Keys.NumPad4)) keyUps.Add(KeyCode.Keypad4);
			else if (state.IsKeyDown(Keys.NumPad4) && xnaKeyboard.IsKeyDown(Keys.NumPad4)) keyHolds.Add(KeyCode.Keypad4);

			if (state.IsKeyDown(Keys.NumPad5) && !xnaKeyboard.IsKeyDown(Keys.NumPad5)) keyDowns.Add(KeyCode.Keypad5);
			else if (!state.IsKeyDown(Keys.NumPad5) && xnaKeyboard.IsKeyDown(Keys.NumPad5)) keyUps.Add(KeyCode.Keypad5);
			else if (state.IsKeyDown(Keys.NumPad5) && xnaKeyboard.IsKeyDown(Keys.NumPad5)) keyHolds.Add(KeyCode.Keypad5);

			if (state.IsKeyDown(Keys.NumPad6) && !xnaKeyboard.IsKeyDown(Keys.NumPad6)) keyDowns.Add(KeyCode.Keypad6);
			else if (!state.IsKeyDown(Keys.NumPad6) && xnaKeyboard.IsKeyDown(Keys.NumPad6)) keyUps.Add(KeyCode.Keypad6);
			else if (state.IsKeyDown(Keys.NumPad6) && xnaKeyboard.IsKeyDown(Keys.NumPad6)) keyHolds.Add(KeyCode.Keypad6);

			if (state.IsKeyDown(Keys.NumPad7) && !xnaKeyboard.IsKeyDown(Keys.NumPad7)) keyDowns.Add(KeyCode.Keypad7);
			else if (!state.IsKeyDown(Keys.NumPad7) && xnaKeyboard.IsKeyDown(Keys.NumPad7)) keyUps.Add(KeyCode.Keypad7);
			else if (state.IsKeyDown(Keys.NumPad7) && xnaKeyboard.IsKeyDown(Keys.NumPad7)) keyHolds.Add(KeyCode.Keypad7);

			if (state.IsKeyDown(Keys.NumPad8) && !xnaKeyboard.IsKeyDown(Keys.NumPad8)) keyDowns.Add(KeyCode.Keypad8);
			else if (!state.IsKeyDown(Keys.NumPad8) && xnaKeyboard.IsKeyDown(Keys.NumPad8)) keyUps.Add(KeyCode.Keypad8);
			else if (state.IsKeyDown(Keys.NumPad8) && xnaKeyboard.IsKeyDown(Keys.NumPad8)) keyHolds.Add(KeyCode.Keypad8);

			if (state.IsKeyDown(Keys.NumPad9) && !xnaKeyboard.IsKeyDown(Keys.NumPad9)) keyDowns.Add(KeyCode.Keypad9);
			else if (!state.IsKeyDown(Keys.NumPad9) && xnaKeyboard.IsKeyDown(Keys.NumPad9)) keyUps.Add(KeyCode.Keypad9);
			else if (state.IsKeyDown(Keys.NumPad9) && xnaKeyboard.IsKeyDown(Keys.NumPad9)) keyHolds.Add(KeyCode.Keypad9);

			//Modifiers

			if (state.IsKeyDown(Keys.LeftControl) && !xnaKeyboard.IsKeyDown(Keys.LeftControl)) keyDowns.Add(KeyCode.LeftControl);
			else if (!state.IsKeyDown(Keys.LeftControl) && xnaKeyboard.IsKeyDown(Keys.LeftControl)) keyUps.Add(KeyCode.LeftControl);
			else if (state.IsKeyDown(Keys.LeftControl) && xnaKeyboard.IsKeyDown(Keys.LeftControl)) keyHolds.Add(KeyCode.LeftControl);

			if (state.IsKeyDown(Keys.LeftAlt) && !xnaKeyboard.IsKeyDown(Keys.LeftAlt)) keyDowns.Add(KeyCode.LeftAlt);
			else if (!state.IsKeyDown(Keys.LeftAlt) && xnaKeyboard.IsKeyDown(Keys.LeftAlt)) keyUps.Add(KeyCode.LeftAlt);
			else if (state.IsKeyDown(Keys.LeftAlt) && xnaKeyboard.IsKeyDown(Keys.LeftAlt)) keyHolds.Add(KeyCode.LeftAlt);

			if (state.IsKeyDown(Keys.LeftWindows) && !xnaKeyboard.IsKeyDown(Keys.LeftWindows)) keyDowns.Add(KeyCode.LeftWindows);
			else if (!state.IsKeyDown(Keys.LeftWindows) && xnaKeyboard.IsKeyDown(Keys.LeftWindows)) keyUps.Add(KeyCode.LeftWindows);
			else if (state.IsKeyDown(Keys.LeftWindows) && xnaKeyboard.IsKeyDown(Keys.LeftWindows)) keyHolds.Add(KeyCode.LeftWindows);

			if (state.IsKeyDown(Keys.LeftWindows) && !xnaKeyboard.IsKeyDown(Keys.LeftWindows)) keyDowns.Add(KeyCode.LeftCommand);
			else if (!state.IsKeyDown(Keys.LeftWindows) && xnaKeyboard.IsKeyDown(Keys.LeftWindows)) keyUps.Add(KeyCode.LeftCommand);
			else if (state.IsKeyDown(Keys.LeftWindows) && xnaKeyboard.IsKeyDown(Keys.LeftWindows)) keyHolds.Add(KeyCode.LeftCommand);

			if (state.IsKeyDown(Keys.LeftWindows) && !xnaKeyboard.IsKeyDown(Keys.LeftWindows)) keyDowns.Add(KeyCode.LeftApple);
			else if (!state.IsKeyDown(Keys.LeftWindows) && xnaKeyboard.IsKeyDown(Keys.LeftWindows)) keyUps.Add(KeyCode.LeftApple);
			else if (state.IsKeyDown(Keys.LeftWindows) && xnaKeyboard.IsKeyDown(Keys.LeftWindows)) keyHolds.Add(KeyCode.LeftApple);

			if (state.IsKeyDown(Keys.Space) && !xnaKeyboard.IsKeyDown(Keys.Space)) keyDowns.Add(KeyCode.Space);
			else if (!state.IsKeyDown(Keys.Space) && xnaKeyboard.IsKeyDown(Keys.Space)) keyUps.Add(KeyCode.Space);
			else if (state.IsKeyDown(Keys.Space) && xnaKeyboard.IsKeyDown(Keys.Space)) keyHolds.Add(KeyCode.Space);

			if (state.IsKeyDown(Keys.RightControl) && !xnaKeyboard.IsKeyDown(Keys.RightControl)) keyDowns.Add(KeyCode.RightControl);
			else if (!state.IsKeyDown(Keys.RightControl) && xnaKeyboard.IsKeyDown(Keys.RightControl)) keyUps.Add(KeyCode.RightControl);
			else if (state.IsKeyDown(Keys.RightControl) && xnaKeyboard.IsKeyDown(Keys.RightControl)) keyHolds.Add(KeyCode.RightControl);

			if (state.IsKeyDown(Keys.RightAlt) && !xnaKeyboard.IsKeyDown(Keys.RightAlt)) keyDowns.Add(KeyCode.RightAlt);
			else if (!state.IsKeyDown(Keys.RightAlt) && xnaKeyboard.IsKeyDown(Keys.RightAlt)) keyUps.Add(KeyCode.RightAlt);
			else if (state.IsKeyDown(Keys.RightAlt) && xnaKeyboard.IsKeyDown(Keys.RightAlt)) keyHolds.Add(KeyCode.RightAlt);

			if (state.IsKeyDown(Keys.RightWindows) && !xnaKeyboard.IsKeyDown(Keys.RightWindows)) keyDowns.Add(KeyCode.RightWindows);
			else if (!state.IsKeyDown(Keys.RightWindows) && xnaKeyboard.IsKeyDown(Keys.RightWindows)) keyUps.Add(KeyCode.RightWindows);
			else if (state.IsKeyDown(Keys.RightWindows) && xnaKeyboard.IsKeyDown(Keys.RightWindows)) keyHolds.Add(KeyCode.RightWindows);

			if (state.IsKeyDown(Keys.RightWindows) && !xnaKeyboard.IsKeyDown(Keys.RightWindows)) keyDowns.Add(KeyCode.RightCommand);
			else if (!state.IsKeyDown(Keys.RightWindows) && xnaKeyboard.IsKeyDown(Keys.RightWindows)) keyUps.Add(KeyCode.RightCommand);
			else if (state.IsKeyDown(Keys.RightWindows) && xnaKeyboard.IsKeyDown(Keys.RightWindows)) keyHolds.Add(KeyCode.RightCommand);

			if (state.IsKeyDown(Keys.RightWindows) && !xnaKeyboard.IsKeyDown(Keys.RightWindows)) keyDowns.Add(KeyCode.RightApple);
			else if (!state.IsKeyDown(Keys.RightWindows) && xnaKeyboard.IsKeyDown(Keys.RightWindows)) keyUps.Add(KeyCode.RightApple);
			else if (state.IsKeyDown(Keys.RightWindows) && xnaKeyboard.IsKeyDown(Keys.RightWindows)) keyHolds.Add(KeyCode.RightApple);

			if (state.IsKeyDown(Keys.RightShift) && !xnaKeyboard.IsKeyDown(Keys.RightShift)) keyDowns.Add(KeyCode.RightShift);
			else if (!state.IsKeyDown(Keys.RightShift) && xnaKeyboard.IsKeyDown(Keys.RightShift)) keyUps.Add(KeyCode.RightShift);
			else if (state.IsKeyDown(Keys.RightShift) && xnaKeyboard.IsKeyDown(Keys.RightShift)) keyHolds.Add(KeyCode.RightShift);

			if (state.IsKeyDown(Keys.LeftShift) && !xnaKeyboard.IsKeyDown(Keys.LeftShift)) keyDowns.Add(KeyCode.LeftShift);
			else if (!state.IsKeyDown(Keys.LeftShift) && xnaKeyboard.IsKeyDown(Keys.LeftShift)) keyUps.Add(KeyCode.LeftShift);
			else if (state.IsKeyDown(Keys.LeftShift) && xnaKeyboard.IsKeyDown(Keys.LeftShift)) keyHolds.Add(KeyCode.LeftShift);

			if (state.IsKeyDown(Keys.Tab) && !xnaKeyboard.IsKeyDown(Keys.Tab)) keyDowns.Add(KeyCode.Tab);
			else if (!state.IsKeyDown(Keys.Tab) && xnaKeyboard.IsKeyDown(Keys.Tab)) keyUps.Add(KeyCode.Tab);
			else if (state.IsKeyDown(Keys.Tab) && xnaKeyboard.IsKeyDown(Keys.Tab)) keyHolds.Add(KeyCode.Tab);

			if (state.IsKeyDown(Keys.CapsLock) && !xnaKeyboard.IsKeyDown(Keys.CapsLock)) keyDowns.Add(KeyCode.CapsLock);
			else if (!state.IsKeyDown(Keys.CapsLock) && xnaKeyboard.IsKeyDown(Keys.CapsLock)) keyUps.Add(KeyCode.CapsLock);
			else if (state.IsKeyDown(Keys.CapsLock) && xnaKeyboard.IsKeyDown(Keys.CapsLock)) keyHolds.Add(KeyCode.CapsLock);

			if (state.IsKeyDown(Keys.Escape) && !xnaKeyboard.IsKeyDown(Keys.Escape)) keyDowns.Add(KeyCode.Escape);
			else if (!state.IsKeyDown(Keys.Escape) && xnaKeyboard.IsKeyDown(Keys.Escape)) keyUps.Add(KeyCode.Escape);
			else if (state.IsKeyDown(Keys.Escape) && xnaKeyboard.IsKeyDown(Keys.Escape)) keyHolds.Add(KeyCode.Escape);

			if (state.IsKeyDown(Keys.Back) && !xnaKeyboard.IsKeyDown(Keys.Back)) keyDowns.Add(KeyCode.Backspace);
			else if (!state.IsKeyDown(Keys.Back) && xnaKeyboard.IsKeyDown(Keys.Back)) keyUps.Add(KeyCode.Backspace);
			else if (state.IsKeyDown(Keys.Back) && xnaKeyboard.IsKeyDown(Keys.Back)) keyHolds.Add(KeyCode.Backspace);

			if (state.IsKeyDown(Keys.Enter) && !xnaKeyboard.IsKeyDown(Keys.Enter)) keyDowns.Add(KeyCode.KeypadEnter);
			else if (!state.IsKeyDown(Keys.Enter) && xnaKeyboard.IsKeyDown(Keys.Enter)) keyUps.Add(KeyCode.KeypadEnter);
			else if (state.IsKeyDown(Keys.Enter) && xnaKeyboard.IsKeyDown(Keys.Enter)) keyHolds.Add(KeyCode.KeypadEnter);

			if (state.IsKeyDown(Keys.Enter) && !xnaKeyboard.IsKeyDown(Keys.Enter)) keyDowns.Add(KeyCode.Return);
			else if (!state.IsKeyDown(Keys.Enter) && xnaKeyboard.IsKeyDown(Keys.Enter)) keyUps.Add(KeyCode.Return);
			else if (state.IsKeyDown(Keys.Enter) && xnaKeyboard.IsKeyDown(Keys.Enter)) keyHolds.Add(KeyCode.Return);

			//Arrows

			if (state.IsKeyDown(Keys.Right) && !xnaKeyboard.IsKeyDown(Keys.Right)) keyDowns.Add(KeyCode.RightArrow);
			else if (!state.IsKeyDown(Keys.Right) && xnaKeyboard.IsKeyDown(Keys.Right)) keyUps.Add(KeyCode.RightArrow);
			else if (state.IsKeyDown(Keys.Right) && xnaKeyboard.IsKeyDown(Keys.Right)) keyHolds.Add(KeyCode.RightArrow);

			if (state.IsKeyDown(Keys.Up) && !xnaKeyboard.IsKeyDown(Keys.Up)) keyDowns.Add(KeyCode.UpArrow);
			else if (!state.IsKeyDown(Keys.Up) && xnaKeyboard.IsKeyDown(Keys.Up)) keyUps.Add(KeyCode.UpArrow);
			else if (state.IsKeyDown(Keys.Up) && xnaKeyboard.IsKeyDown(Keys.Up)) keyHolds.Add(KeyCode.UpArrow);

			if (state.IsKeyDown(Keys.Left) && !xnaKeyboard.IsKeyDown(Keys.Left)) keyDowns.Add(KeyCode.LeftArrow);
			else if (!state.IsKeyDown(Keys.Left) && xnaKeyboard.IsKeyDown(Keys.Left)) keyUps.Add(KeyCode.LeftArrow);
			else if (state.IsKeyDown(Keys.Left) && xnaKeyboard.IsKeyDown(Keys.Left)) keyHolds.Add(KeyCode.LeftArrow);

			if (state.IsKeyDown(Keys.Down) && !xnaKeyboard.IsKeyDown(Keys.Down)) keyDowns.Add(KeyCode.DownArrow);
			else if (!state.IsKeyDown(Keys.Down) && xnaKeyboard.IsKeyDown(Keys.Down)) keyUps.Add(KeyCode.DownArrow);
			else if (state.IsKeyDown(Keys.Down) && xnaKeyboard.IsKeyDown(Keys.Down)) keyHolds.Add(KeyCode.DownArrow);

			//F Keys

			if (state.IsKeyDown(Keys.F1) && !xnaKeyboard.IsKeyDown(Keys.F1)) keyDowns.Add(KeyCode.F1);
			else if (!state.IsKeyDown(Keys.F1) && xnaKeyboard.IsKeyDown(Keys.F1)) keyUps.Add(KeyCode.F1);
			else if (state.IsKeyDown(Keys.F1) && xnaKeyboard.IsKeyDown(Keys.F1)) keyHolds.Add(KeyCode.F1);

			if (state.IsKeyDown(Keys.F2) && !xnaKeyboard.IsKeyDown(Keys.F2)) keyDowns.Add(KeyCode.F2);
			else if (!state.IsKeyDown(Keys.F2) && xnaKeyboard.IsKeyDown(Keys.F2)) keyUps.Add(KeyCode.F2);
			else if (state.IsKeyDown(Keys.F2) && xnaKeyboard.IsKeyDown(Keys.F2)) keyHolds.Add(KeyCode.F2);

			if (state.IsKeyDown(Keys.F3) && !xnaKeyboard.IsKeyDown(Keys.F3)) keyDowns.Add(KeyCode.F3);
			else if (!state.IsKeyDown(Keys.F3) && xnaKeyboard.IsKeyDown(Keys.F3)) keyUps.Add(KeyCode.F3);
			else if (state.IsKeyDown(Keys.F3) && xnaKeyboard.IsKeyDown(Keys.F3)) keyHolds.Add(KeyCode.F3);

			if (state.IsKeyDown(Keys.F4) && !xnaKeyboard.IsKeyDown(Keys.F4)) keyDowns.Add(KeyCode.F4);
			else if (!state.IsKeyDown(Keys.F4) && xnaKeyboard.IsKeyDown(Keys.F4)) keyUps.Add(KeyCode.F4);
			else if (state.IsKeyDown(Keys.F4) && xnaKeyboard.IsKeyDown(Keys.F4)) keyHolds.Add(KeyCode.F4);

			if (state.IsKeyDown(Keys.F5) && !xnaKeyboard.IsKeyDown(Keys.F5)) keyDowns.Add(KeyCode.F5);
			else if (!state.IsKeyDown(Keys.F5) && xnaKeyboard.IsKeyDown(Keys.F5)) keyUps.Add(KeyCode.F5);
			else if (state.IsKeyDown(Keys.F5) && xnaKeyboard.IsKeyDown(Keys.F5)) keyHolds.Add(KeyCode.F5);

			if (state.IsKeyDown(Keys.F6) && !xnaKeyboard.IsKeyDown(Keys.F6)) keyDowns.Add(KeyCode.F6);
			else if (!state.IsKeyDown(Keys.F6) && xnaKeyboard.IsKeyDown(Keys.F6)) keyUps.Add(KeyCode.F6);
			else if (state.IsKeyDown(Keys.F6) && xnaKeyboard.IsKeyDown(Keys.F6)) keyHolds.Add(KeyCode.F6);

			if (state.IsKeyDown(Keys.F7) && !xnaKeyboard.IsKeyDown(Keys.F7)) keyDowns.Add(KeyCode.F7);
			else if (!state.IsKeyDown(Keys.F7) && xnaKeyboard.IsKeyDown(Keys.F7)) keyUps.Add(KeyCode.F7);
			else if (state.IsKeyDown(Keys.F7) && xnaKeyboard.IsKeyDown(Keys.F7)) keyHolds.Add(KeyCode.F7);

			if (state.IsKeyDown(Keys.F8) && !xnaKeyboard.IsKeyDown(Keys.F8)) keyDowns.Add(KeyCode.F8);
			else if (!state.IsKeyDown(Keys.F8) && xnaKeyboard.IsKeyDown(Keys.F8)) keyUps.Add(KeyCode.F8);
			else if (state.IsKeyDown(Keys.F8) && xnaKeyboard.IsKeyDown(Keys.F8)) keyHolds.Add(KeyCode.F8);

			if (state.IsKeyDown(Keys.F9) && !xnaKeyboard.IsKeyDown(Keys.F9)) keyDowns.Add(KeyCode.F9);
			else if (!state.IsKeyDown(Keys.F9) && xnaKeyboard.IsKeyDown(Keys.F9)) keyUps.Add(KeyCode.F9);
			else if (state.IsKeyDown(Keys.F9) && xnaKeyboard.IsKeyDown(Keys.F9)) keyHolds.Add(KeyCode.F9);

			if (state.IsKeyDown(Keys.F10) && !xnaKeyboard.IsKeyDown(Keys.F10)) keyDowns.Add(KeyCode.F10);
			else if (!state.IsKeyDown(Keys.F10) && xnaKeyboard.IsKeyDown(Keys.F10)) keyUps.Add(KeyCode.F10);
			else if (state.IsKeyDown(Keys.F10) && xnaKeyboard.IsKeyDown(Keys.F10)) keyHolds.Add(KeyCode.F10);

			if (state.IsKeyDown(Keys.F11) && !xnaKeyboard.IsKeyDown(Keys.F11)) keyDowns.Add(KeyCode.F11);
			else if (!state.IsKeyDown(Keys.F11) && xnaKeyboard.IsKeyDown(Keys.F11)) keyUps.Add(KeyCode.F11);
			else if (state.IsKeyDown(Keys.F11) && xnaKeyboard.IsKeyDown(Keys.F11)) keyHolds.Add(KeyCode.F11);

			if (state.IsKeyDown(Keys.F12) && !xnaKeyboard.IsKeyDown(Keys.F12)) keyDowns.Add(KeyCode.F12);
			else if (!state.IsKeyDown(Keys.F12) && xnaKeyboard.IsKeyDown(Keys.F12)) keyUps.Add(KeyCode.F12);
			else if (state.IsKeyDown(Keys.F12) && xnaKeyboard.IsKeyDown(Keys.F12)) keyHolds.Add(KeyCode.F12);

			//Others

			if (state.IsKeyDown(Keys.OemComma) && !xnaKeyboard.IsKeyDown(Keys.OemComma)) keyDowns.Add(KeyCode.Comma);
			else if (!state.IsKeyDown(Keys.OemComma) && xnaKeyboard.IsKeyDown(Keys.OemComma)) keyUps.Add(KeyCode.Comma);
			else if (state.IsKeyDown(Keys.OemComma) && xnaKeyboard.IsKeyDown(Keys.OemComma)) keyHolds.Add(KeyCode.Comma);

			if (state.IsKeyDown(Keys.OemPeriod) && !xnaKeyboard.IsKeyDown(Keys.OemPeriod)) keyDowns.Add(KeyCode.Period);
			else if (!state.IsKeyDown(Keys.OemPeriod) && xnaKeyboard.IsKeyDown(Keys.OemPeriod)) keyUps.Add(KeyCode.Period);
			else if (state.IsKeyDown(Keys.OemPeriod) && xnaKeyboard.IsKeyDown(Keys.OemPeriod)) keyHolds.Add(KeyCode.Period);

			if (state.IsKeyDown(Keys.OemSemicolon) && !xnaKeyboard.IsKeyDown(Keys.OemSemicolon)) keyDowns.Add(KeyCode.Semicolon);
			else if (!state.IsKeyDown(Keys.OemSemicolon) && xnaKeyboard.IsKeyDown(Keys.OemSemicolon)) keyUps.Add(KeyCode.Semicolon);
			else if (state.IsKeyDown(Keys.OemSemicolon) && xnaKeyboard.IsKeyDown(Keys.OemSemicolon)) keyHolds.Add(KeyCode.Semicolon);

			if (state.IsKeyDown(Keys.Delete) && !xnaKeyboard.IsKeyDown(Keys.Delete)) keyDowns.Add(KeyCode.Delete);
			else if (!state.IsKeyDown(Keys.Delete) && xnaKeyboard.IsKeyDown(Keys.Delete)) keyUps.Add(KeyCode.Delete);
			else if (state.IsKeyDown(Keys.Delete) && xnaKeyboard.IsKeyDown(Keys.Delete)) keyHolds.Add(KeyCode.Delete);

			if (state.IsKeyDown(Keys.Pause) && !xnaKeyboard.IsKeyDown(Keys.Pause)) keyDowns.Add(KeyCode.Pause);
			else if (!state.IsKeyDown(Keys.Pause) && xnaKeyboard.IsKeyDown(Keys.Pause)) keyUps.Add(KeyCode.Pause);
			else if (state.IsKeyDown(Keys.Pause) && xnaKeyboard.IsKeyDown(Keys.Pause)) keyHolds.Add(KeyCode.Pause);

			if (state.IsKeyDown(Keys.Decimal) && !xnaKeyboard.IsKeyDown(Keys.Decimal)) keyDowns.Add(KeyCode.KeypadPeriod);
			else if (!state.IsKeyDown(Keys.Decimal) && xnaKeyboard.IsKeyDown(Keys.Decimal)) keyUps.Add(KeyCode.KeypadPeriod);
			else if (state.IsKeyDown(Keys.Decimal) && xnaKeyboard.IsKeyDown(Keys.Decimal)) keyHolds.Add(KeyCode.KeypadPeriod);

			if (state.IsKeyDown(Keys.Divide) && !xnaKeyboard.IsKeyDown(Keys.Divide)) keyDowns.Add(KeyCode.KeypadDivide);
			else if (!state.IsKeyDown(Keys.Divide) && xnaKeyboard.IsKeyDown(Keys.Divide)) keyUps.Add(KeyCode.KeypadDivide);
			else if (state.IsKeyDown(Keys.Divide) && xnaKeyboard.IsKeyDown(Keys.Divide)) keyHolds.Add(KeyCode.KeypadDivide);

			if (state.IsKeyDown(Keys.Add) && !xnaKeyboard.IsKeyDown(Keys.Add)) keyDowns.Add(KeyCode.KeypadPlus);
			else if (!state.IsKeyDown(Keys.Add) && xnaKeyboard.IsKeyDown(Keys.Add)) keyUps.Add(KeyCode.KeypadPlus);
			else if (state.IsKeyDown(Keys.Add) && xnaKeyboard.IsKeyDown(Keys.Add)) keyHolds.Add(KeyCode.KeypadPlus);

			if (state.IsKeyDown(Keys.Subtract) && !xnaKeyboard.IsKeyDown(Keys.Subtract)) keyDowns.Add(KeyCode.KeypadMinus);
			else if (!state.IsKeyDown(Keys.Subtract) && xnaKeyboard.IsKeyDown(Keys.Subtract)) keyUps.Add(KeyCode.KeypadMinus);
			else if (state.IsKeyDown(Keys.Subtract) && xnaKeyboard.IsKeyDown(Keys.Subtract)) keyHolds.Add(KeyCode.KeypadMinus);

			if (state.IsKeyDown(Keys.Multiply) && !xnaKeyboard.IsKeyDown(Keys.Multiply)) keyDowns.Add(KeyCode.KeypadMultiply);
			else if (!state.IsKeyDown(Keys.Multiply) && xnaKeyboard.IsKeyDown(Keys.Multiply)) keyUps.Add(KeyCode.KeypadMultiply);
			else if (state.IsKeyDown(Keys.Multiply) && xnaKeyboard.IsKeyDown(Keys.Multiply)) keyHolds.Add(KeyCode.KeypadMultiply);

			if (state.IsKeyDown(Keys.OemTilde) && !xnaKeyboard.IsKeyDown(Keys.OemTilde)) keyDowns.Add(KeyCode.BackQuote);
			else if (!state.IsKeyDown(Keys.OemTilde) && xnaKeyboard.IsKeyDown(Keys.OemTilde)) keyUps.Add(KeyCode.BackQuote);
			else if (state.IsKeyDown(Keys.OemTilde) && xnaKeyboard.IsKeyDown(Keys.OemTilde)) keyHolds.Add(KeyCode.BackQuote);

			if (state.IsKeyDown(Keys.Insert) && !xnaKeyboard.IsKeyDown(Keys.Insert)) keyDowns.Add(KeyCode.Insert);
			else if (!state.IsKeyDown(Keys.Insert) && xnaKeyboard.IsKeyDown(Keys.Insert)) keyUps.Add(KeyCode.Insert);
			else if (state.IsKeyDown(Keys.Insert) && xnaKeyboard.IsKeyDown(Keys.Insert)) keyHolds.Add(KeyCode.Insert);

			if (state.IsKeyDown(Keys.Home) && !xnaKeyboard.IsKeyDown(Keys.Home)) keyDowns.Add(KeyCode.Home);
			else if (!state.IsKeyDown(Keys.Home) && xnaKeyboard.IsKeyDown(Keys.Home)) keyUps.Add(KeyCode.Home);
			else if (state.IsKeyDown(Keys.Home) && xnaKeyboard.IsKeyDown(Keys.Home)) keyHolds.Add(KeyCode.Home);

			if (state.IsKeyDown(Keys.End) && !xnaKeyboard.IsKeyDown(Keys.End)) keyDowns.Add(KeyCode.End);
			else if (!state.IsKeyDown(Keys.End) && xnaKeyboard.IsKeyDown(Keys.End)) keyUps.Add(KeyCode.End);
			else if (state.IsKeyDown(Keys.End) && xnaKeyboard.IsKeyDown(Keys.End)) keyHolds.Add(KeyCode.End);

			if (state.IsKeyDown(Keys.PageUp) && !xnaKeyboard.IsKeyDown(Keys.PageUp)) keyDowns.Add(KeyCode.PageUp);
			else if (!state.IsKeyDown(Keys.PageUp) && xnaKeyboard.IsKeyDown(Keys.PageUp)) keyUps.Add(KeyCode.PageUp);
			else if (state.IsKeyDown(Keys.PageUp) && xnaKeyboard.IsKeyDown(Keys.PageUp)) keyHolds.Add(KeyCode.PageUp);

			if (state.IsKeyDown(Keys.PageDown) && !xnaKeyboard.IsKeyDown(Keys.PageDown)) keyDowns.Add(KeyCode.PageDown);
			else if (!state.IsKeyDown(Keys.PageDown) && xnaKeyboard.IsKeyDown(Keys.PageDown)) keyUps.Add(KeyCode.PageDown);
			else if (state.IsKeyDown(Keys.PageDown) && xnaKeyboard.IsKeyDown(Keys.PageDown)) keyHolds.Add(KeyCode.PageDown);

			if (state.IsKeyDown(Keys.OemPlus) && !xnaKeyboard.IsKeyDown(Keys.OemPlus)) keyDowns.Add(KeyCode.Plus);
			else if (!state.IsKeyDown(Keys.OemPlus) && xnaKeyboard.IsKeyDown(Keys.OemPlus)) keyUps.Add(KeyCode.Plus);
			else if (state.IsKeyDown(Keys.OemPlus) && xnaKeyboard.IsKeyDown(Keys.OemPlus)) keyHolds.Add(KeyCode.Plus);

			if (state.IsKeyDown(Keys.OemMinus) && !xnaKeyboard.IsKeyDown(Keys.OemMinus)) keyDowns.Add(KeyCode.Minus);
			else if (!state.IsKeyDown(Keys.OemMinus) && xnaKeyboard.IsKeyDown(Keys.OemMinus)) keyUps.Add(KeyCode.Minus);
			else if (state.IsKeyDown(Keys.OemMinus) && xnaKeyboard.IsKeyDown(Keys.OemMinus)) keyHolds.Add(KeyCode.Minus);

			if (state.IsKeyDown(Keys.OemBackslash) && !xnaKeyboard.IsKeyDown(Keys.OemBackslash)) keyDowns.Add(KeyCode.Backslash);
			else if (!state.IsKeyDown(Keys.OemBackslash) && xnaKeyboard.IsKeyDown(Keys.OemBackslash)) keyUps.Add(KeyCode.Backslash);
			else if (state.IsKeyDown(Keys.OemBackslash) && xnaKeyboard.IsKeyDown(Keys.OemBackslash)) keyHolds.Add(KeyCode.Backslash);

			if (state.IsKeyDown(Keys.OemQuotes) && !xnaKeyboard.IsKeyDown(Keys.OemQuotes)) keyDowns.Add(KeyCode.Quote);
			else if (!state.IsKeyDown(Keys.OemQuotes) && xnaKeyboard.IsKeyDown(Keys.OemQuotes)) keyUps.Add(KeyCode.Quote);
			else if (state.IsKeyDown(Keys.OemQuotes) && xnaKeyboard.IsKeyDown(Keys.OemQuotes)) keyHolds.Add(KeyCode.Quote);

			if (state.IsKeyDown(Keys.OemOpenBrackets) && !xnaKeyboard.IsKeyDown(Keys.OemOpenBrackets)) keyDowns.Add(KeyCode.LeftBracket);
			else if (!state.IsKeyDown(Keys.OemOpenBrackets) && xnaKeyboard.IsKeyDown(Keys.OemOpenBrackets)) keyUps.Add(KeyCode.LeftBracket);
			else if (state.IsKeyDown(Keys.OemOpenBrackets) && xnaKeyboard.IsKeyDown(Keys.OemOpenBrackets)) keyHolds.Add(KeyCode.LeftBracket);

			if (state.IsKeyDown(Keys.OemCloseBrackets) && !xnaKeyboard.IsKeyDown(Keys.OemCloseBrackets)) keyDowns.Add(KeyCode.RightBracket);
			else if (!state.IsKeyDown(Keys.OemCloseBrackets) && xnaKeyboard.IsKeyDown(Keys.OemCloseBrackets)) keyUps.Add(KeyCode.RightBracket);
			else if (state.IsKeyDown(Keys.OemCloseBrackets) && xnaKeyboard.IsKeyDown(Keys.OemCloseBrackets)) keyHolds.Add(KeyCode.RightBracket);

			if (state.IsKeyDown(Keys.Scroll) && !xnaKeyboard.IsKeyDown(Keys.Scroll)) keyDowns.Add(KeyCode.ScrollLock);
			else if (!state.IsKeyDown(Keys.Scroll) && xnaKeyboard.IsKeyDown(Keys.Scroll)) keyUps.Add(KeyCode.ScrollLock);
			else if (state.IsKeyDown(Keys.Scroll) && xnaKeyboard.IsKeyDown(Keys.Scroll)) keyHolds.Add(KeyCode.ScrollLock);

			if (state.IsKeyDown(Keys.NumLock) && !xnaKeyboard.IsKeyDown(Keys.NumLock)) keyDowns.Add(KeyCode.Numlock);
			else if (!state.IsKeyDown(Keys.NumLock) && xnaKeyboard.IsKeyDown(Keys.NumLock)) keyUps.Add(KeyCode.Numlock);
			else if (state.IsKeyDown(Keys.NumLock) && xnaKeyboard.IsKeyDown(Keys.NumLock)) keyHolds.Add(KeyCode.Numlock);



			xnaKeyboard = state;
		}

		internal static void UpdateMouse()
		{
			MouseState state = Mouse.GetState();

			if (state.LeftButton == ButtonState.Pressed && xnaMouse.LeftButton == ButtonState.Released) keyDowns.Add(KeyCode.Mouse0);
			else if (state.LeftButton == ButtonState.Released && xnaMouse.LeftButton == ButtonState.Pressed) keyUps.Add(KeyCode.Mouse0);
			else if (state.LeftButton == ButtonState.Pressed && xnaMouse.LeftButton == ButtonState.Pressed) keyHolds.Add(KeyCode.Mouse0);

			if (state.RightButton == ButtonState.Pressed && xnaMouse.RightButton == ButtonState.Released) keyDowns.Add(KeyCode.Mouse1);
			else if (state.RightButton == ButtonState.Released && xnaMouse.RightButton == ButtonState.Pressed) keyUps.Add(KeyCode.Mouse1);
			else if (state.RightButton == ButtonState.Pressed && xnaMouse.RightButton == ButtonState.Pressed) keyHolds.Add(KeyCode.Mouse1);

			if (state.MiddleButton == ButtonState.Pressed && xnaMouse.MiddleButton == ButtonState.Released) keyDowns.Add(KeyCode.Mouse2);
			else if (state.MiddleButton == ButtonState.Released && xnaMouse.MiddleButton == ButtonState.Pressed) keyUps.Add(KeyCode.Mouse2);
			else if (state.MiddleButton == ButtonState.Pressed && xnaMouse.MiddleButton == ButtonState.Pressed) keyHolds.Add(KeyCode.Mouse2);

			if (state.XButton1 == ButtonState.Pressed && xnaMouse.XButton1 == ButtonState.Released) keyDowns.Add(KeyCode.Mouse3);
			else if (state.XButton1 == ButtonState.Released && xnaMouse.XButton1 == ButtonState.Pressed) keyUps.Add(KeyCode.Mouse3);
			else if (state.XButton1 == ButtonState.Pressed && xnaMouse.XButton1 == ButtonState.Pressed) keyHolds.Add(KeyCode.Mouse3);

			if (state.XButton2 == ButtonState.Pressed && xnaMouse.XButton2 == ButtonState.Released) keyDowns.Add(KeyCode.Mouse4);
			else if (state.XButton2 == ButtonState.Released && xnaMouse.XButton2 == ButtonState.Pressed) keyUps.Add(KeyCode.Mouse4);
			else if (state.XButton2 == ButtonState.Pressed && xnaMouse.XButton2 == ButtonState.Pressed) keyHolds.Add(KeyCode.Mouse4);

			xnaMouse = state;

			mMousePos.x = xnaMouse.X;
			mMousePos.y = xnaMouse.Y;
			//TODO orient with bottom corner = 0,0
		}

		#endregion Internal Methods


		#region Private Methods

		private static KeyCode StringtoKeyCode(string keycode)
		{
			switch (keycode.ToLower())
			{
				case "space": return KeyCode.Space;
			}
			return KeyCode.None;
		}

		#endregion Private Methods
	}
}

