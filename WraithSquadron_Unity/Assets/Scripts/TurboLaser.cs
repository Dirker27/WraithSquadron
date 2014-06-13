using UnityEngine;
using System.Collections;

public class TurboLaser : MonoBehaviour
{
	public float damage = 20f;
	public float range = 50f;
	private float speed = 75f;
	public Color color = Color.green;

	private Vector3 startPos;

	// Use this for initialization
	void Start () {
		gameObject.AddComponent<Rigidbody> ();
		rigidbody.useGravity = false;


		startPos = transform.position;

		renderer.material.color = color;

		transform.localScale = new Vector3 (0.05f, 0.05f, 3f);

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

