using UnityEngine;
using System.Collections;

public class LaserCannon : Weapon {

	public float FIRE_RATE = 0.2f;
	private float fireInterval;

	// Use this for initialization
	void Start () {
		fireInterval = 0f;
	}
	
	// Update is called once per frame
	public override void Fire () {
		if (fireInterval > FIRE_RATE) {
			GameObject laser = GameObject.CreatePrimitive(PrimitiveType.Cube);
			laser.transform.position = transform.position + (transform.forward * 5f);
			laser.transform.rotation = transform.rotation;
			laser.AddComponent<TurboLaser>();

			fireInterval = 0f;
		}

		fireInterval += Time.deltaTime;
	}
}