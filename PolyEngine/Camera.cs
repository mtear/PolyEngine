using System.Linq;
using Microsoft.Xna.Framework;
using PolyEngine.Rendering;

namespace PolyEngine
{
	public class Camera : Behaviour
	{


		#region Static Variables

		//allCameras Returns all enabled cameras in the scene.
		public Camera[] allCameras
		{
			get
			{
				return GraphicsEngine.cameras.Where(x => x.enabled).ToArray();
			}
		}

		//allCamerasCount The number of cameras in the current scene.
		public int allCamerasCount{
			get{
				return GraphicsEngine.cameras.Count;
			}
		}

		//current The camera we are currently rendering with, for low-level render control only (Read Only).
		public Camera current
		{
			get
			{
				return GraphicsEngine.currentCamera;
			}
		}

		//main The first enabled camera tagged "MainCamera" (Read Only).
		public Camera main
		{
			get
			{
				foreach (Camera c in GraphicsEngine.cameras)
				{
					if (c.CompareTag("MainCmaera") && c.enabled)
						return c;
				}
				throw new System.Exception("No main camera!");
			}
		}

		//onPostRender Event that is fired after any camera finishes rendering.
		//onPreCull   Event that is fired before any camera starts culling.
		//onPreRender Event that is fired before any camera starts rendering.

		#endregion Static Variables


		#region Public Variables

		//actualRenderingPath The rendering path that is currently being used(Read Only).The actual rendering path might be different from the user-specified renderingPath if the underlying gpu/platform does not support the requested one, or some other situation caused a fallback(for example, deferred rendering is not supported with orthographic projection cameras).
		//aspect The aspect ratio(width divided by height).
		//backgroundColor The color with which the screen will be cleared.
		public Color backgroundColor = new Color(49, 77, 121, 255);

		//cameraToWorldMatrix Matrix that transforms from camera space to world space (Read Only).
		//cameraType Identifies what kind of camera this is.
		//clearFlags How the camera clears the background.
		//clearStencilAfterLightingPass Should the camera clear the stencil buffer after the deferred light pass?
		//commandBufferCount  Number of command buffers set up on this camera (Read Only).
		//cullingMask This is used to render parts of the scene selectively.
		//cullingMatrix Sets a custom matrix for the camera to use for all culling queries.
		//depth Camera's depth in the camera rendering order.
		public float depth = 0;

		//depthTextureMode How and if camera generates a depth texture.
		//eventMask Mask to select which layers can trigger events on the camera.
		//farClipPlane The far clipping plane distance.
		//fieldOfView The field of view of the camera in degrees.
		//hdr High dynamic range rendering.
		//layerCullDistances Per-layer culling distances.
		//layerCullSpherical How to perform per-layer culling for a Camera.
		//nearClipPlane The near clipping plane distance.
		//nonJitteredProjectionMatrix Get or set the raw projection matrix with no camera offset (no jittering).
		//opaqueSortMode Opaque object sorting mode.
		//orthographic Is the camera orthographic(true) or perspective(false)?
		public bool orthographic = true;

		//orthographicSize Camera's half-size when in orthographic mode.
		public int orthographicSize = 5;

		//pixelHeight How tall is the camera in pixels(Read Only).
		public int pixelHeight
		{
			get
			{
				return (int)pixelRect.height;
			}
		}

		//pixelRect Where on the screen is the camera rendered in pixel coordinates.
		public Rect pixelRect
		{
			get
			{
				return mPixelRect;
			}
		}

		//pixelWidth  How wide is the camera in pixels (Read Only).
		public int pixelWidth
		{
			get
			{
				return (int)pixelRect.width;
			}
		}

		//projectionMatrix Set a custom projection matrix.
		//rect    Where on the screen is the camera rendered in normalized coordinates.
		public Rect rect
		{
			get
			{
				return mRect;
			}
			set
			{
				mRect = value;
				RecalculatePixelRect();
			}
		}

		//renderingPath The rendering path that should be used, if possible.In some situations, it may not be possible to use the rendering path specified, in which case the renderer will automatically use a different path. For example, if the underlying gpu/platform does not support the requested one, or some other situation caused a fallback (for example, deferred rendering is not supported with orthographic projection cameras).For this reason, we also provide the read-only property actualRenderingPath which allows you to discover which path is actually being used.
		//stereoConvergence Distance to a point where virtual eyes converge.
		//stereoEnabled   Stereoscopic rendering.
		//stereoMirrorMode Render only once and use resulting image for both eyes.
		//stereoSeparation Distance between the virtual eyes.
		//stereoTargetEye When Virtual Reality is enabled, the stereoTargetEye value determines which eyes of the Head Mounted Display(HMD) this camera renders to.The default is to render both eyes.The values passed to stereoTargetEye are found in the StereoTargetEyeMask enum. Every camera will render to the Main Game Window by default. If you do not want to see the content from this camera in the Main Game Window, use a camera with a higher depth value than this camera, or set the Camera's showDeviceView value to false.
		//targetDisplay Set the target display for this Camera.
		//targetTexture Destination render texture.
		//transparencySortMode Transparent object sorting mode.
		//useOcclusionCulling Whether or not the Camera will use occlusion culling during rendering.
		//velocity Get the world-space speed of the camera (Read Only).
		//worldToCameraMatrix Matrix that transforms from world to camera space.

