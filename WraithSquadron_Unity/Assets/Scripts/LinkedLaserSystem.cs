using UnityEngine;
using System.Collections.Generic;

public class LinkedLaserSystem : Weapon {

	public float FIRE_RATE = 0.2f;
	public List<LaserCannon> cannons = new List<LaserCannon> ();

	private float fireInterval;
	private int primedCannon;

	// Use this for initialization
	void Start () {
		fireInterval = 0f;
		primedCannon = 0;
	}

	public override void Target (Vector3 target) { }
	
	// Update is called once per frame
	public override void Fire () {
		if (fireInterval > FIRE_RATE) {
			cannons[primedCannon].Fire();
			primedCannon++;
			if (primedCannon == cannons.Count) {
				primedCannon = 0;
			}


			fireInterval = 0f;
		}

		fireInterval += Time.deltaTime;
	}
}