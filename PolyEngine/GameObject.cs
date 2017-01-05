using System;
using System.Collections.Generic;
using PolyEngine.SceneManagement;

namespace PolyEngine
{
	public class GameObject : Object
	{

		#region Public Variables

		//activeInHeiarchy
		public bool activeInHeiarchy
		{
			get
			{
				return true;
			}
		}

		public bool activeSelf //TODO differentiate maybe need a method when parent is decoupled?! recalculate!?
		{
			get
			{
				return active;
			}
		}

		//isStatic

		//layer

		public Scene scene;

		public string tag = "Untagged";

		public Transform transform = new Transform(); //TODO parent child changes

		#endregion Public Variables


		#region Private Variables

		protected bool active = false;

		protected List<Component> components = new List<Component>(); //TODO make into a dictionary, set?

		internal List<MonoBehaviour> scripts = new List<MonoBehaviour>();

		internal List<Collider2D> colliders = new List<Collider2D>();

		#endregion Private Variables


		#region Internal Variables

		internal Rigidbody2D rigidBody = null;

		#endregion Internal Variables


		#region Constructors

		//TODO Add to Scene when created
		public GameObject()
		{ //TODO Add transform when created
			this.name = "New Game Object";
			transform = new Transform();
			transform.gameObject = this;
			SceneManager.CreateGameObject(this);
		}

		public GameObject(string name)
		{
			this.name = name;
			transform = new Transform();
			transform.gameObject = this;
			SceneManager.CreateGameObject(this);
		}

		public GameObject(string name, params Type[] components)
		{
			transform = new Transform();
			transform.gameObject = this;
			foreach (Type t in components)
			{
				AddComponent(t);
			}
			SceneManager.CreateGameObject(this);
		}

		#endregion Constructors


		#region Public Functions

		//AddComponent Adds a component class named className to the game object.
		public Component AddComponent(string className)
		{
			//TODO Components that require other components
			Type type = Type.GetType(className);
			return AddComponent(type);
		}

		public T AddComponent<T>() where T : Component
		{
			return (T)AddComponent(typeof(T));
		}

		//BroadcastMessage Calls the method named methodName on every MonoBehaviour in this game object or any of its children.

		//CompareTag  Is this game object tagged with tag ?
		public bool CompareTag(string tag)
		{
			return this.tag == tag;
		}

		//GetComponent Returns the component of Type type if the game object has one attached, null if it doesn't.
		public Component GetComponent(Type type)
		{
			foreach (Component c in components)
			{
				if (c.GetType() == type) return c;
			}

			return null;
		}

		public T GetComponent<T>() where T : Component
		{ //TODO Fix this shit
			Type type = typeof(T);
			foreach (Component c in components)
			{
				if (c.GetType() == type) return (T)c;
			}
			return null;
		}

		//GetComponentInChildren Returns the component of Type type in the GameObject or any of its children using depth first search.
		public Component GetComponentInChildren(Type type)
		{
			return GetComponentInChildren(type, false);
		}

		public Component GetComponentInChildren(Type type, bool includeInactive)
		{
			if (!activeInHeiarchy) return null; //TODO make sure this gets updated

			Component ret = GetComponent(type);
			while (ret == null)
			{
				for (int i = 0; i < transform.childCount; i++)
				{
					ret = transform.GetChild(i).gameObject.GetComponentInChildren(type);
					if (ret != null) return ret;
				}
			}

			return ret;
		}

		//GetComponentInParent    Returns the component of Type type in the GameObject or any of its parents.
		public Component GetComponentInParent(Type type)
		{
			Component ret = (activeInHeiarchy) ? GetComponent(type) : null;
			if (ret != null) return ret;
			if (transform.parent == null) return null;

			return transform.parent.gameObject.GetComponentInParent(type);
		}

		//GetComponents Returns all components of Type type in the GameObject.
		//GetComponentsInChildren Returns all components of Type type in the GameObject or any of its children.
		//GetComponentsInParent Returns all components of Type type in the GameObject or any of its parents.
		//SendMessage Calls the method named methodName on every MonoBehaviour in this game object.
		//SendMessageUpwards Calls the method named methodName on every MonoBehaviour in this game object and on every ancestor of the behaviour.
		//SetActive Activates/Deactivates the GameObject. TODO Does this affect children!?!?

		#endregion Public Functions


		#region Static Functions

		//CreatePrimitive Creates a game object with a primitive mesh renderer and appropriate collider.
		//Find Finds a game object by name and returns it.
		//FindGameObjectsWithTag  Returns a list of active GameObjects tagged tag. Returns empty array if no GameObject was found.
		//FindWithTag Returns one active GameObject tagged tag.Returns null if no GameObject was found.

		#endregion Static Functions


		#region Private Functions

		private Component AddComponent(Type type)
		{
			Component myObject = (Component)Activator.CreateInstance(type);
			myObject.gameObject = this;

			if (type == typeof(Rigidbody2D))
			{
				rigidBody = (Rigidbody2D)myObject;
				rigidBody.position = transform.position;
			}

			if (type.IsSubclassOf(typeof(MonoBehaviour)))
			{
				scripts.Add(myObject as MonoBehaviour);
			}
			else if (type.IsSubclassOf(typeof(Collider2D)))
			{
				Collider2D cmo = myObject as Collider2D;
				cmo.computeInertia();
				colliders.Add(cmo);
			}
			components.Add(myObject);
			return myObject;
		}

		#endregion Private Functions


	}
}

