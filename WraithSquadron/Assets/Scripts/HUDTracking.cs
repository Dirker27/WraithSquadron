using UnityEngine;
using System.Collections;

public class HUDTracking : MonoBehaviour
{
	private Transform host;

	// Use this for initialization
	void Start ()
	{
		host = GameObject.FindGameObjectWithTag ("Player").transform;
	}
	
	// Update is called once per frame
	void Update ()
	{
		transform.position = host.position;
		transform.rotation = host.rotation;
	}
}