		#endregion Public Variables


		#region Private Variables

		Rect mPixelRect;
		Rect mRect = new Rect(0, 0, 1, 1);

		#endregion Private Variables


		#region Constructors

		public Camera()
		{
			RecalculatePixelRect();
			GraphicsEngine.AddCamera(this);
		}

		#endregion Constructors


		#region Public Methods

		//AddCommandBuffer Add a command buffer to be executed at a specified place.
		//CalculateObliqueMatrix  Calculates and returns oblique near-plane projection matrix.
		//CopyFrom Makes this camera's settings match other camera.
		//GetCommandBuffers Get command buffers to be executed at a specified place.
		//GetStereoProjectionMatrix Get the projection matrix of a specific stereo eye.
		//GetStereoViewMatrix Get the view matrix of a specific stereo eye.
		//RemoveAllCommandBuffers Remove all command buffers set on this camera.
		//RemoveCommandBuffer Remove command buffer from execution at a specified place.
		//RemoveCommandBuffers Remove command buffers from execution at a specified place.
		//Render Render the camera manually.
		//RenderToCubemap Render into a static cubemap from this camera.
		//RenderWithShader Render the camera with shader replacement.
		//ResetAspect Revert the aspect ratio to the screen's aspect ratio.
		//ResetCullingMatrix Make culling queries reflect the camera's built in parameters.
		//ResetFieldOfView Reset to the default field of view.
		//ResetProjectionMatrix Make the projection reflect normal camera's parameters.
		//ResetReplacementShader Remove shader replacement from camera.
		//ResetStereoProjectionMatrices   Use the default projection matrix for both stereo eye.Only work in 3D flat panel display.
		//ResetStereoViewMatrices Use the default view matrix for both stereo eye.Only work in 3D flat panel display.
		//ResetWorldToCameraMatrix Make the rendering position reflect the camera's position in the scene.
		//ScreenPointToRay Returns a ray going from camera through a screen point.
		//ScreenToViewportPoint Transforms position from screen space into viewport space.
		//ScreenToWorldPoint Transforms position from screen space into world space.
		//SetReplacementShader Make the camera render with shader replacement.
		//SetStereoProjectionMatrices Set custom projection matrices for both eyes.
		//SetStereoProjectionMatrix Sets a custom stereo projection matrix.
		//SetStereoViewMatrices Set custom view matrices for both eyes.
		//SetStereoViewMatrix Sets a custom stereo view matrix.
		//SetTargetBuffers Sets the Camera to render to the chosen buffers of one or more RenderTextures.
		//ViewportPointToRay Returns a ray going from camera through a viewport point.
		//ViewportToScreenPoint Transforms position from viewport space into screen space.
		//ViewportToWorldPoint Transforms position from viewport space into world space.
		//WorldToScreenPoint Transforms position from world space into screen space.
		//WorldToViewportPoint Transforms position from world space into viewport space.

		#endregion Public Methods


		#region Static Functions

		//GetAllCameras Fills an array of Camera with the current cameras in the scene, without allocating a new array.

		#endregion Static Functions


		#region Internal Methods

		internal void RecalculatePixelRect()
		{
			float width = Screen.currentResolution.width * rect.width;
			float height = Screen.currentResolution.height * rect.height;
			float x = Screen.currentResolution.width * rect.xMin;
			float y = Screen.currentResolution.height * rect.yMin;
			mPixelRect = new Rect(x, y, width, height);
		}

		#endregion Internal Methods


		#region Messages

		//OnPostRender OnPostRender is called after a camera has finished rendering the scene.
		//OnPreCull OnPreCull is called before a camera culls the scene.
		//OnPreRender OnPreRender is called before a camera starts rendering the scene.
		//OnRenderImage   OnRenderImage is called after all rendering is complete to render image.
		//OnRenderObject OnRenderObject is called after camera has rendered the scene.
		//OnWillRenderObject OnWillRenderObject is called for each camera if the object is visible.

		#endregion Messages


		#region Delegates

		//CameraCallback Delegate type for camera callbacks.

		#endregion Delegates
	}
}

