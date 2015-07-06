using UnityEngine;
using System.Collections;

public class LevelTransition : MonoBehaviour {
	[SerializeField] string loadScene;

	void OnTriggerEnter2D () {
		LevelLoader.current.LoadScene(loadScene);
	}
}
