using UnityEngine;
using System.Collections;

public class Movement : MonoBehaviour {

	public float distance = 2.0F;

	private float yPos;
	private float rotX;
	private float rotY;
	private float rotZ;

	// Use this for initialization
	void Start () {
		yPos = transform.position.y;
		rotX = Random.value * 100;
		rotY = Random.value * 100;
		rotZ = Random.value * 100;
	}
	
	// Update is called once per frame
	void Update () {
		move ();
		rotate ();
	}

	void move() {
		transform.position = new Vector3(transform.position.x, yPos + Mathf.PingPong(Time.time, distance), transform.position.z);
	}

	void rotate() {
		transform.Rotate (new Vector3 (rotX, rotY, rotZ) * Time.deltaTime);
	}
		
}
