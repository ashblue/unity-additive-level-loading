using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LevelLoader : MonoBehaviour {
	public static LevelLoader current;
	Transform catalogue;
	Transform shared;
	GameObject player;
	Dictionary<Transform, bool> ignore = new Dictionary<Transform, bool>();

	void Awake () {
		if (current != null) {
			Destroy(gameObject);
			return;
		}

		current = this;
		player = GameObject.FindWithTag("Player");

		// Create a shared area
		shared = new GameObject().transform;
		shared.name = "_Shared";
		player.transform.SetParent(shared);

		// Setup ignored elements
		ignore[transform] = true;
		ignore[player.transform] = true;
		ignore[shared] = true;

		UpdateCatalogue(Application.loadedLevelName);
	}

	void UpdateCatalogue (string sceneName) {
		// Create the catalogue
		catalogue = new GameObject().transform;
		catalogue.name = "_" + sceneName;

		// Store all items without a parent
		foreach (Transform t in Object.FindObjectsOfType<Transform>()) {
			if (ignore.ContainsKey(t)) continue;
			
			if (t.parent == null) {
				t.SetParent(catalogue);
			}
		}
	}

	public void LoadScene (string scene) {
		StartCoroutine(LoadSceneLoop(scene));
	}

	IEnumerator LoadSceneLoop (string scene) {
		Destroy(catalogue.gameObject);
		Application.LoadLevelAdditive(scene);

		yield return null;

		UpdateCatalogue(scene);

		// @TODO Place player at proper position
		// @TODO Cleanup other player
	}
}
