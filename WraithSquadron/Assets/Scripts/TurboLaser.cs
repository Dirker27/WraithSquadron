using UnityEngine;
using System.Collections;

public class TurboLaser : MonoBehaviour
{
	public float damage = 20f;
	public float range = 100f;
	private float speed = 75f;
	public Color color = Color.green;

	private Vector3 startPos;

	// Use this for initialization
	void Start () {
		// Transform
		transform.parent = GameObject.FindGameObjectWithTag ("LaserDump").transform;
		startPos = transform.position;

		// Box Collider
		BoxCollider col = gameObject.AddComponent<BoxCollider> ();
		col.size = new Vector3 (0.025f, 0.025f, 3f);

		// Rigidbody
		gameObject.AddComponent<Rigidbody> ();
		GetComponent<Rigidbody>().velocity = transform.forward * speed;
		GetComponent<Rigidbody>().useGravity = false;

		// Line Renderer
		LineRenderer lr = gameObject.AddComponent<LineRenderer> ();
		lr.material = new Material (Shader.Find("Particles/Additive"));
		lr.SetWidth (0.05f, 0.05f);
		lr.SetColors (color, color);
		lr.castShadows = false;
		lr.receiveShadows = false;
		lr.SetPosition(0, transform.position - (transform.forward * -1.5f));
		lr.SetPosition(1, transform.position - (transform.forward * 1.5f));
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 delta = transform.position - startPos;
		if (delta.magnitude > range) {
			GameObject.Destroy(gameObject);
		}

		LineRenderer lr = GetComponent<LineRenderer> ();
		lr.SetColors (color, color);
		lr.SetPosition(0, transform.position - (transform.forward * -1.5f));
		lr.SetPosition(1, transform.position - (transform.forward * 1.5f));
	}

	void OnCollisionEnter(Collision col) {
		GameObject.Destroy (gameObject);
	}
}