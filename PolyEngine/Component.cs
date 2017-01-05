using System;

namespace PolyEngine
{
	public class Component : Object
	{

		#region Public Variables

		public GameObject gameObject;

		public string tag
		{
			get
			{
				return gameObject.tag;
			}
			set
			{
				gameObject.tag = value;
			}
		}

		public Transform transform
		{
			get
			{
				return gameObject.transform;
			}
			set
			{
				gameObject.transform = value;
			}
		}

		#endregion Public Varibles


		#region Public Functions

		//Broadcast Message

		//CompareTag
		public bool CompareTag(string tag)
		{
			return gameObject.CompareTag(tag);
		}

		//GetComponent
		public Component GetComponent(Type type)
		{
			return gameObject.GetComponent(type);
		}

		//GetComponentInChildren

		//GetComponentInParent

		//GetComponents

		//GetComponentsInParent

		//SendMessage

		//SendMessageUpwards

		#endregion Public Functions


		#region Internal Methods

		internal virtual void InternalUpdate()
		{

		}

		#endregion Internal Methods


	}
}

