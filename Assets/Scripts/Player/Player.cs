using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
	public float moveSpeed;
	public LayerMask environmentMask;
	public float rayLength;

	// Movement 
	private Vector2 velocity;
	private Rigidbody2D rBody;

	// Colour Switching
	private ColourController colourController;
	private SwitchableController switchableController;
	private SpriteRenderer sprite;

	private TrailRenderer trail;

	public void Init () {
		rBody = GetComponent<Rigidbody2D>();

		colourController = ColourController.Instance;
		switchableController = SwitchableController.Instance;
		sprite = GetComponentInChildren<SpriteRenderer>();
		sprite.color = colourController.GetColour(colourController.CurrentColour);

		trail = GetComponent<TrailRenderer>();
		trail.startColor = sprite.color;
		trail.endColor = sprite.color;
	}

	void Update () {
		UpdateVelocity();
		CheckInput();
	}

	private void FixedUpdate() {
		if (rBody == null)
			return;

		rBody.velocity = velocity;
	}

	private void UpdateVelocity(){
		if (rBody == null)
			return;

		velocity = rBody.velocity;
		velocity.x = moveSpeed;
	}

	private void CheckInput(){
		if(Input.GetButtonDown("GravitySwitch")){
			SwitchGravity();
		}

		if (Input.GetButtonDown("ColourSwitch")) {
			SwitchColour();
		}
	}

	private void SwitchGravity() {
		if (IsGrounded() == false)
			return;

		Physics2D.gravity = new Vector2(0, -Physics2D.gravity.y);
		velocity.y = Physics2D.gravity.y;

		UpdateVelocity();
	}

	private void SwitchColour() {
		colourController.UpdateCurrentColour();
		sprite.color = colourController.GetColour(colourController.CurrentColour);
		trail.startColor = sprite.color;
		trail.endColor = sprite.color;

		switchableController.Switch();
	}

	private bool IsGrounded() {
		//Which way should the ray fire?
		Vector3 direction = Physics2D.gravity.y > 0 ? transform.up : -transform.up;

		if (Physics2D.Raycast(transform.position, direction, rayLength, environmentMask))
			return true;

		return false;
	}
}
