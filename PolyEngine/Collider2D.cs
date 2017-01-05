namespace PolyEngine
{
	public class Collider2D : Behaviour
	{


		#region Public Variables

		//attachedRigidbody The Rigidbody2D attached to the Collider2D's GameObject.
		public Rigidbody2D attachedRigidBody
		{
			get
			{
				return gameObject.rigidBody;
			}
		}

		//bounds The world space bounding area of the collider.
		//density The density of the collider used to calculate its mass(when auto mass is enabled).
		//isTrigger Is this collider configured as a trigger?
		public bool isTrigger = false;

		//offset The local offset of the collider geometry.
		//shapeCount  The number of separate shaped regions in the collider.
		//sharedMaterial The [[PhysicsMaterial2D that is applied to this collider.
		public PhysicsMaterial2D sharedMaterial = PhysicsMaterial2D.FrictionlessNonElastic();

		//usedByEffector  Whether the collider is used by an attached effector or not.

		#endregion Public Variables


		#region Internal Variables

		internal float InvertedInertia
		{
			get
			{
				if (attachedRigidBody != null)
					return attachedRigidBody.invertedInertia;
				else return 0;
			}
		}

		internal float InvertedMass
		{
			get
			{
				if (attachedRigidBody != null)
					return attachedRigidBody.invertedMass;
				else return 0;
			}
		}

		internal Vector2 position
		{
			get
			{
				if (attachedRigidBody != null)
					return attachedRigidBody.position;
				else return (Vector2)transform.position;
			}
		}

		#endregion Internal Variables


		#region Constructors

		public Collider2D()
		{
			Physics2D.AddPhysicsComponent(this);
		}

		#endregion Constructors


		#region Public Methods

		//Cast Casts the collider shape into the scene starting at the collider position ignoring the collider itself.
		//IsTouching Check whether this collider is touching the collider or not.
		public virtual bool IsTouching(Collider2D collider)
		{
			return false;
		}

		//IsTouchingLayers Checks whether this collider is touching any colliders on the specified layerMask or not.
		//OverlapPoint Check if a collider overlaps a point in space.
		//Raycast Casts a ray into the scene starting at the collider position ignoring the collider itself.

		#endregion Public Methods


		#region Internal Methods

		internal virtual void setOrient(float radians){}

		internal virtual void computeAutoMass(float density) { }

		internal virtual void computeInertia() { }

		internal virtual void RenderGizmo() { }

		#endregion Internal Methods


	}
}

