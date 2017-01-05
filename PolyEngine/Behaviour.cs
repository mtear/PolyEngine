namespace PolyEngine
{
	public class Behaviour : Component
	{

		#region Public Variables

		public bool enabled
		{
			get
			{
				return mEnabled;
			}
			set
			{
				mEnabled = value;
				EnabledChanged();
			}
		}

		public bool isActiveAndEnabled
		{
			get
			{
				return enabled && gameObject.activeSelf;
			}
		}

		#endregion Public Variables


		#region Private Variables

		private bool mEnabled = true;

		#endregion Private Variables


		#region Protected Methods

		protected virtual void EnabledChanged() { }

		#endregion Protected Methods


	}
}

