using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(BoxCollider2D))]
public class DeathZone : MonoBehaviour {
	private void OnTriggerEnter2D(Collider2D collision) {
		if(collision.tag == "Player") {
			//THIS NEEDS TO CHANGE! 1 MILLION PERCENT IT NEEDS TO CHANGE!
			SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
		}
	}

	void OnDrawGizmosSelected() {
		Gizmos.color = new Color(1f, 0f, 0f, 0.25f);
		Gizmos.DrawCube(transform.position, transform.GetComponent<BoxCollider2D>().bounds.extents * 2);
	}
}
