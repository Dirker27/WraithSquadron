using UnityEngine;
using System.Collections;

public abstract class Weapon : MonoBehaviour
{
	public abstract void Target (Vector3 t);

	public abstract void Fire ();
}

