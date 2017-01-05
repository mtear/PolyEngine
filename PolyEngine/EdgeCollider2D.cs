using System;
using PolyEngine.InternalPhysics;

namespace PolyEngine
{
	public class EdgeCollider2D : Collider2D
	{


		#region Public Variables
		//edgeCount Gets the number of edges.
		public int edgeCount
		{
			get
			{
				return (mPoints == 0) ? 0 : mPoints - 1;
			}
		}

		//pointCount  Gets the number of points.
		public int pointCount
		{
			get
			{
				return mPoints;
			}
		}

		//points Get or set the points defining multiple continuous edges.
		public Vector2[] points = new Vector2[MAX_POLY_VERTEX_COUNT];

		#endregion Public Variables


		#region Private Variables

		private int mPoints = 0;

		#endregion Private Variables


		#region Internal Variables

		internal static int MAX_POLY_VERTEX_COUNT = 64;

		internal Vector2[] normals = new Vector2[MAX_POLY_VERTEX_COUNT];

		internal Mat2 u = new Mat2();

		#endregion Internal Variables


		#region Constructors

		public EdgeCollider2D()
		{
			for (int i = 0; i < MAX_POLY_VERTEX_COUNT; i++)
			{
				points[i] = new Vector2(0, 0);
				normals[i] = new Vector2(0, 0);
			}
		}

		#endregion Constructors


		#region Public Methods

		//Reset Reset to a single edge consisting of two points. TODO

		#endregion Public Methods


		#region Private Methods

		private void RecalculateMass()
		{
			if (attachedRigidBody == null) return;
			if (attachedRigidBody.useAutoMass) { }
			else
			{
				computeInertia();
			}
		}

		#endregion Private Methods


		#region Internal Methods

		internal override void computeAutoMass(float density)
		{
			if (attachedRigidBody == null) return;

			// Calculate centroid and moment of inertia
			Vector2 c = new Vector2(0.0f, 0.0f); // centroid
			float area = 0.0f;
			float I = 0.0f;
			const float k_inv3 = 1.0f / 3.0f;

			for (int i = 0; i < mPoints; ++i)
			{
				// Triangle vertices, third vertex implied as (0, 0)
				Vector2 p1 = points[i];
				Vector2 p2 = points[(i + 1) % mPoints];

				float D = VectorMath.cross(p1, p2);
				float triangleArea = 0.5f * D;

				area += triangleArea;

				// Use area to weight the centroid average, not just vertex position
				c += triangleArea * k_inv3 * (p1 + p2);

				float intx2 = p1.x * p1.x + p2.x * p1.x + p2.x * p2.x;
				float inty2 = p1.y * p1.y + p2.y * p1.y + p2.y * p2.y;
				I += (0.25f * k_inv3 * D) * (intx2 + inty2);
			}

			c *= (1.0f / area);

			// Translate vertices to centroid (make the centroid (0, 0)
			// for the polygon in model space)
			// Not really necessary, but I like doing this anyway
			for (int i = 0; i < mPoints; ++i)
			{
				points[i] -= c;
			}

			attachedRigidBody.mass = density * area;
			attachedRigidBody.inertia = I * density;
		}

		internal override void computeInertia()
		{
			if (attachedRigidBody == null) return;

			// Calculate centroid and moment of inertia
			Vector2 c = new Vector2(0.0f, 0.0f); // centroid
			float area = 0.0f;
			float I = 0.0f;
			const float k_inv3 = 1.0f / 3.0f;

			for (int i = 0; i < mPoints; ++i)
			{
				// Triangle vertices, third vertex implied as (0, 0)
				Vector2 p1 = points[i];
				Vector2 p2 = points[(i + 1) % mPoints];

				float D = VectorMath.cross(p1, p2);
				float triangleArea = 0.5f * D;

				area += triangleArea;

				// Use area to weight the centroid average, not just vertex position
				c += triangleArea * k_inv3 * (p1 + p2);

				float intx2 = p1.x * p1.x + p2.x * p1.x + p2.x * p2.x;
				float inty2 = p1.y * p1.y + p2.y * p1.y + p2.y * p2.y;
				I += (0.25f * k_inv3 * D) * (intx2 + inty2);
			}

			c *= (1.0f / area);

			// Translate vertices to centroid (make the centroid (0, 0)
			// for the polygon in model space)
			// Not really necessary, but I like doing this anyway
			for (int i = 0; i < mPoints; ++i)
			{
				points[i] -= c;
			}

			attachedRigidBody.inertia = I * (attachedRigidBody.mass / area);
		}

