using UnityEngine;
using System.Collections;

public class Orbit : MonoBehaviour {

	public Transform center;
	public Vector3 axis = Vector3.up;
	public float radius = 500.0f;
	public float radiusSpeed = 0.5f;
	public float rotationSpeed = 15.0f;

	// Use this for initialization
	void Start () {
		transform.position = (transform.position - center.position).normalized * radius + center.position;
		rotationSpeed = Random.Range(5.0f, 15.0f);
	}
	
	// Update is called once per frame
	void Update () {
		transform.RotateAround (center.position, axis, rotationSpeed * Time.deltaTime);
	 	Vector3 desiredPosition = (transform.position - center.position).normalized * radius + center.position;
		transform.position = Vector3.MoveTowards(transform.position, desiredPosition, Time.deltaTime * radiusSpeed);
	}
}