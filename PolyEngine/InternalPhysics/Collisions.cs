using System;
namespace PolyEngine.InternalPhysics
{
	internal class Collisions
	{
		public static void Dispatch(Collider2D a, Collider2D b, Manifold manifold)
		{
			if (a.GetType() == typeof(CircleCollider2D) && b.GetType() == typeof(CircleCollider2D))
			{
				CircleCircle((CircleCollider2D)a, (CircleCollider2D)b, manifold);
			}
			else if (a.GetType() == typeof(CircleCollider2D) && b.GetType() == typeof(EdgeCollider2D))
			{
				CircleEdge((CircleCollider2D)a, (EdgeCollider2D)b, manifold);
			}
			else if (a.GetType() == typeof(EdgeCollider2D) && b.GetType() == typeof(CircleCollider2D))
			{
				EdgeCircle((EdgeCollider2D)a, (CircleCollider2D)b, manifold);
			}
			else if (a.GetType() == typeof(EdgeCollider2D) && b.GetType() == typeof(EdgeCollider2D))
			{
				EdgeEdge((EdgeCollider2D)a, (EdgeCollider2D)b, manifold);
			}
		}

		public static void CircleCircle(CircleCollider2D a, CircleCollider2D b, Manifold m)
		{
			// Calculate translational vector, which is normal
			// Vec2 normal = b->position - a->position;
			Vector2 normal = b.position - a.position;

			// real dist_sqr = normal.LenSqr( );
			// real radius = A->radius + B->radius;
			float dist_sqr = Vector2.SqrMagnitude(normal);
			float radius = a.radius + b.radius;

			// Not in contact
			if (dist_sqr >= radius * radius)
			{
				m.contactCount = 0;
				return;
			}

			float distance = (float)Mathf.Sqrt(dist_sqr);

			m.contactCount = 1;

			if (distance == 0.0f)
			{
				// m->penetration = A->radius;
				// m->normal = Vec2( 1, 0 );
				// m->contacts [0] = a->position;
				m.penetration = a.radius;
				m.normal = Vector2.right;
				m.contacts[0] = a.position;
			}
			else
			{
				// m->penetration = radius - distance;
				// m->normal = normal / distance; // Faster than using Normalized since
				// we already performed sqrt
				// m->contacts[0] = m->normal * A->radius + a->position;
				m.penetration = radius - distance;
				m.normal = normal / distance;
				m.contacts[0] = m.normal * a.radius + a.position;
			}
		}

