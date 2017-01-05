using System;
using PolyEngine.Rendering;

namespace PolyEngine
{
	public class Renderer : Component
	{


		#region Public Variables

		//bounds The bounding volume of the renderer(Read Only).
		public Bounds bounds
		{
			get
			{
				return GetBounds();
			}
		}

		//enabled Makes the rendered 3D object visible if enabled.
		public bool enabled = true;

		//isPartOfStaticBatch Has this renderer been statically batched with any other renderers?
		//isVisible Is this renderer visible in any camera? (Read Only)
		public bool isVisible
		{
			get
			{
				return mVisible;
			}
		}

		//lightmapIndex The index of the baked lightmap applied to this renderer.
		//lightmapScaleOffset The UV scale & offset used for a lightmap.
		//lightProbeProxyVolumeOverride If set, the Renderer will use the Light Probe Proxy Volume component attached to the source game object.
		//lightProbeUsage The light probe interpolation type.
		//localToWorldMatrix Matrix that transforms a point from local space into world space (Read Only).
		//material Returns the first instantiated Material assigned to the renderer.
		//materials Returns all the instantiated materials of this object.
		//motionVectors Specifies whether this renderer has a per-object motion vector pass.
		//probeAnchor If set, Renderer will use this Transform's position to find the light or reflection probe.
		//realtimeLightmapIndex The index of the realtime lightmap applied to this renderer.
		//realtimeLightmapScaleOffset The UV scale & offset used for a realtime lightmap.
		//receiveShadows Does this object receive shadows?
		//reflectionProbeUsage    Should reflection probes be used for this Renderer?
		//shadowCastingMode   Does this object cast shadows?
		//sharedMaterial  The shared material of this object.
		//sharedMaterials All the shared materials of this object.
		//sortingLayerID Unique ID of the Renderer's sorting layer.
		//sortingLayerName Name of the Renderer's sorting layer.
		//sortingOrder Renderer's order within a sorting layer.
		//worldToLocalMatrix Matrix that transforms a point from world space into local space (Read Only).

		#endregion Public Variables


		#region Private Variables

		private bool mVisible = true;

		#endregion Private Variables


		#region Constructors

		public Renderer()
		{
			GraphicsEngine.AddRenderComponent(this);
		}

		#endregion Constructors


		#region Public Methods

		//GetClosestReflectionProbes Returns an array of closest reflection probes with weights, weight shows how much influence the probe has on the renderer, this value is also used when blending between reflection probes occur.
		//GetPropertyBlock    Get per-renderer material property block.
		//SetPropertyBlock Lets you add per-renderer material parameters without duplicating a material.

		#endregion Public Methods


		#region Protected Methods

		protected virtual Bounds GetBounds()
		{
			return new Bounds();
		}

		#endregion Protected Methods


		#region Messages

		//OnBecameInvisible OnBecameInvisible is called when the object is no longer visible by any camera.
		public virtual void OnBecameInvisible() { }

		//OnBecameVisible OnBecameVisible is called when the object became visible by any camera.
		public virtual void OnBecameVisible() { }

		#endregion Messages


		#region Internal Methods

		internal virtual void OnRender() { }

		internal virtual void SetVisible(bool visible)
		{
			if (visible == mVisible) return;
			else { //Change
				mVisible = visible;
				if (gameObject.activeInHeiarchy)
				{
					if (visible) OnBecameVisible();
					else OnBecameInvisible();
					foreach (MonoBehaviour script in gameObject.scripts)
					{
						if (!script.enabled) continue;
						if (visible)
						{
							script.OnBecameVisible();
						}
						else {
							script.OnBecameInvisible();
						}
					}
				}
			}
		}

		#endregion Internal Methods


	}
}

