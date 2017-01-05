using System;
using Microsoft.Xna.Framework;
using PolyEngine;

namespace PolyPhys
{
	public static class CollisionResolution
	{

		public static bool GenerateManifold_CirclexCircle(Circle A, Circle B, ref Manifold m)
		{
			// Setup a couple pointers to each object
			m.A = A;
			m.B = B;

			// Vector from A to B
			Microsoft.Xna.Framework.Vector2 n = B.Position - A.Position;

			float r = A.Radius + B.Radius;
			r *= r;

			if (n.LengthSquared() > r)
				return false;

			// Circles have collided, now compute manifold
			float d = n.Length(); // perform actual sqrt

  			// If distance between circles is not zero
			if (d != 0)
			{
				// Distance is difference between radius and distance
				m.PenetrationDepth = r - d;

				// Utilize our d since we performed sqrt on it already within Length( )
				// Points from A to B, and is a unit vector
				m.Normal = n / d;

				return true;
  			}

			// Circles are on same position
			else
			{
				// Choose random (but consistent) values
				m.PenetrationDepth = A.Radius;

				m.Normal = new Microsoft.Xna.Framework.Vector2(1, 0);

				return true;
		    }
		}

		//bool AABBvsAABB(Manifold* m)
		public static bool GenerateManifold_AABBxAABB(AABB A, AABB B, ref Manifold m)
		{
			// Setup a couple pointers to each object
			//Object* A = m->A
			//Object* B = m->B
			//PhysObj A = m.A;
			//PhysObj B = m.B;

			m.A = A;
			m.B = B;

			// Vector from A to B
			//Vec2 n = B->pos - A->pos
			Microsoft.Xna.Framework.Vector2 n = B.Position - A.Position;

			//test
			Microsoft.Xna.Framework.Vector2 n2 = new Microsoft.Xna.Framework.Vector2(n.X, n.Y);
			n2.Normalize();

			//AABB abox = A->aabb
			//AABB bbox = B->aabb
			AABB abox = (AABB)A;
			AABB bbox = (AABB)B;

			// Calculate half extents along x axis for each object
			//float a_extent = (abox.max.x - abox.min.x) / 2
			//float b_extent = (bbox.max.x - bbox.min.x) / 2
			float a_extent = (abox.Right - abox.Left) / 2;
			float b_extent = (bbox.Right - bbox.Left) / 2;

			// Calculate overlap on x axis
			//float x_overlap = a_extent + b_extent - abs(n.x)
			float x_overlap = a_extent + b_extent - ((n.X >= 0) ? n.X : n.X*-1);

			// SAT test on x axis
			if (x_overlap > 0)
			{
				// Calculate half extents along x axis for each object
				//float a_extent = (abox.max.y - abox.min.y) / 2
				//float b_extent = (bbox.max.y - bbox.min.y) / 2
				a_extent = (abox.Bottom - abox.Top) / 2;
				b_extent = (bbox.Bottom - bbox.Top) / 2;

				// Calculate overlap on y axis
				//float y_overlap = a_extent + b_extent - abs(n.y)
				float y_overlap = a_extent + b_extent - ((n.Y >= 0) ? n.Y : n.Y * -1);

				// SAT test on y axis
				if (y_overlap > 0)
				{
					// Find out which axis is axis of least penetration
					//if (x_overlap > y_overlap)
					if(x_overlap < y_overlap)
					{
						// Point towards B knowing that n points from A to B
						//if (n.x < 0)
						if (n.X < 0)
							//m->normal = Vec2(-1, 0)
							m.Normal = new Microsoft.Xna.Framework.Vector2(-1, 0);

						else
							//m->normal = Vec2(0, 0)
							m.Normal = new Microsoft.Xna.Framework.Vector2(1, 0);

						//m->penetration = x_overlap
						m.PenetrationDepth = x_overlap;

						//test
						//m.Normal = n2;

						return true;
				    }
					else
					{
						// Point toward B knowing that n points from A to B
						//if (n.y < 0)
						if (n.Y < 0)
							//m->normal = Vec2(0, -1)
							m.Normal = new Microsoft.Xna.Framework.Vector2(0, -1);

						else
							//m->normal = Vec2(0, 1)
							m.Normal = new Microsoft.Xna.Framework.Vector2(0, 1);

						//m->penetration = y_overlap
						m.PenetrationDepth = y_overlap;

						//test
						//m.Normal = n2;

						return true;
				    }
				}
			}

			return false;
		}

		public static bool GenerateManifold_AABBxCircle(AABB A, Circle B, ref Manifold m)
		{
			// Setup a couple pointers to each object
			m.A = A;
			m.B = B;

			// Vector from A to B
			Microsoft.Xna.Framework.Vector2 n = B.Position - A.Position;

			// Closest point on A to center of B
			Microsoft.Xna.Framework.Vector2 closest = new Microsoft.Xna.Framework.Vector2(n.X, n.Y);

			// Calculate half extents along each axis
			float x_extent = (A.Right - A.Left) / 2;
			float y_extent = (A.Bottom - A.Top) / 2;

			// Clamp point to edges of the AABB
			closest.X = Mathf.Clamp(closest.X, -x_extent, x_extent);
			closest.Y = Mathf.Clamp(closest.Y, -y_extent, y_extent);

			bool inside = false;

  			// Circle is inside the AABB, so we need to clamp the circle's center
  			// to the closest edge
			if (n.X == closest.X && n.Y == closest.Y)
			{
				inside = true;

				// Find closest axis
				if (Mathf.Abs(n.X) > Mathf.Abs(n.Y))
				{
					// Clamp to closest extent
					if (closest.X > 0)
						closest.X = x_extent;
					else
						closest.X = -x_extent;
				}

				// y axis is shorter
				else
				{
					// Clamp to closest extent
					if (closest.Y > 0)
						closest.Y = y_extent;
					else
						closest.Y = -y_extent;
				}
			}

			Microsoft.Xna.Framework.Vector2 normal = n - closest;
			float d = normal.LengthSquared();
			float r = B.Radius;

			// Early out of the radius is shorter than distance to closest point and
			// Circle not inside the AABB
			if (d > r * r && !inside)
				return false;

			// Avoided sqrt until we needed
			d = Mathf.Sqrt(d);

  			// Collision normal needs to be flipped to point outside if circle was
  			// inside the AABB
			if (inside)
			{
				m.Normal = -normal;

				m.PenetrationDepth = r - d;
  			}
			else
			{
				m.Normal = normal;

				m.PenetrationDepth = r - d;
		    }

			m.Normal.Normalize();
			return true;
		}

		public static void ResolveCollision(Manifold m)
		{
			//Calculate relative velocity
			Microsoft.Xna.Framework.Vector2 relVelocity = m.B.Velocity - m.A.Velocity;

			//Calculate relative velocity in terms of the normal direction
			float velAlongNormal = PolyPhysUtil.DotProduct(relVelocity, m.Normal);

			//Do not resolve if velocities are separating
			if (velAlongNormal > 0)
				return;

			//Calculate Restitution
			float e = Math.Min(m.A.Restitution, m.B.Restitution);

			//Calculate impulse scalar
			float j = -(1 + e) * velAlongNormal;
			j /= m.A.InvertedMass + m.B.InvertedMass;

			//Apply Impulse
			Microsoft.Xna.Framework.Vector2 impulse = j * m.Normal;
			m.A.Velocity -= m.A.InvertedMass * impulse;
			m.B.Velocity += m.B.InvertedMass * impulse;
		}



	} //End Class
} // End Namespace

