using UnityEngine;
using System.Collections;

public class FlightController : MonoBehaviour {
	public bool turnAssist = true;
	public bool crippled = false;

	public float MAX_THRUST = 100f; // MGLT
	public float MIN_THRUST = 5f; // MGLT
	public float MAX_BANK = 60f;
	public float thrust = 25f; // MGLT

	public float turnSpeed = 25f;
	public float pitchSpeed = 25f;
	public float rollSpeed = 25f;
	public float bankSmooth = 5f;
	public float throttleAccelleration = 30f;
	public float brakeDecelleration = 50f;

	private float nativeRoll;
	private float bankTarget;
	private float bankCurrent;

	public Vector3 trajectory;

	// Use this for initialization
	void Start () {
		nativeRoll = transform.rotation.z;
		bankTarget = 0f;
		bankCurrent = 0f;

		trajectory = Vector3.zero;
	}
	
	// Update is called once per frame
	void Update () {
		if (! crippled) {
			Steering ();
			Thrusting ();
		}
	}

	private void Steering() {
		if (Input.GetButtonDown("TurnAssist")) {
			turnAssist = !turnAssist;
		}

		// Reset Augmented Roll
		transform.Rotate (0f, 0f, -bankCurrent);

		float pitch = Input.GetAxis ("Pitch");
		float yaw = (turnAssist) ? Input.GetAxis ("Yaw") : 0f;
		float roll = (turnAssist) ? 0f : Input.GetAxis ("Roll");

		bool rudder_left = Input.GetButton ("Rudder_Left");
		bool rudder_right = Input.GetButton ("Rudder_Right");
		float rudder = (rudder_left) ? -1f : (rudder_right) ? 1f : 0f;
		rudder *= turnSpeed * Time.deltaTime / 2f;

		Vector3 deltaRotation = new Vector3 (-pitch * pitchSpeed * Time.deltaTime,
		                                     yaw * turnSpeed * Time.deltaTime + rudder,
		                                     -roll * rollSpeed * Time.deltaTime);
		transform.Rotate (deltaRotation);
		nativeRoll += -roll * rollSpeed * Time.deltaTime;

		//- Banking ----------
		bankTarget = -yaw * MAX_BANK;

		bankCurrent = Mathf.Lerp (bankCurrent, bankTarget, bankSmooth * Time.deltaTime);

		//float z = -yaw * rollSpeed * Time.deltaTime;
		//artificialRoll += z;
		transform.Rotate (0f, 0f, bankCurrent);
	}

	private void Thrusting() {
		float throttle = Input.GetAxis ("Throttle");
		float brake = Input.GetAxis ("Brake");
		thrust += throttle * throttleAccelleration * Time.deltaTime;
		if (thrust > MAX_THRUST) {
			thrust = MAX_THRUST;
		}
		thrust -= brake * brakeDecelleration * Time.deltaTime;
		if (thrust < MIN_THRUST) {
			thrust = MIN_THRUST;
		}
		
		Vector3 moveDirection = transform.forward;
		float velocity = thrust * 0.277778f;
		trajectory = moveDirection * velocity;
		transform.position += trajectory * Time.deltaTime;
	}
}