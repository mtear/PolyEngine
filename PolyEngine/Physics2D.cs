using System;
using System.Collections.Generic;
using PolyEngine.InternalPhysics;

namespace PolyEngine
{
	public class Physics2D
	{


		#region Static Variables


		//AllLayers Layer mask constant that includes all layers.
		//alwaysShowColliders Should the collider gizmos always be shown even when they are not selected?
		//angularSleepTolerance   A rigid-body cannot sleep if its angular velocity is above this tolerance.
		//baumgarteScale The scale factor that controls how fast overlaps are resolved.
		//baumgarteTOIScale The scale factor that controls how fast TOI overlaps are resolved.
		//changeStopsCallbacks Whether or not to stop reporting collision callbacks immediately if any of the objects involved in the collision are deleted/moved.
		//colliderAsleepColor The color used by the gizmos to show all asleep colliders (collider is asleep when the body is asleep).
		//colliderAwakeColor The color used by the gizmos to show all awake colliders(collider is awake when the body is awake).
		//colliderContactColor The color used by the gizmos to show all collider contacts.
		//contactArrowScale   The scale of the contact arrow used by the collider gizmos.
		//DefaultRaycastLayers Layer mask constant that includes all layers participating in raycasts by default.
		//gravity Acceleration due to gravity.
		public static Vector2 gravity = new Vector2(0, 0f);

		//IgnoreRaycastLayer Layer mask constant for the default layer that ignores raycasts.
		//linearSleepTolerance A rigid-body cannot sleep if its linear velocity is above this tolerance.
		//maxAngularCorrection The maximum angular position correction used when solving constraints. This helps to prevent overshoot.
		//maxLinearCorrection The maximum linear position correction used when solving constraints. This helps to prevent overshoot.
		//maxRotationSpeed The maximum angular speed of a rigid-body per physics update. Increasing this can cause numerical problems.
		//maxTranslationSpeed The maximum linear speed of a rigid-body per physics update. Increasing this can cause numerical problems.
		//minPenetrationForPenalty The minimum contact penetration radius allowed before any separation impulse force is applied.Extreme caution should be used when modifying this value as making this smaller means that polygons will have an insufficient buffer for continuous collision and making it larger may create artefacts for vertex collision.
		//positionIterations The number of iterations of the physics solver when considering objects' positions.
		//queriesHitTriggers Do raycasts detect Colliders configured as triggers?
		//queriesStartInColliders Do ray/line casts that start inside a collider(s) detect those collider(s)?
		//showColliderContacts Should the collider gizmos show current contacts for each collider?
		//showColliderSleep Should the collider gizmos show the sleep-state for each collider?
		//timeToSleep The time in seconds that a rigid-body must be still before it will go to sleep.
		//velocityIterations  The number of iterations of the physics solver when considering objects' velocities.
		//velocityThreshold Any collisions with a relative linear velocity below this threshold will be treated as inelastic.


		#endregion Static Variables


		#region Private Variables

		private static List<Rigidbody2D> rigidBodies = new List<Rigidbody2D>();
		private static List<Collider2D> colliders = new List<Collider2D>();

		#endregion Private Variables


		#region Static Methods

		//BoxCast Casts a box against colliders in the scene, returning the first collider to contact with it.
		//BoxCastAll Casts a box against colliders in the scene, returning all colliders that contact with it.
		//BoxCastNonAlloc Casts a box into the scene, returning colliders that contact with it into the provided results array.
		//CircleCast Casts a circle against colliders in the scene, returning the first collider to contact with it.
		//CircleCastAll Casts a circle against colliders in the scene, returning all colliders that contact with it.
		//CircleCastNonAlloc Casts a circle into the scene, returning colliders that contact with it into the provided results array.
		//GetIgnoreCollision Checks whether the collision detection system will ignore all collisions/triggers between collider1 and collider2 or not.
		//GetIgnoreLayerCollision Should collisions between the specified layers be ignored?
		//GetLayerCollisionMask   Get the collision layer mask that indicates which layer(s) the specified layer can collide with.
		//GetRayIntersection  Cast a 3D ray against the colliders in the scene returning the first collider along the ray.
		//GetRayIntersectionAll Cast a 3D ray against the colliders in the scene returning all the colliders along the ray.
		//GetRayIntersectionNonAlloc Cast a 3D ray against the colliders in the scene returning the colliders along the ray.
		//IgnoreCollision Makes the collision detection system ignore all collisions/triggers between collider1 and collider2.
		//IgnoreLayerCollision Choose whether to detect or ignore collisions between a specified pair of layers.
		//IsTouching Check whether collider1 is touching collider2 or not.
		//IsTouchingLayers Checks whether the collider is touching any colliders on the specified layerMask or not.
		//Linecast Casts a line against colliders in the scene.
		//LinecastAll Casts a line against colliders in the scene.
		//LinecastNonAlloc Casts a line against colliders in the scene.
		//OverlapArea Check if a collider falls within a rectangular area.
		//OverlapAreaAll Get a list of all colliders that fall within a rectangular area.
		//OverlapAreaNonAlloc Get a list of all colliders that fall within a specified area.
		//OverlapBox Check if a collider falls within a box area.
		//OverlapBoxAll Get a list of all colliders that fall within a box area.
		//OverlapBoxNonAlloc Get a list of all colliders that fall within a box area.
		//OverlapCircle Check if a collider falls within a circular area.
		//OverlapCircleAll Get a list of all colliders that fall within a circular area.
		//OverlapCircleNonAlloc Get a list of all colliders that fall within a circular area.
		//OverlapPoint Check if a collider overlaps a point in space.
		//OverlapPointAll Get a list of all colliders that overlap a point in space.
		//OverlapPointNonAlloc Get a list of all colliders that overlap a point in space.
		//Raycast Casts a ray against colliders in the scene.
		//RaycastAll Casts a ray against colliders in the scene, returning all colliders that contact with it.
		//RaycastNonAlloc Casts a ray into the scene.
		//SetLayerCollisionMask Set the collision layer mask that indicates which layer(s) the specified layer can collide with.

		#endregion Static Methods


		#region Internal Static Methods

		internal static void AddPhysicsComponent(Rigidbody2D c)
		{ //TODO get a way to remove them
			rigidBodies.Add(c);
		}

		internal static void AddPhysicsComponent(Collider2D c)
		{
			colliders.Add(c);
		}

		internal static void UpdateAll()
		{
			UpdateRigidbodyPositions();

			InternalPhysicsDriver.step(colliders);

			foreach (Rigidbody2D rb in rigidBodies)
			{
				rb.PhysicsUpdate();
			}
		}

		internal static void Reset()
		{
			rigidBodies.Clear();
		}

		internal static void UpdateRigidbodyPositions()
		{
			foreach (Rigidbody2D r in rigidBodies)
			{
				r.position.Set(r.transform.position.x, r.transform.position.y);
			}

			foreach (Collider2D c in colliders)
			{
				if (c.attachedRigidBody == null) continue;
				c.setOrient((c.attachedRigidBody.rotation)*Mathf.Deg2Rad);
				//TODO euler angles isn't getting reflected in the rigidbody
			}
		}

		internal static void RenderColliders()
		{
			foreach (Collider2D c in colliders)
			{
				c.RenderGizmo();
			}
		}

		#endregion Internal Static Methods


	}
}

