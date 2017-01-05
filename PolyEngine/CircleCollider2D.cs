using System;
namespace PolyEngine
{
	public class CircleCollider2D : Collider2D
	{
		public float radius = 1;

		public override bool IsTouching(Collider2D collider)
		{
			if (collider.GetType() == typeof(CircleCollider2D))
			{
				CircleCollider2D other = collider as CircleCollider2D;

				// Vector from A to B
				Vector2 n = collider.position
				                    - collider.position;

				float r = this.radius + other.radius;
				r *= r;

				return (Vector2.SqrMagnitude(n) > r);
			}
			return false;
		}

		internal override void computeAutoMass(float density)
		{
			if (attachedRigidBody == null) return;
			attachedRigidBody.mass = Mathf.PI * radius * radius * density;
			attachedRigidBody.inertia = attachedRigidBody.mass * radius * radius;
		}

		internal override void computeInertia()
		{
			if (attachedRigidBody == null) return;
			//attachedRigidBody.inertia = attachedRigidBody.mass * radius * radius;
			attachedRigidBody.inertia = 0.5f * attachedRigidBody.mass * radius * radius;
		}

		internal override void RenderGizmo()
		{
			PolyEngine.Rendering.GraphicsEngine.DrawCircle(transform.position, radius, Microsoft.Xna.Framework.Color.Blue);
			Vector2 opoint = new Vector2(transform.position.x, transform.position.y);
			if (attachedRigidBody == null) return;
			opoint.x += Mathf.Cos((attachedRigidBody.rotation) * Mathf.Deg2Rad) * radius;
			opoint.y += Mathf.Sin((attachedRigidBody.rotation) * Mathf.Deg2Rad) * radius;
			PolyEngine.Rendering.GraphicsEngine.DrawLine(transform.position, opoint);
		}
	}
}

