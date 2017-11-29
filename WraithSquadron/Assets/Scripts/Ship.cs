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
	// Ship Allegience
	public Faction faction = Faction.EMPIRE;

	// AI or Player
	public bool PLAYER_CONTROLLED = false;

	// Weaponry
	public Weapon primaryWeapon = null;
	public Weapon secondaryWeapon = null;

	// Ship Health
	public float SHEILD_CAPACITY = 100f;
	public float SHEILD_RECHARGE = 10f;
	public float SHEILD_DELAY = 10f;

	// Acting Fields
	public bool crippled = false;
	public float hullIntegrity = 100f;
	public float sheildIntegrity = 100f;

	private FlightController flightControl;

	private Vector3 steer;
	private float throttle;
	private float brake;
	private bool primaryFire;
	private bool secondaryFire;
	
	// Use this for initialization
	void Start () {
		flightControl = GetComponent<FlightController>();
	}
	
	// Update is called once per frame
	void Update ()
	{
		// Cripple Ship
		if (hullIntegrity <= 0f && crippled == false) {
			crippled = true;

			GetComponent<Rigidbody>().useGravity = true;
			GetComponent<Rigidbody>().velocity = flightControl.trajectory;
			GetComponent<Rigidbody>().drag = 0f;
			GetComponent<Rigidbody>().angularDrag = 0.01f;
			GetComponent<Rigidbody>().AddTorque(flightControl.trajectory * 1000f);
		}

		if (! crippled) {
			GetControls();

			flightControl.Steer(steer);
			flightControl.Throttle(throttle, brake);

			if (primaryWeapon != null && primaryFire) {
				primaryWeapon.Fire();
			}
			if (secondaryWeapon != null && secondaryFire) {
				secondaryWeapon.Fire();
			}
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

	private void GetControls() {
		if (PLAYER_CONTROLLED) {
			steer = InputManager.GetFlightControl();
			throttle = InputManager.GetThrottle();
			brake = InputManager.GetBrake();
			primaryFire = InputManager.GetPrimaryFire();
			secondaryFire = InputManager.GetSecondaryFire();
		} else {
			steer = Vector3.zero;
			throttle = 0f;
			brake = 0f;
			primaryFire = false;
			secondaryFire = false;
		}
	}
}

