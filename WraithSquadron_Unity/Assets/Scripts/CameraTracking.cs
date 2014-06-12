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

	private Vector3 smartRotDiff;

	private float distFromPlayer = 5f;

	// Use this for initialization
	void Start ()
	{
		player = GameObject.FindGameObjectWithTag ("Player");
		fc = player.GetComponent<FlightController> ();

		freeHorizontalTarget = 0f;
		freeHorizontalCurrent = 0f;
		freeVerticalTarget = 0f;
		freeVerticalCurrent = 0f;

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
		transform.rotation = Quaternion.Euler (br.x + freeRotDiff.x + smartRotDiff.x,
		                                       br.y + freeRotDiff.y + smartRotDiff.y,
		                                       br.z + freeRotDiff.z + smartRotDiff.z);
	}

	private void Follow () {
		/*distFromPlayer = 1f;//(fc.thrust / fc.MAX_THRUST) + 1f;
		Vector3 targetPosition = player.transform.position - (distFromPlayer  * (3f * player.transform.forward) - (1f * player.transform.up));
		basePosition = Vector3.Lerp (basePosition, targetPosition, 5f * Time.deltaTime);

		Vector3 targetBullseye = player.transform.position + (10f * player.transform.forward);
		//baseRotation = Quaternion.Lerp (baseRotation, player.transform.rotation, 2f * Time.deltaTime);
		transform.LookAt (targetBullseye);*/
		distFromPlayer = (fc.thrust / fc.MAX_THRUST) + 2f;
		basePosition = player.transform.position - (distFromPlayer  * (0.5f * player.transform.forward) - (0.5f * player.transform.up));

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

	private void SmartLook() {
		//smartRotDiff = new Vector3 (-fc.deltaPitch * 10f, fc.deltaYaw * 10f, -fc.deltaRoll * 10f);
	}
}

