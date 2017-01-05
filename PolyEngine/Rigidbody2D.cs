using System;
using PolyEngine.InternalPhysics;

namespace PolyEngine
{
	public class Rigidbody2D : Component
	{


		#region Public Variables

		//angularDrag Coefficient of angular drag.
		public float angularDrag = 0.05f;

		//angularVelocity Angular velocity in degrees per second.
		public float angularVelocity = 0;

		//centerOfMass The center of mass of the rigidBody in local space.
		//collisionDetectionMode  The method used by the physics engine to check if two objects have collided.
		//constraints Controls which degrees of freedom are allowed for the simulation of this Rigidbody2D.
		//drag Coefficient of drag.
		public float drag = 0;

		//freezeRotation Controls whether physics will change the rotation of the object.
		public bool freezeRotation = false;

		//gravityScale The degree to which this object is affected by gravity.
		public float gravityScale = 1;

		//inertia The rigidBody rotational inertia.
		public float inertia
		{
			get
			{
				return mInertia;
			}
			set
			{
				//todo make this safe
				mInertia = value;
				if (value == 0) invInertia = 0;
				else invInertia = 1f / value;
			}
		}

		//interpolation Physics interpolation used between updates.
		//isKinematic Should this rigidbody be taken out of physics control?
		public bool isKinematic = false;

		//mass    Mass of the rigidbody.
		public float mass
		{
			get
			{
				return mMass;
			}
			set
			{
				mMass = value;
				if (value == 0) invMass = 0;
				else invMass = 1f / value;
				if (!mUseAutoMass)
				{
					Collider2D c = gameObject.GetComponent<Collider2D>();
					if(c != null) c.computeInertia();
				}
			}
		}

		//position The position of the rigidbody.
		public Vector2 position
		{
			get
			{
				return mPosition;
			}
			set
			{
				mPosition = value;
				transform.position = value;
			}
		}
		//rotation The rotation of the rigidbody.
		public float rotation
		{
			get
			{
				return mRotation;
			}
			set
			{ //todo transform rotation
				if (value == 0) return;
				float delta = value - mRotation;
				if (delta == 0) return;
				mRotation = value;
				if(delta != 0)
					transform.Rotate(new Vector3(0, 0, delta));
			}
		}

		//simulated Indicates whether the rigid body should be simulated or not by the physics system.
		//sleepMode The sleep state that the rigidbody will initially be in.
		//useAutoMass Should the total rigid-body mass be automatically calculated from the[[Collider2D.density]] of attached colliders?
		public bool useAutoMass
		{
			get
			{
				return mUseAutoMass;
			}
			set
			{
				mUseAutoMass = value;
			}
		}

		//velocity    Linear velocity of the rigidbody.
		public Vector2 velocity
		{
			get
			{
				return mVelocity;
			}
			set
			{
				mVelocity = value;
			}
		}

		//worldCenterOfMass Gets the center of mass of the rigidBody in global space.

		#endregion Public Variables


		#region Internal Variables

		internal float invertedMass
		{
			get
			{
				return invMass;
			}
		}

		internal float invertedInertia
		{
			get
			{
				return invInertia;
			}
		}

		#endregion Internal Variables


		#region Private Variables

		private float mRotation = 0;

		private bool mUseAutoMass = false;
		private Vector2 mPosition = Vector2.zero, mVelocity = Vector2.zero, mForces = Vector2.zero;

		private float mMass = 1, invMass = 1, mInertia = 0, invInertia = 0;

		#endregion Private Variables


		#region Constructors

		public Rigidbody2D()
		{
			Physics2D.AddPhysicsComponent(this);
		}

		#endregion Constructors


		#region Public Methods

		//AddForce Apply a force to the rigidbody.
		//AddForceAtPosition Apply a force at a given position in space.
		//AddRelativeForce Adds a force to the rigidbody2D relative to its coordinate system.
		//AddTorque   Apply a torque at the rigidbody's centre of mass.
		//Cast All the Collider2D shapes attached to the Rigidbody2D are cast into the scene starting at each collider position ignoring the colliders attached to the same Rigidbody2D.
		//GetPoint Get a local space point given the point point in rigidBody global space.
		//GetPointVelocity The velocity of the rigidbody at the point Point in global space.
		//GetRelativePoint Get a global space point given the point relativePoint in rigidBody local space.
		//GetRelativePointVelocity The velocity of the rigidbody at the point Point in local space.
		//GetRelativeVector Get a global space vector given the vector relativeVector in rigidBody local space.
		//GetVector Get a local space vector given the vector vector in rigidBody global space.
		//IsAwake Is the rigidbody "awake"?
		//IsSleeping Is the rigidbody "sleeping"?
		//IsTouching Check whether any of the collider(s) attached to this rigidbody are touching the collider or not.
		//IsTouchingLayers Checks whether any of the collider(s) attached to this rigidbody are touching any colliders on the specified layerMask or not.
		//MovePosition Moves the rigidbody to position.
		//MoveRotation    Rotates the rigidbody to angle (given in degrees).
		//OverlapPoint Check if any of the Rigidbody2D colliders overlap a point in space.
		//Sleep Make the rigidbody "sleep".
		//WakeUp Disables the "sleeping" state of a rigidbody.

		#endregion Public Methods


		#region Internal Methods

		internal void AddImpulse(Vector2 impulse, Vector2 contactVector)
		{
			// velocity += im * impulse;
			// angularVelocity += iI * Cross( contactVector, impulse );
			velocity += impulse * invMass;
			float addval = invInertia * VectorMath.cross(contactVector, impulse) * Mathf.Rad2Deg;
			angularVelocity += addval;
		}

		internal void PhysicsUpdate()
		{
			mPosition = transform.position;
			if (isKinematic) return;

			mVelocity += (mForces * invMass + gravityScale * Physics2D.gravity) * Time.fixedDeltaTime;
			mVelocity *= 1f / (1f + Time.fixedDeltaTime * drag);
			position += mVelocity * Time.fixedDeltaTime;

			//TODO torque, w += h * b->m_invI * b->m_torque;
			//angularVelocity *= 1.0f / (1.0f + Time.fixedDeltaTime * angularDrag);
			rotation += angularVelocity * Time.fixedDeltaTime;

			mForces = Vector2.zero;
		}

		internal void SetRotation(float rot)
		{
			mRotation = rot;
		}

		#endregion Internal Methods


	}
}

