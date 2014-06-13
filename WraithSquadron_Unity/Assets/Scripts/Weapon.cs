using UnityEngine;
using System.Collections;

public enum WeaponClassification {
	PRIMARY,
	SECONDARY,
}

public abstract class Weapon : MonoBehaviour
{
	public WeaponClassification classification = WeaponClassification.PRIMARY;

	public abstract void Fire ();
}

