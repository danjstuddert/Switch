using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SwitchableObject : MonoBehaviour {
	public SwitchableColour colour;

	protected ColourController colourController;

	protected SpriteRenderer sprite;
	protected Collider2D col;

	public virtual void Init() {
		colourController = ColourController.Instance;
		sprite = GetComponent<SpriteRenderer>();

		if (sprite.color != colourController.GetColour(colour))
			sprite.color = colourController.GetColour(colour);

		col = GetComponent<Collider2D>();
	}

	public virtual void Activate() {
		col.enabled = true;
		colourController.UpdateColour(sprite);
	}

	public virtual void Deactivate() {
		col.enabled = false;
		colourController.UpdateColour(sprite, true);
	}
}
