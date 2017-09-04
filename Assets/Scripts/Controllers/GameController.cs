using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : Singleton<GameController> {
	public float worldGravity;

	void Awake () {
		Physics2D.gravity = new Vector2(0, -worldGravity);

		CameraController.Instance.Init();
		ColourController.Instance.Init();
		SwitchableController.Instance.Init();

		GameObject.FindWithTag("Player").GetComponent<Player>().Init();
	}
}
