using System;
using System.Collections.Generic;
using XNA = Microsoft.Xna.Framework;

namespace PolyEngine
{
	public class Transform : Component
	{


		#region Public Variables

		//childCount The number of children the Transform has.
		public int childCount
		{
			get { return children.Count; }
		}

		//eulerAngles The rotation as Euler angles in degrees.
		public Vector3 eulerAngles
		{
			get
			{
				return mRotation.eulerAngles;
			}
			set
			{
				mRotation.eulerAngles = value;
				if (gameObject == null) return;
				if (gameObject.rigidBody == null) return;
				gameObject.rigidBody.SetRotation(value.z);
			}
		}

		//forward The blue axis of the transform in world space.
		//hasChanged Has the transform changed since the last time the flag was set to 'false'?
		//hierarchyCapacity The transform capacity of the transform's hierarchy data structure.
		//hierarchyCount The number of transforms in the transform's hierarchy data structure.
		//localEulerAngles The rotation as Euler angles in degrees relative to the parent transform's rotation.
		public Vector3 localEulerAngles
		{
			get
			{
				return mLocalRotation.eulerAngles;
			}
			set
			{
				mLocalRotation.eulerAngles = value;
			}
		}

		//localPosition Position of the transform relative to the parent transform.
		public Vector3 localPosition
		{
			get
			{
				return mLocalPosition;
			}
			set
			{
				mLocalPosition = value;
			}
		}

		//localRotation The rotation of the transform relative to the parent transform's rotation.
		public Quaternion localRotation
		{
			get
			{
				return mLocalRotation;
			}
			set
			{
				mLocalRotation = value;
			}
		}

		//localScale The scale of the transform relative to the parent.
		public Vector3 localScale
		{
			get
			{
				return mLocalScale;
			}
			set
			{
				mLocalScale = value;
				mScale = value;
			}
		}

		//localToWorldMatrix Matrix that transforms a point from local space into world space (Read Only).
		//lossyScale The global scale of the object (Read Only).
		public Vector3 lossyScale
		{
			get
			{
				return mScale;
			}
		}

		//parent The parent of the transform.
		public Transform parent
		{
			get
			{
				return mParent;
			}
			set
			{
				mParent = value;
				gameObject.scene.UpdateSceneParent(gameObject);
			}
		}

		//position    The position of the transform in world space.
		public Vector3 position
		{
			get
			{
				return mPosition;
			}
			set
			{
				mPosition = value;
			}
		}

		//right The red axis of the transform in world space.
		//root Returns the topmost transform in the hierarchy.
		public Transform root
		{
			get
			{
				Transform t = this.parent, t2 = this;
				do
				{
					if (t == null) return t2;
					t2 = t; t = t.parent;
				} while (true);
			}
		}

		//rotation The rotation of the transform in world space stored as a Quaternion.
		public Quaternion rotation
		{
			get
			{
				return mRotation;
			}
			set
			{
				mRotation = value;
			}
		}

		//up The green axis of the transform in world space.
		//worldToLocalMatrix Matrix that transforms a point from world space into local space (Read Only).

		#endregion Public Variables


		#region Private Variables

		private List<Transform> children = new List<Transform>();

		private Transform mParent = null;

		private Vector3 mPosition = Vector3.zero;
		private Vector3 mLocalPosition = Vector3.zero;
		private Vector3 mScale = Vector3.one;
		private Vector3 mLocalScale = Vector3.one;

		private Quaternion mRotation = new Quaternion(), mLocalRotation = new Quaternion();

		#endregion Private Variables


		#region Public Functions

		//DetachChildren Unparents all children.
		public void DetachChildren()
		{
			foreach (Transform t in children)
			{
				t.parent = null;
			}
		}

		//Find    Finds a child by name and returns it. TODO path name slashes /
		//TODO Check how this works in Unity. Is it from the GameObject?
		public Transform Find(string name)
		{
			foreach (Transform t in children)
			{
				if (t.name == name) return t;
			}
			return null;
		}

		//GetChild Returns a transform child by index.
		public Transform GetChild(int index)
		{
			if (index >= childCount) throw new Exception("index must be lower than childCount!");
			return children[index];
		}

		//GetSiblingIndex Gets the sibling index. TODO check how this works if unparented

		//InverseTransformDirection Transforms a direction from world space to local space. The opposite of Transform.TransformDirection.
		//InverseTransformPoint Transforms position from world space to local space.
		//InverseTransformVector Transforms a vector from world space to local space. The opposite of Transform.TransformVector.
		//IsChildOf Is this transform a child of parent?
		public bool IsChildOf(Transform trans)
		{
			Transform t = this.parent;
			while (t != null)
			{
				if (t == trans) return true;
				t = t.parent;
			}
			return false;
		}

		//LookAt  Rotates the transform so the forward vector points at /target/'s current position.
		//Rotate Applies a rotation of eulerAngles.z degrees around the z axis, eulerAngles.x degrees around the x axis, and eulerAngles.y degrees around the y axis (in that order).
		public void Rotate(Vector3 eulerAngles) //TODO full overloads
		{
			Vector3 e = this.eulerAngles;
			e.z += eulerAngles.z;
			e.x += eulerAngles.x;
			e.y += eulerAngles.y;
			this.eulerAngles = e;
			localEulerAngles = e;
		}

		//RotateAround Rotates the transform about axis passing through point in world coordinates by angle degrees.
		//SetAsFirstSibling Move the transform to the start of the local transform list.
		//SetAsLastSibling    Move the transform to the end of the local transform list.
		//SetParent Set the parent of the transform.


		//SetSiblingIndex Sets the sibling index.
		//TransformDirection Transforms direction from local space to world space.
		//TransformPoint Transforms position from local space to world space.
		//TransformVector Transforms vector from local space to world space.
		//Translate Moves the transform in the direction and distance of translation.
		public void Translate(Vector3 translation) //TODO full overloads
		{
			mPosition += translation;
			mLocalPosition += translation;
		}

		#endregion Public Functions


	}
}

