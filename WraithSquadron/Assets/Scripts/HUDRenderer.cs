using UnityEngine;
using System.Collections;

public class HUDRenderer : MonoBehaviour
{
	private GameObject player;
	private FlightController flightData;
	private Ship shipData;

	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag ("Player");
		shipData = player.GetComponent<Ship> ();
		flightData = player.GetComponent<FlightController> ();
	}

	void OnGUI() {
		GUI.Label (new Rect (25f, 25f, 125f, 25f), "HULL: " + shipData.hullIntegrity);
		GUI.Label (new Rect (25f, 150f, 125f, 25f), "Thrust: " + flightData.thrust);
		GUI.Label (new Rect (25f, 200f, 125f, 25f), "Pitch: " + player.transform.rotation.eulerAngles.x);
		GUI.Label (new Rect (25f, 225f, 125f, 25f), "Yaw:   " + player.transform.rotation.eulerAngles.y);
		GUI.Label (new Rect (25f, 250f, 125f, 25f), "Roll:  " + player.transform.rotation.eulerAngles.z);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}

