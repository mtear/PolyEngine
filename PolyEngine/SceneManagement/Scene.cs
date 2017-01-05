using System.Collections.Generic;

namespace PolyEngine.SceneManagement
{
	public struct Scene
	{


		#region Public Variables


		//buildIndex Returns the index of the scene in the Build Settings.Always returns -1 if the scene was loaded through an AssetBundle.
	  	//isDirty Returns true if the scene is modifed.
	  	//isLoaded Returns true if the scene is loaded.
   		//name Returns the name of the scene.


		//path Returns the relative path of the scene.Like: "Assets/MyScenes/MyScene.unity".
		//rootCount The number of root transforms of this scene.


		#endregion Public Variables


		#region Internal Variables

		internal List<GameObject> rootGameObjects;
		internal List<GameObject> gameObjects;

		#endregion Internal Variables


		#region Public Methods

		//GetRootGameObjects Returns all the root game objects in the scene.
		public GameObject[] GetRootGameObjects()
		{
			return rootGameObjects.ToArray();
		}

		//IsValid Whether this is a valid scene.A scene may be invalid if, for example, you tried to open a scene that does not exist. In this case, the scene returned from EditorSceneManager.OpenScene would return False for IsValid.
		public bool IsValid()
		{ //TODO Fix this and make it real
			return true;
		}

		#endregion Public Methods


		#region Internal Methods

		internal void AddRootGameObject(GameObject go)
		{
			go.scene = this;
			rootGameObjects.Add(go);
			gameObjects.Add(go);
		}

		internal void UpdateSceneParent(GameObject go)
		{
			if (rootGameObjects.Contains(go))
			{ //If it WAS a root object
				if (go.transform.parent != null)
				{ //No longer a root object
					rootGameObjects.Remove(go);
				}
			}
			else { //Was NOT a root object
				if (go.transform.parent == null)
				{ // Is NOW a root object
					rootGameObjects.Add(go);
				}
			}
		}

		internal void UpdateAll()
		{
			foreach (GameObject go in gameObjects)
			{
				if (!go.activeInHeiarchy) continue;
				foreach (MonoBehaviour script in go.scripts)
				{
					if(script.enabled)
						script.Update();
				}
			}
		}

		internal void FixedUpdateAll()
		{
			foreach (GameObject go in gameObjects)
			{
				if (!go.activeInHeiarchy) continue;
				foreach (MonoBehaviour script in go.scripts)
				{
					if(script.enabled)
						script.FixedUpdate();
				}
			}
		}

		internal void LateUpdateAll()
		{
			foreach (GameObject go in gameObjects)
			{
				if (!go.activeInHeiarchy) continue;
				foreach (MonoBehaviour script in go.scripts)
				{
					if (script.enabled)
						script.LateUpdate();
				}
			}
		}

		internal void PreCullAll()
		{
			foreach (GameObject go in gameObjects)
			{
				if (!go.activeInHeiarchy) continue;
				foreach (MonoBehaviour script in go.scripts)
				{
					if (script.enabled)
						script.OnPreCull();
				}
			}
		}

		#endregion Internal Methods


	}
}

