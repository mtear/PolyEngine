using System;
namespace PolyEngine.InternalPhysics
{
	internal class Manifold
	{
		public Collider2D A, B;
		public Vector2 normal = Vector2.zero;
		public Vector2[] contacts = { new Vector2(0,0), new Vector2(0,0) };
		public int contactCount;
		public float e, rf, penetration;

		public Manifold(Collider2D a, Collider2D b)
		{
			A = a;
			B = b;
		}

		public void solve()
		{
			Collisions.Dispatch(A, B, this);
		}

		public void initialize()
		{
			// Calculate average restitution
			// e = std::min( A->restitution, B->restitution );
			e = Mathf.Min(A.sharedMaterial.bounciness, B.sharedMaterial.bounciness);

			// Calculate static and dynamic friction
			// sf = std::sqrt( A->staticFriction * A->staticFriction );
			// df = std::sqrt( A->dynamicFriction * A->dynamicFriction );
			rf = (float)Mathf.Sqrt(A.sharedMaterial.friction * A.sharedMaterial.friction
								   + B.sharedMaterial.friction * B.sharedMaterial.friction);

			for (int i = 0; i < contactCount; ++i)
			{
				// Calculate radii from COM to contact
				// Vec2 ra = contacts[i] - A->position;
				// Vec2 rb = contacts[i] - B->position;
				Vector2 ra = contacts[i] - (Vector2)A.position;
				Vector2 rb = contacts[i] - (Vector2)B.position;

				//TODO no rigidbody
				// Vec2 rv = B->velocity + Cross( B->angularVelocity, rb ) -
				// A->velocity - Cross( A->angularVelocity, ra );
				//Vector2 rv = B.velocity.add(Vector2.cross(B.angularVelocity, rb, new Vec2())).subi(A.velocity).subi(Vec2.cross(A.angularVelocity, ra, new Vec2()));
				Vector2 rv = B.attachedRigidBody.velocity + VectorMath.cross(B.attachedRigidBody.angularVelocity, rb) -
							  A.attachedRigidBody.velocity - VectorMath.cross(A.attachedRigidBody.angularVelocity, ra);

				// Determine if we should perform a resting collision or not
				// The idea is if the only thing moving this object is gravity,
				// then the collision should be performed without any restitution
				// if(rv.LenSqr( ) < (dt * gravity).LenSqr( ) + EPSILON)
				if (Vector2.SqrMagnitude(rv) < Vector2.SqrMagnitude(Time.fixedDeltaTime * Physics2D.gravity) + VectorMath.EPSILON)
				{
					e = 0.0f;
				}
			}
		}

		public void positionalCorrection()
		{
			// const real k_slop = 0.05f; // Penetration allowance
			// const real percent = 0.4f; // Penetration percentage to correct
			// Vec2 correction = (std::max( penetration - k_slop, 0.0f ) / (A->im +
			// B->im)) * normal * percent;
			// A->position -= correction * A->im;
			// B->position += correction * B->im;

			float k_slop = 0.05f;
			float percent = 0.4f;
			Vector2 correction = (Mathf.Max(penetration - k_slop, 0f) / (A.InvertedMass +
												   		B.InvertedMass)) * normal * percent;
			A.attachedRigidBody.position -= correction * A.InvertedMass;
			B.attachedRigidBody.position -= correction * B.InvertedMass;
		}

		public void infiniteMassCorrection()
		{
			A.attachedRigidBody.velocity = Vector2.zero;
			B.attachedRigidBody.velocity = Vector2.zero;
		}

