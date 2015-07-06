using UnityEngine;
using System.Collections;

public class PlayerManager : MonoBehaviour {
	public static PlayerManager current;

	void Awake () {
		if (current != null) {
			DestroyImmediate(gameObject);
			return;
		}

		current = this;
	}
}