		public static void CircleEdge(CircleCollider2D a, EdgeCollider2D b, Manifold m)
		{
			m.contactCount = 0;

			// Transform circle center to Polygon model space
			// Vec2 center = a->position;
			// center = B->u.Transpose( ) * (center - b->position);
			Vector2 center = b.u.transpose() * (a.position - b.position);

			// Find edge with minimum penetration
			// Exact concept as using support points in Polygon vs Polygon
			float separation = -Single.MaxValue;
			int faceNormal = 0;
			for (int i = 0; i < b.pointCount; ++i)
			{
				// real s = Dot( B->m_normals[i], center - B->m_vertices[i] );
				float s = Vector2.Dot(b.normals[i], center - b.points[i]);

				if (s > a.radius)
				{
					return;
				}

				if (s > separation)
				{
					separation = s;
					faceNormal = i;
				}
			}

			// Grab face's vertices
			Vector2 v1 = b.points[faceNormal];
			int i2 = faceNormal + 1 < b.pointCount ? faceNormal + 1 : 0;
			Vector2 v2 = b.points[i2];

			// Check to see if center is within polygon
			if (separation < VectorMath.EPSILON)
			{
				// m->contact_count = 1;
				// m->normal = -(B->u * B->m_normals[faceNormal]);
				// m->contacts[0] = m->normal * A->radius + a->position;
				// m->penetration = A->radius;

				m.contactCount = 1;
				m.normal = -(b.u * b.normals[faceNormal]);
				//b.u.mul(b.normals[faceNormal], m.normal).negi();
				m.contacts[0] = m.normal * a.radius + a.position;
				m.penetration = a.radius;
				return;
			}

			// Determine which voronoi region of the edge center of circle lies within
			// real dot1 = Dot( center - v1, v2 - v1 );
			// real dot2 = Dot( center - v2, v1 - v2 );
			// m->penetration = A->radius - separation;
			float dot1 = Vector2.Dot(center - v1, v2 - v1);
			float dot2 = Vector2.Dot(center - v2, v1 - v2);
			m.penetration = a.radius - separation;

			// Closest to v1
			if (dot1 <= 0.0f)
			{
				//dx * dx + dy * dy
				float dx = center.x - v1.x;
				float dy = center.y - v1.y;
				float dsq = dx * dx + dy * dy;
				//if (Vector2.distanceSq(center, v1) > a.radius * a.radius)
				if (dsq > a.radius * a.radius)
				{
					return;
				}

				// m->contact_count = 1;
				// Vec2 n = v1 - center;
				// n = B->u * n;
				// n.Normalize( );
				// m->normal = n;
				// v1 = B->u * v1 + b->position;
				// m->contacts[0] = v1;

				m.contactCount = 1;
				//b.u.muli(m.normal.set(v1).subi(center)).normalize();
				//b.u.mul(v1, m.contacts[0]).addi(b.position);
				Vector2 n = v1 - center;
				n = b.u * n;
				n.Normalize();
				m.normal = n;
				v1 = b.u * v1 + b.position;
				m.contacts[0] = v1;
			}

			// Closest to v2
			else if (dot2 <= 0.0f)
			{
				float dx = center.x - v2.x;
				float dy = center.y - v2.y;
				float dsq = dx * dx + dy * dy;
				if (dsq > a.radius * a.radius)
				{
					return;
				}

				// m->contact_count = 1;
				// Vec2 n = v2 - center;
				// v2 = B->u * v2 + b->position;
				// m->contacts[0] = v2;
				// n = B->u * n;
				// n.Normalize( );
				// m->normal = n;

				m.contactCount = 1;
				//b.u.muli(m.normal.set(v2).subi(center)).normalize();
				//b.u.mul(v2, m.contacts[0]).addi(b.position);
				Vector2 n = v2 - center;
				v2 = b.u * v2 + b.position;
				m.contacts[0] = v2;
				n = b.u * n;
				n.Normalize();
				m.normal = n;
			}

			// Closest to face
			else
			{
				Vector2 n = b.normals[faceNormal];

				if (Vector2.Dot(center-v1, n) > a.radius)
				{
					return;
				}

				// n = B->u * n;
				// m->normal = -n;
				// m->contacts[0] = m->normal * A->radius + a->position;
				// m->contact_count = 1;

				n = b.u * n;
				m.normal = -n;
				m.contacts[0] = m.normal * a.radius + a.position;
				m.contactCount = 1;
				//b.u.mul(n, m.normal).negi();
				//m.contacts[0].set(a.position).addsi(m.normal, a.radius);

			}
		}

		public static void EdgeCircle(EdgeCollider2D a, CircleCollider2D b, Manifold m)
		{
			CircleEdge(b, a, m);
			m.normal = -m.normal;
		}