		internal Vector2 getSupport(Vector2 dir)
		{
			float bestProjection = -Single.MaxValue;
			Vector2 bestVertex = points[0];

			for (int i = 0; i < mPoints; ++i)
			{
				Vector2 v = points[i];
				float projection = Vector2.Dot(v, dir);

				if (projection > bestProjection)
				{
					bestVertex = v;
					bestProjection = projection;
				}
			}

			return bestVertex;
		}

		internal void set(params Vector2[] verts)
		{
			// Find the right most point on the hull
			int rightMost = 0;
			float highestXCoord = verts[0].x;
			for (int i = 1; i < verts.Length; ++i)
			{
				float x = verts[i].x;

				if (x > highestXCoord)
				{
					highestXCoord = x;
					rightMost = i;
				}
				// If matching x then take farthest negative y
				else if (x == highestXCoord)
				{
					if (verts[i].y < verts[rightMost].y)
					{
						rightMost = i;
					}
				}
			}

			int[] hull = new int[MAX_POLY_VERTEX_COUNT];
			int outCount = 0;
			int indexHull = rightMost;

			for (;;)
			{
				hull[outCount] = indexHull;

				// Search for next index that wraps around the hull
				// by computing cross products to find the most counter-clockwise
				// vertex in the set, given the previos hull index
				int nextHullIndex = 0;
				for (int i = 1; i < verts.Length; ++i)
				{
					// Skip if same coordinate as we need three unique
					// points in the set to perform a cross product
					if (nextHullIndex == indexHull)
					{
						nextHullIndex = i;
						continue;
					}

					// Cross every set of three unique vertices
					// Record each counter clockwise third vertex and add
					// to the output hull
					// See : http://www.oocities.org/pcgpe/math2d.html
					Vector2 e1 = verts[nextHullIndex] -= (verts[hull[outCount]]);
					Vector2 e2 = verts[i] -= (verts[hull[outCount]]);
					float c = VectorMath.cross(e1, e2);
					if (c < 0.0f)
					{
						nextHullIndex = i;
					}

					// Cross product is zero then e vectors are on same line
					// therefore want to record vertex farthest along that line
					if (c == 0.0f && Vector2.SqrMagnitude(e2) > Vector2.SqrMagnitude(e1))
					{
						nextHullIndex = i;
					}
				}

				++outCount;
				indexHull = nextHullIndex;

				// Conclude algorithm upon wrap-around
				if (nextHullIndex == rightMost)
				{
					mPoints = outCount;
					break;
				}
			}

			// Copy vertices into shape's vertices
			for (int i = 0; i < mPoints; ++i)
			{
				points[i].Set(verts[hull[i]].x, verts[hull[i]].y);
			}

			// Compute face normals
			for (int i = 0; i < mPoints; ++i)
			{
				Vector2 face = points[(i + 1) % mPoints] - (points[i]);

				// Calculate normal with 2D cross product between vector and scalar
				normals[i].Set(face.y, -face.x);
				normals[i].Normalize();
			}

			RecalculateMass();
		}

		internal void setBox(float hw, float hh)
		{
			mPoints = 4;
			points[0].Set(-hw, -hh);
			points[1].Set(hw, -hh);
			points[2].Set(hw, hh);
			points[3].Set(-hw, hh);
			normals[0].Set(0.0f, -1.0f);
			normals[1].Set(1.0f, 0.0f);
			normals[2].Set(0.0f, 1.0f);
			normals[3].Set(-1.0f, 0.0f);

			RecalculateMass();
		}

		internal override void setOrient(float radians)
		{
			u.set(radians);
		}

		internal override void RenderGizmo()
		{
			for (int i = 0; i < mPoints-1; i++)
			{
				PolyEngine.Rendering.GraphicsEngine.DrawLine(u * points[i]+(Vector2)transform.position,
				                                             u * points[i+1]+ (Vector2)transform.position);
			}
			PolyEngine.Rendering.GraphicsEngine.DrawLine(u * points[0]+ (Vector2)transform.position,
			                                             u * points[mPoints-1]+ (Vector2)transform.position);
		}

		#endregion Internal Methods


	}
}

