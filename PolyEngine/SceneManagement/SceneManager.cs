using System.Collections.Generic;
using System.IO;
using PolyEngine.Rendering;

namespace PolyEngine.SceneManagement
{
	public class SceneManager
	{


		#region Public Variables

		//sceneCount The total number of scenes.
		//sceneCountInBuildSettings   Number of scenes in Build Settings.

		#endregion Public Variables


		#region Internal Variables

		internal static Scene currentScene;

		#endregion Internal Variables


		#region Public Static Methods

		//CreateScene Create an empty new scene with the given name additively.
		//GetActiveScene  Gets the currently active scene.
		//GetSceneAt Get the scene at index in the SceneManager's list of added scenes.
		//GetSceneByName Searches through the scenes added to the SceneManager for a scene with the given name.
		//GetSceneByPath Searches all scenes added to the SceneManager for a scene that has the given asset path.
		//LoadScene Loads the scene by its name or index in Build Settings.
		public static void LoadScene(string scenename)
		{//TODO this is temporary
			List<string> lines = new List<string>(File.ReadAllLines("Content/"+scenename+".unity"));

			currentScene = new Scene();
			currentScene.rootGameObjects = new System.Collections.Generic.List<GameObject>();
			currentScene.gameObjects = new System.Collections.Generic.List<GameObject>();
			GameObject go = new GameObject("MainCamera", typeof(Camera));

			GameObject go2 = new GameObject("player", typeof(TestXNA4.Test), typeof(Rigidbody2D));
			go2.transform.position = new Vector3(-2,7f,0);
			//go2.transform.localScale = new Vector3(5f, 5f, 0);
			Rigidbody2D rb = go2.GetComponent<Rigidbody2D>();
			rb.velocity = new Vector2(-1, -6f); //TODO TEST
			rb.mass = 1;
			//rb.velocity = new Vector2(8, -24f);
			//go2.GetComponent<Rigidbody2D>().angularVelocity = 90;
			//go2.transform.eulerAngles = new Vector3(0, 0, 20);
			go2.AddComponent<CircleCollider2D>();
			//EdgeCollider2D cc = go2.AddComponent<EdgeCollider2D>();
			//cc.setBox(2.5f, 2f);
			SpriteRenderer sr = (SpriteRenderer)go2.AddComponent("PolyEngine.SpriteRenderer");
			//todo resources load
			//sr.sprite = Sprite.Create(GraphicsEngine.GetSprite("doge"));

			/*GameObject go3 = new GameObject("player2", typeof(TestXNA4.Test), typeof(Rigidbody2D));
			//go3.transform.position = new Vector3(-5, 1, 0); //TODO TEST THIS
			go3.transform.position = new Vector3(4, 1, 0);
			go3.transform.localScale = new Vector3(6f, 6f, 0);
			CircleCollider2D cc2 = go3.AddComponent<CircleCollider2D>();*/

			GameObject go3b = new GameObject("player2", typeof(TestXNA4.Test), typeof(Rigidbody2D));
			go3b.transform.position = new Vector3(-4, 0, 0);
			CircleCollider2D cc3b = go3b.AddComponent<CircleCollider2D>();
			PhysicsMaterial2D pm = new PhysicsMaterial2D();
			pm.friction = 1f;
			cc3b.sharedMaterial = pm;
			go3b.GetComponent<Rigidbody2D>().mass = 1;

			/*GameObject go3c = new GameObject("player2", typeof(TestXNA4.Test), typeof(Rigidbody2D));
			go3c.transform.position = new Vector3(3, -4, 0);
			CircleCollider2D cc3c = go3c.AddComponent<CircleCollider2D>();
			cc3c.radius = 2;
			go3c.GetComponent<Rigidbody2D>().mass = 4;*/

			GameObject go4 = new GameObject("wall", typeof(Rigidbody2D));
			go4.rigidBody.mass = 0;
			go4.transform.position = new Vector3(0, -10, 0);
			EdgeCollider2D ec = go4.AddComponent<EdgeCollider2D>();
			ec.setBox(15f, 2f);

			GameObject go4a = new GameObject("wall", typeof(Rigidbody2D));
			go4a.rigidBody.mass = 0;
			go4a.transform.position = new Vector3(0, 10, 0);
			EdgeCollider2D eca = go4a.AddComponent<EdgeCollider2D>();
			eca.setBox(15f, 2f);

			GameObject go4b = new GameObject("wall", typeof(Rigidbody2D));
			go4b.rigidBody.mass = 0;
			go4b.transform.position = new Vector3(-15, 0, 0);
			EdgeCollider2D ecb = go4b.AddComponent<EdgeCollider2D>();
			ecb.setBox(2f, 15f);

			GameObject go4c = new GameObject("wall", typeof(Rigidbody2D));
			go4c.rigidBody.mass = 0;
			go4c.transform.position = new Vector3(15, 0, 0);
			EdgeCollider2D ecc = go4c.AddComponent<EdgeCollider2D>();
			ecc.setBox(2f, 15f);

			((Camera)go.GetComponent<Camera>()).backgroundColor = Microsoft.Xna.Framework.Color.Black;
			//((Camera)go.GetComponent<Camera>()).rect = new Rect(0.0f, 0.0f, 1f, 1f);
			((Camera)go.GetComponent<Camera>()).rect = new Rect(0.25f, 0.25f, 0.5f, 0.5f);

			//Awake events
			foreach (GameObject g in currentScene.gameObjects)
			{
				if (!g.activeInHeiarchy) continue;

				foreach (MonoBehaviour c in g.scripts)
				{
					if (c.isActiveAndEnabled)
						c.CallAwake();
				}
			}

			//Start events
			foreach (GameObject g in currentScene.gameObjects)
			{
				if (!g.activeInHeiarchy) continue;

				foreach (MonoBehaviour c in g.scripts)
				{
					if(c.isActiveAndEnabled)
						c.Start();
				}
			}
		}

		//LoadSceneAsync Loads the scene asynchronously in the background.
		//MergeScenes This will merge the source scene into the destinationScene. This function merges the contents of the source scene into the destination scene, and deletes the source scene.All GameObjects at the root of the source scene are moved to the root of the destination scene. NOTE: This function is destructive: The source scene will be destroyed once the merge has been completed.
		//MoveGameObjectToScene Move a GameObject from its current scene to a new scene.It is required that the GameObject is at the root of its current scene.
		//SetActiveScene Set the scene to be active.
		//UnloadScene Unloads all GameObjects associated with the given scene.

		#endregion Public Static Methods


		#region Internal Static Methods

		internal static void CreateGameObject(GameObject go)
		{
			currentScene.AddRootGameObject(go);
		}

		#endregion Internal Static Methods


	}
}