		public static void EdgeEdge(EdgeCollider2D a, EdgeCollider2D b, Manifold m)
		{
			m.contactCount = 0;

			// Check for a separating axis with A's face planes
			int[] faceA = { 0 };
			float penetrationA = findAxisLeastPenetration(faceA, a, b);
			if (penetrationA >= 0.0f)
			{
				return;
			}

			// Check for a separating axis with B's face planes
			int[] faceB = { 0 };
			float penetrationB = findAxisLeastPenetration(faceB, b, a);
			if (penetrationB >= 0.0f)
			{
				return;
			}

			int referenceIndex;
			bool flip; // Always point from a to b

			EdgeCollider2D RefPoly; // Reference
			EdgeCollider2D IncPoly; // Incident

			// Determine which shape contains reference face
			if (VectorMath.gt(penetrationA, penetrationB))
			{
				RefPoly = a;
				IncPoly = b;
				referenceIndex = faceA[0];
				flip = false;
			}
			else
			{
				RefPoly = b;
				IncPoly = a;
				referenceIndex = faceB[0];
				flip = true;
			}

			// World space incident face
			Vector2[] incidentFace = new Vector2[2];
			incidentFace[0] = new Vector2(0, 0);
			incidentFace[1] = new Vector2(0, 0);

			findIncidentFace(incidentFace, RefPoly, IncPoly, referenceIndex);

			// y
			// ^ .n ^
			// +---c ------posPlane--
			// x < | i |\
			// +---+ c-----negPlane--
			// \ v
			// r
			//
			// r : reference face
			// i : incident poly
			// c : clipped point
			// n : incident normal

			// Setup reference face vertices
			Vector2 v1 = RefPoly.points[referenceIndex];
			referenceIndex = referenceIndex + 1 == RefPoly.pointCount ? 0 : referenceIndex + 1;
			Vector2 v2 = RefPoly.points[referenceIndex];

			// Transform vertices to world space
			// v1 = RefPoly->u * v1 + RefPoly->body->position;
			// v2 = RefPoly->u * v2 + RefPoly->body->position;
			v1 = RefPoly.u * v1 + (Vector2)RefPoly.transform.position;
			v2 = RefPoly.u * v2 + (Vector2)RefPoly.transform.position;

			// Calculate reference face side normal in world space
			// Vec2 sidePlaneNormal = (v2 - v1);
			// sidePlaneNormal.Normalize( );
			Vector2 sidePlaneNormal = v2 - v1;
			sidePlaneNormal.Normalize();

			// Orthogonalize
			// Vec2 refFaceNormal( sidePlaneNormal.y, -sidePlaneNormal.x );
			Vector2 refFaceNormal = new Vector2(sidePlaneNormal.y, -sidePlaneNormal.x);

			// ax + by = c
			// c is distance from origin
			// real refC = Dot( refFaceNormal, v1 );
			// real negSide = -Dot( sidePlaneNormal, v1 );
			// real posSide = Dot( sidePlaneNormal, v2 );
			float refC = Vector2.Dot(refFaceNormal, v1);
			float negSide = -Vector2.Dot(sidePlaneNormal, v1);
			float posSide = Vector2.Dot(sidePlaneNormal, v2);

			// Clip incident face to reference face side planes
			// if(Clip( -sidePlaneNormal, negSide, incidentFace ) < 2)
			if (clip(-sidePlaneNormal, negSide, incidentFace) < 2)
			{
				return; // Due to floating point error, possible to not have required
						// points
			}

			// if(Clip( sidePlaneNormal, posSide, incidentFace ) < 2)
			if (clip(sidePlaneNormal, posSide, incidentFace) < 2)
			{
				return; // Due to floating point error, possible to not have required
						// points
			}

			// Flip
			m.normal.Set(refFaceNormal.x, refFaceNormal.y);
			if (flip)
			{
				m.normal = -m.normal;
			}

			// Keep points behind reference face
			int cp = 0; // clipped points behind reference face
			float separation = Vector2.Dot(refFaceNormal, incidentFace[0]) - refC;
			if (separation <= 0.0f)
			{
				m.contacts[cp].Set(incidentFace[0].x, incidentFace[0].y);
				m.penetration = -separation;
				++cp;
			}
			else
			{
				m.penetration = 0;
			}

			separation = Vector2.Dot(refFaceNormal, incidentFace[1]) - refC;

			if (separation <= 0.0f)
			{
				m.contacts[cp].Set(incidentFace[1].x, incidentFace[1].y);

				m.penetration += -separation;
				++cp;

				// Average penetration
				m.penetration /= cp;
			}

			m.contactCount = cp;
		}

