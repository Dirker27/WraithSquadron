using UnityEngine;
using System.Collections;

public class FlightController : MonoBehaviour {
	// Flight Behavior
	public float MIN_THRUST = 10f;    // MGLT
	public float MAX_THRUST = 100f;   // MGLT
	public float AVE_THRUST = 45f;    // MGLT
	public float ACCELLERATION = 30f; // MGLT/Second
	public float DECELLERATION = 50f; // MGLT/Second
	public float MAX_BANK = 60f;      // Degrees
	public float TURN_SPEED = 50f;    // Degrees/Second
	public float ROLL_SPEED = 100f;   // Degrees/Second
	
	public float thrust = 25f; // MGLT

	public float turnSpeed = 25f;
	public float pitchSpeed = 25f;
	public float rollSpeed = 25f;
	public float bankSmooth = 5f;

	private const float MGLT_CONVERSION = 0.2777778f;

	public float nativeRoll;
	private float bankTarget;
	private float bankCurrent;

	public Vector3 trajectory;

	public Ship shipData;

	// Use this for initialization
	void Start () {
		shipData = GetComponent<Ship>();

		nativeRoll = transform.rotation.z;
		bankTarget = 0f;
		bankCurrent = 0f;

		trajectory = Vector3.zero;
	}

	public void Steer(Vector3 input) {
		// Reset Augmented Roll
		transform.Rotate (0f, 0f, -bankCurrent);

		/*bool rudder_left = Input.GetButton ("Rudder_Left");
		bool rudder_right = Input.GetButton ("Rudder_Right");
		float rudder = (rudder_left) ? -1f : (rudder_right) ? 1f : 0f;
		rudder *= turnSpeed * Time.deltaTime / 2f;*/

		Vector3 deltaRotation = new Vector3 (-input.x * pitchSpeed * Time.deltaTime,
		                                     input.y * turnSpeed * Time.deltaTime,// + rudder,
		                                     -input.z * rollSpeed * Time.deltaTime);
		transform.Rotate (deltaRotation);
		nativeRoll += -input.z * rollSpeed * Time.deltaTime;

		//- Banking ----------
		bankTarget = -input.y * MAX_BANK;

		bankCurrent = Mathf.Lerp (bankCurrent, bankTarget, bankSmooth * Time.deltaTime);

		//float z = -yaw * rollSpeed * Time.deltaTime;
		//artificialRoll += z;
		transform.Rotate (0f, 0f, bankCurrent);
	}

	public void Throttle(float throttle, float brake) {
		thrust = Mathf.Lerp (thrust, AVE_THRUST, 0.5f * Time.deltaTime);

		thrust += throttle * ACCELLERATION * Time.deltaTime;
		if (thrust > MAX_THRUST) {
			thrust = MAX_THRUST;
		}
		thrust -= brake * DECELLERATION * Time.deltaTime;
		if (thrust < MIN_THRUST) {
			thrust = MIN_THRUST;
		}
		
		Vector3 moveDirection = transform.forward;
		float velocity = thrust * MGLT_CONVERSION;
		trajectory = moveDirection * velocity;
		transform.position += trajectory * Time.deltaTime;
	}
}