using UnityEngine;
using System.Collections;

public enum Faction {
	// Space Religon
	JEDI,
	SITH,
	// Clone Wars
	REPUBLIC,
	CONFEDERACY,
	// Galactic Civil War
	ALLIANCE,
	EMPIRE,
	// Crime Lords
	BLACK_SUN,
	ZANN_CONSORTIUM,
	// Independent
	PIRATE,
	MANDALORE
}

public class Ship : MonoBehaviour
{
	public float SHEILD_CAPACITY = 100f;
	public float SHEILD_RECHARGE = 10f;
	public float SHEILD_DELAY = 10f;

	public Faction faction = Faction.EMPIRE;

	public bool crippled = false;
	public float hullIntegrity = 100f;
	public float sheildIntegrity = 100f;

	private FlightController flightControl;
	
	// Use this for initialization
	void Start () {
		flightControl = GetComponent<FlightController>();
	}
	
	// Update is called once per frame
	void Update ()
	{
		// Cripple Ship
		if (hullIntegrity <= 0f && crippled == false) {
			flightControl.crippled = true;
			crippled = true;

			rigidbody.useGravity = true;
			rigidbody.velocity = flightControl.trajectory;
			rigidbody.drag = 0f;
			rigidbody.angularDrag = 0.01f;
			rigidbody.AddTorque(flightControl.trajectory * 1000f);
		}
	}

	void OnCollisionEnter (Collision col) {
		//hullIntegrity -= col.relativeVelocity.magnitude * 0.05f;

		TurboLaser laser = col.gameObject.GetComponent<TurboLaser> ();
		if (laser) {
			hullIntegrity -= laser.damage;
			//hullIntegrity -= col.rigidbody.gameObject.GetComponent<TurboLaser>().damage;
			Debug.Log("LASER HIT");
		}

		if (hullIntegrity <= -10) {
			//GameObject.Destroy(gameObject);
		}
	}
}