		public static int clip(Vector2 n, float c, Vector2[] face)
		{
			int sp = 0;
			Vector2[] outa = {
					new Vector2(face[0].x, face[0].y),
					new Vector2(face[1].x, face[1].y)
				};

			// Retrieve distances from each endpoint to the line
			// d = ax + by - c
			// real d1 = Dot( n, face[0] ) - c;
			// real d2 = Dot( n, face[1] ) - c;
			float d1 = Vector2.Dot(n, face[0]) - c;
			float d2 = Vector2.Dot(n, face[1]) - c;

			// If negative (behind plane) clip
			// if(d1 <= 0.0f) out[sp++] = face[0];
			// if(d2 <= 0.0f) out[sp++] = face[1];
			if (d1 <= 0.0f) outa[sp++].Set(face[0].x, face[0].y );
			if (d2 <= 0.0f) outa[sp++].Set(face[1].x, face[1].y );

			// If the points are on different sides of the plane
			if (d1* d2 < 0.0f) // less than to ignore -0.0f
			{
				// Push intersection point
				// real alpha = d1 / (d1 - d2);
				// out[sp] = face[0] + alpha * (face[1] - face[0]);
				// ++sp;

				float alpha = d1 / (d1 - d2);
				outa[sp++] = face[0] + alpha * (face[1] - face[0]);
			}

			// Assign our new converted values
			face[0] = outa[0];
			face[1] = outa[1];

			// assert( sp != 3 );

			return sp;
		}

		public static void findIncidentFace(Vector2[] v, EdgeCollider2D RefPoly, EdgeCollider2D IncPoly, int referenceIndex)
		{
			Vector2 referenceNormal = RefPoly.normals[referenceIndex];

			// Calculate normal in incident's frame of reference
			// referenceNormal = RefPoly->u * referenceNormal; // To world space
			// referenceNormal = IncPoly->u.Transpose( ) * referenceNormal; // To
			// incident's model space
			referenceNormal = RefPoly.u.mul(referenceNormal); // To world space
			referenceNormal = IncPoly.u.transpose().mul(referenceNormal); // To
																		  // incident's
																		  // model
																		  // space

			// Find most anti-normal face on incident polygon
			int incidentFace = 0;
			float minDot = Single.MaxValue;
			for (int i = 0; i < IncPoly.pointCount; ++i)
			{
				// real dot = Dot( referenceNormal, IncPoly->m_normals[i] );
				float dot = Vector2.Dot(referenceNormal, IncPoly.normals[i]);

				if (dot < minDot)
				{
					minDot = dot;
					incidentFace = i;
				}
			}

			// Assign face vertices for incidentFace
			// v[0] = IncPoly->u * IncPoly->m_vertices[incidentFace] +
			// IncPoly->body->position;
			// incidentFace = incidentFace + 1 >= (int32)IncPoly->m_vertexCount ? 0 :
			// incidentFace + 1;
			// v[1] = IncPoly->u * IncPoly->m_vertices[incidentFace] +
			// IncPoly->body->position;

			v[0] = IncPoly.u * IncPoly.points[incidentFace]+ (Vector2)IncPoly.transform.position;
			incidentFace = incidentFace + 1 >= (int)IncPoly.pointCount ? 0 : incidentFace + 1;
			v[1] = IncPoly.u * IncPoly.points[incidentFace] + (Vector2)IncPoly.transform.position;
		}

		public static float findAxisLeastPenetration(int[] faceIndex, EdgeCollider2D A, EdgeCollider2D B)
		{
			float bestDistance = -Single.MaxValue;
			int bestIndex = 0;

			for (int i = 0; i < A.pointCount; ++i)
			{
				// Retrieve a face normal from A
				// Vec2 n = A->m_normals[i];
				// Vec2 nw = A->u * n;
				Vector2 nw = A.u.mul(A.normals[i]);

				// Transform face normal into B's model space
				// Mat2 buT = B->u.Transpose( );
				// n = buT * nw;
				Mat2 buT = B.u.transpose();
				Vector2 n = buT.mul(nw);

				// Retrieve support point from B along -n
				// Vec2 s = B->GetSupport( -n );
				Vector2 s = B.getSupport(-n);

				// Retrieve vertex on face from A, transform into
				// B's model space
				// Vec2 v = A->m_vertices[i];
				// v = A->u * v + A->body->position;
				// v -= B->body->position;
				// v = buT * v;
				Vector2 v = A.points[i];
				v = A.u * v + (Vector2)A.transform.position;
				v -= (Vector2)B.transform.position;
				v = buT * v;

				// Compute penetration distance (in B's model space)
				// real d = Dot( n, s - v );
				float d = Vector2.Dot(n, s - v);

				// Store greatest distance
				if (d > bestDistance)
				{
					bestDistance = d;
					bestIndex = i;
				}
			}

			faceIndex[0] = bestIndex;
			return bestDistance;
		}

	}
}

