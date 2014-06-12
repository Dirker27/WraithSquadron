using UnityEngine;
using System.Collections;

public enum WeaponClassification {
	PRIMARY,
	SECONDARY,
}

public abstract class Weapon : MonoBehaviour
{
	public WeaponClassification classification = WeaponClassification.PRIMARY;
	
	// Update is called once per frame
	void Update ()
	{
		bool f = false;
		if (classification == WeaponClassification.PRIMARY) {
			f = Input.GetButton("Weapon_Primary");
		} else {
			f = Input.GetButton("Weapon_Secondary");
		}

		if (f) {
			Fire ();
		}
	}

	public abstract void Fire ();
}

