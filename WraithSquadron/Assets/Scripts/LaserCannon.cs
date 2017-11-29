using UnityEngine;
using System.Collections;

public class LaserCannon : Weapon
{
	public Color color = Color.green;
	public float range = 100f;
	public float offset = 2f;

	public override void Target(Vector3 target) { }

	public override void Fire() {
		GameObject laser = new GameObject ();
		laser.transform.position = transform.position + (transform.forward * offset);
		laser.transform.rotation = transform.rotation;

		TurboLaser t = laser.AddComponent<TurboLaser>();
		t.color = color;
		t.range = range;
	}
}

