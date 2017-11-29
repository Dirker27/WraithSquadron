using UnityEngine;
using System.Collections;

public class CameraTracking : MonoBehaviour
{
	public float freelookSmooth = 2f;

	private GameObject player;
	private FlightController fc;

	private Vector3 basePosition;
	private Quaternion baseRotation;

	private Vector3 freePosDiff;
	private Vector3 freeRotDiff;
	private float freeHorizontalTarget;
	private float freeHorizontalCurrent;
	private float freeVerticalTarget;
	private float freeVerticalCurrent;

	private float target_dx;
	private float target_dy;
	private float target_dz;
	private float current_dx;
	private float current_dy;
	private float current_dz;

	private Vector3 smartRotDiff;

	private float distFromPlayer = 5f;

	private FlightController flightData;

	// Use this for initialization
	void Start ()
	{
		player = GameObject.FindGameObjectWithTag ("Player");
		flightData = player.GetComponent<FlightController> ();
		fc = player.GetComponent<FlightController> ();

		freeHorizontalTarget = 0f;
		freeHorizontalCurrent = 0f;
		freeVerticalTarget = 0f;
		freeVerticalCurrent = 0f;

		target_dx = 0f;
		target_dy = 0.03f;
		target_dz = -0.2f;

		basePosition = transform.position;
		baseRotation = transform.rotation;
	}
	
	// Update is called once per frame
	void Update () {
		/*
		Follow ();
		Freelook ();

		transform.position = basePosition + freePosDiff;

		Vector3 br = baseRotation.eulerAngles;
		transform.rotation = Quaternion.Euler (br.x + freeRotDiff.x,
		                                      br.y + freeRotDiff.y,
		                                      br.z + freeRotDiff.z);
		*/
		Follow ();
		//Freelook ();
		//SmartLook ();
		
		transform.position = basePosition + freePosDiff;
		
		Vector3 br = baseRotation.eulerAngles;
		transform.rotation = Quaternion.Euler (br.x + freeRotDiff.x,
		                                       br.y + freeRotDiff.y,
		                                       br.z + freeRotDiff.z);

		//Vector3 lookTarget = player.transform.position + (player.transform.forward * distFromPlayer);
		//transform.LookAt (lookTarget);
	}

	private void Follow () {
		/*distFromPlayer = 1f;//(fc.thrust / fc.MAX_THRUST) + 1f;
		Vector3 targetPosition = player.transform.position - (distFromPlayer  * (3f * player.transform.forward) - (1f * player.transform.up));
		basePosition = Vector3.Lerp (basePosition, targetPosition, 5f * Time.deltaTime);

		Vector3 targetBullseye = player.transform.position + (10f * player.transform.forward);
		//baseRotation = Quaternion.Lerp (baseRotation, player.transform.rotation, 2f * Time.deltaTime);
		transform.LookAt (targetBullseye);*/
		distFromPlayer = (fc.thrust * 4f / fc.MAX_THRUST) + 0.4f;

		Vector3 flightControls = InputManager.GetFlightControl();
		target_dx = Mathf.Sin(flightControls.y / 0.5f) * distFromPlayer;
		target_dy = Mathf.Sin(flightControls.x / 1.5f) * distFromPlayer + 0.5f;
		target_dz = Mathf.Cos(flightControls.y / 2f) * -distFromPlayer;

		current_dx = Mathf.Lerp (current_dx, target_dx, 2f * Time.deltaTime);
		current_dy = Mathf.Lerp (current_dy, target_dy, 2f * Time.deltaTime);
		current_dz = Mathf.Lerp (current_dz, target_dz, 2f * Time.deltaTime);

		basePosition = player.transform.position + 
				current_dx * player.transform.right + 
				current_dy * player.transform.up + 
				current_dz * player.transform.forward;

		Quaternion targetRotation = player.transform.rotation;
		Vector3 targetEuler = targetRotation.eulerAngles;
		//baseRotation = Quaternion.Euler (targetEuler.x, targetEuler.y, flightData.nativeRoll);
		baseRotation = Quaternion.Lerp (baseRotation, player.transform.rotation, 5f * Time.deltaTime);
	}

	private void Freelook() {
		float horizontal = Input.GetAxis ("Freelook_Horizontal");
		float vertical = Input.GetAxis ("Freelook_Vertical");

		freeVerticalTarget = 70f * vertical;
		freeHorizontalTarget = 70f * horizontal;
		
		freeVerticalCurrent = Mathf.Lerp (freeVerticalCurrent,
		                                  freeVerticalTarget, 
		                                  freelookSmooth * Time.deltaTime);
		freeHorizontalCurrent = Mathf.Lerp (freeHorizontalCurrent,
		                                    freeHorizontalTarget, 
		                                    freelookSmooth * Time.deltaTime);

		freePosDiff = new Vector3 (0f, 0f, 0f);
		freeRotDiff = new Vector3 (freeVerticalCurrent, freeHorizontalCurrent, 0f);
		//freeRotDiff = (freeVerticalCurrent * player.transform.right) + (freeHorizontalCurrent * player.transform.up);
	}
}

