using System;
using System.Collections.Generic;

namespace PolyEngine.InternalPhysics
{
	internal class InternalPhysicsDriver
	{

		static int iterations = 10;
		public static List<Manifold> contacts = new List<Manifold>();

		public static void step(List<Collider2D> bodies)
		{
			// Generate new collision info
			contacts.Clear();
			for (int i = 0; i < bodies.Count; ++i)
			{
				Collider2D A = bodies[i];

				for (int j = i + 1; j < bodies.Count; ++j)
				{
					Collider2D B = bodies[j];

					if (A.InvertedMass == 0 && B.InvertedMass == 0)
					{
						continue;
					}

					if (A.gameObject == B.gameObject) continue;

					Manifold m = new Manifold(A, B);
					m.solve();

					if (m.contactCount > 0)
					{
						contacts.Add(m);
					}
				}
			}

			// Integrate forces
			/*
			for (int i = 0; i < bodies.size(); ++i)
			{
				integrateForces(bodies.get(i), dt);
			}*/

			// Initialize collision
			for (int i = 0; i < contacts.Count; ++i)
			{
				contacts[i].initialize();
			}

			// Solve collisions
			for (int j = 0; j < iterations; ++j)
			{
				for (int i = 0; i < contacts.Count; ++i)
				{
					contacts[i].applyImpulse();
				}
			}

			// Integrate velocities
			/*
			for (int i = 0; i < bodies.size(); ++i)
			{
				integrateVelocity(bodies.get(i), dt);
			}*/

			// Correct positions
			for (int i = 0; i < contacts.Count; ++i)
			{
				contacts[i].positionalCorrection();
			}

			// Clear all forces
			/*
			for (int i = 0; i < bodies.size(); ++i)
			{
				Body b = bodies.get(i);
				b.force.set(0, 0);
				b.torque = 0;
			}*/
		}
	}
}

