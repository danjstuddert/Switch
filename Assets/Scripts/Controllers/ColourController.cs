using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SwitchableColour { colourOne, colourTwo }

[CreateAssetMenu(fileName = "New Pallet", menuName = "Colour Pallet", order = 1)]
public class ColourPallet : ScriptableObject {
	public Color colourOne;
	public Color colourTwo;
}

public class ColourController : Singleton<ColourController> {
	public SwitchableColour startColour;
	public ColourPallet pallet;
	public Color colourOne;
	public Color colourTwo;

	public float fadedAlpha;

	public SwitchableColour CurrentColour { get; private set; }

	public void Init() {
		CurrentColour = startColour;
	}

	public Color GetColour(SwitchableColour colour) {
		return colour == SwitchableColour.colourOne ? colourOne : colourTwo;
	}

	public void UpdateCurrentColour() {
		CurrentColour = CurrentColour == SwitchableColour.colourOne ? SwitchableColour.colourTwo : SwitchableColour.colourOne;
	}

	public void UpdateColour(SpriteRenderer sprite, bool fade = false) {
		Color c = sprite.color;
		c.a = fade ? fadedAlpha : 1f;

		sprite.color = c;
	}

}