		public void applyImpulse()
		{
			// Early out and positional correct if both objects have infinite mass
			// if(Equal( A->im + B->im, 0 ))
			if((A.InvertedMass + B.InvertedMass) < VectorMath.EPSILON) //TODO cull for this?
			//if (ImpulseMath.equal(A.invMass + B.invMass, 0))
			{
				infiniteMassCorrection();
				return;
			}

			//TODO check why this is getting here when it shouldn't be colliding

			Vector2 Av = A.attachedRigidBody.velocity;
			float Aw = A.attachedRigidBody.angularVelocity;
			Vector2 Bv = B.attachedRigidBody.velocity;
			float Bw = B.attachedRigidBody.angularVelocity;

			for (int i = 0; i < contactCount; ++i)
			{
			// Calculate radii from COM to contact
			// Vec2 ra = contacts[i] - A->position;
			// Vec2 rb = contacts[i] - B->position;
			//Experimental!
			Vector2 ra = new Vector2(0, 0);
			Vector2 rb = new Vector2(0, 0);
			//for (int i = 0; i < contactCount; i++)
			//{
				ra += (contacts[i] - (Vector2)A.position);
				rb += (contacts[i] - (Vector2)B.position);
			//}

				// Relative velocity
				// Vec2 rv = B->velocity + Cross( B->angularVelocity, rb ) -
				// A->velocity - Cross( A->angularVelocity, ra );
				//Vec2 rv = B.velocity.add(Vec2.cross(B.angularVelocity, rb, new Vec2())).subi(A.velocity).subi(Vec2.cross(A.angularVelocity, ra, new Vec2()));
				Vector2 rv = Bv + VectorMath.cross(Bw*Mathf.Deg2Rad, rb) -
				              Av - VectorMath.cross(Aw*Mathf.Deg2Rad, ra);
				
				// Relative velocity along the normal
				// real contactVel = Dot( rv, normal );
				float contactVel = Vector2.Dot(rv, normal);

				// Do not resolve if velocities are separating
				if (contactVel > 0)
				{
					return;
				}

				// real raCrossN = Cross( ra, normal );
				// real rbCrossN = Cross( rb, normal );
				// real invMassSum = A->im + B->im + Sqr( raCrossN ) * A->iI + Sqr(
				// rbCrossN ) * B->iI;
				float raCrossN = VectorMath.cross(ra, normal);
				float rbCrossN = VectorMath.cross(rb, normal);
				float invMassSum = A.InvertedMass + B.InvertedMass + (raCrossN * raCrossN)
				                    * A.InvertedInertia + (rbCrossN * rbCrossN) * B.InvertedInertia;

				//EXP
				float normalMass = invMassSum > 0.0f ? 1.0f / invMassSum : 0.0f;

				// Calculate impulse scalar
				float j = -(1.0f + e) * contactVel;
				j /= invMassSum;
				//j *= normalMass;
				j /= contactCount;

				// Apply impulse
				Vector2 impulse = normal * j;
				A.attachedRigidBody.AddImpulse(-impulse, ra);
				B.attachedRigidBody.AddImpulse(impulse, rb);

				// Friction impulse
				// rv = B->velocity + Cross( B->angularVelocity, rb ) -
				// A->velocity - Cross( A->angularVelocity, ra );
				//rv = B.velocity.add(Vec2.cross(B.angularVelocity, rb, new Vec2())).subi(A.velocity).subi(Vec2.cross(A.angularVelocity, ra, new Vec2()));
				rv = Bv + VectorMath.cross(Bw*Mathf.Deg2Rad, rb) -
				      Av - VectorMath.cross(Aw*Mathf.Deg2Rad, ra); //THIS IS DV

				// Vec2 t = rv - (normal * Dot( rv, normal ));
				// t.Normalize( );
				Vector2 t = rv - (normal * Vector2.Dot(rv, normal));
				t.Normalize();

				// j tangent magnitude
				float jt = -Vector2.Dot(rv, t);
				jt /= invMassSum;
				//jt *= normalMass;
				jt /= contactCount;

				// Don't apply tiny friction impulses
				if(Mathf.Abs(jt) < VectorMath.EPSILON)
				//if (ImpulseMath.equal(jt, 0.0f))
				{
					return;
				}

				// Coulumb's law
				Vector2 tangentImpulse;
				// if(std::abs( jt ) < j * sf)
				//if (Mathf.Abs(jt) < j * rf)
				//{
					// tangentImpulse = t * jt;
					//tangentImpulse = t * jt;
				//}
				//else
				//{
					// tangentImpulse = t * -j * df;
				//	tangentImpulse = t * -j * rf;
				//}

				// Apply friction impulse
				// A->ApplyImpulse( -tangentImpulse, ra );
				// B->ApplyImpulse( tangentImpulse, rb );
				//A.attachedRigidBody.AddImpulse(-tangentImpulse, ra);
				//B.attachedRigidBody.AddImpulse(tangentImpulse, rb);
			}
		}


	}
}

