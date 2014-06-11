using UnityEngine;
using System.Collections;

public class EngineLight : MonoBehaviour
{
	private FlightController fc;
	// Use this for initialization
	void Start ()
	{
		fc = GameObject.FindGameObjectWithTag ("Player").GetComponent<FlightController> ();
	}
	
	// Update is called once per frame
	void Update ()
	{
		float mag = fc.thrust / fc.MAX_THRUST;

		light.intensity = 0.25f + (mag * 0.75f);
	}
}

