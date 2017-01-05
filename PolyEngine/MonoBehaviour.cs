using System;
namespace PolyEngine
{
	public class MonoBehaviour : Behaviour
	{
		private bool calledAwake = false;

		public MonoBehaviour()
		{
		}

		public virtual void Awake() { }

		public virtual void Start() { }

		public virtual void Update() { }

		public virtual void OnEnable() { }

		public virtual void OnApplicationPause(bool pauseStatus) { }

		public virtual void FixedUpdate() { } //TODO make this work like unity

		public virtual void LateUpdate() { }

		public virtual void OnPreCull() { }

		public virtual void OnBecameInvisible() { }

		public virtual void OnBecameVisible() { }

		public virtual void OnWillRenderObject() { }

		public virtual void OnPreRender() { }

		public virtual void OnRenderObject() { }

		public virtual void OnPostRender() { }

		public virtual void OnRenderImage() { }

		public virtual void OnDestroy() { }

		public virtual void OnApplicationQuit() { }

		public virtual void OnDisable() { }

		protected override void EnabledChanged()
		{
			if (!calledAwake)
			{
				calledAwake = true;
				Awake();
			}
			if (isActiveAndEnabled) OnEnable();
			else if (!enabled && gameObject.activeInHeiarchy)
				OnDisable();
		}

		internal void CallAwake()
		{
			calledAwake = true;
			Awake();
		}

	}
}

