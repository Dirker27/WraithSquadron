using UnityEngine;
using System.Collections;

public class TurboLaser : MonoBehaviour
{
	public float damage = 10f;
	public float range = 500f;
	private float speed = 250f;
	public Color color = Color.green;

	private Vector3 startPos;

	// Use this for initialization
	void Start () {
		gameObject.AddComponent<Rigidbody> ();
		rigidbody.useGravity = false;


		startPos = transform.position;

		renderer.material.color = color;

		transform.localScale = new Vector3 (0.1f, 0.1f, 4f);

		rigidbody.velocity = transform.forward * speed;
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 delta = transform.position - startPos;
		if (delta.magnitude > range) {
			GameObject.Destroy(gameObject);
		}
	}

	void OnCollisionEnter(Collision col) {
		GameObject.Destroy (gameObject);
	}
}

