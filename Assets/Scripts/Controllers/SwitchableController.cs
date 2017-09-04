using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchableController : Singleton<SwitchableController> {
	private ColourController colourController;

	private List<SwitchableObject> colourOneSwitchables;
	private List<SwitchableObject> colourTwoSwitchables;

	public void Init() {
		colourController = ColourController.instance;

		colourOneSwitchables = new List<SwitchableObject>();
		colourTwoSwitchables = new List<SwitchableObject>();

		foreach (GameObject switchable in GameObject.FindGameObjectsWithTag("Switchable")) {
			SwitchableObject s = switchable.GetComponent<SwitchableObject>();
			s.Init();

			if (s.colour == SwitchableColour.colourOne)
				colourOneSwitchables.Add(s);
			else
				colourTwoSwitchables.Add(s);
		}

		Switch();
	}

	public void Switch() {
		if(colourController.CurrentColour == SwitchableColour.colourOne) {
			int i;
			for (i = 0; i < colourOneSwitchables.Count; i++) {
				colourOneSwitchables[i].Activate();
			}

			for (i = 0; i < colourTwoSwitchables.Count; i++) {
				colourTwoSwitchables[i].Deactivate();
			}
		} else {
			int i;
			for (i = 0; i < colourOneSwitchables.Count; i++) {
				colourOneSwitchables[i].Deactivate();
			}

			for (i = 0; i < colourTwoSwitchables.Count; i++) {
				colourTwoSwitchables[i].Activate();
			}
		}

	}
}
