using UnityEngine;
using System.Collections;

public class PlayerControls : MonoBehaviour {

	public enum RotationAxes { MouseXAndY = 0, MouseX = 1, MouseY = 2 }
	public RotationAxes axes = RotationAxes.MouseXAndY;
	public float sensitivityX = 15F;
	public float sensitivityY = 15F;
	public float minimumX = -360F;
	public float maximumX = 360F;
	public float minimumY = -60F;
	public float maximumY = 60F;
	public float movementSpeed = 50.0f;

	private float rotationY = 0F;
	private Vector3 moveDirection;
	private CharacterController controller;


	void Start() {
		moveDirection = Vector3.zero;
		controller = GetComponent<CharacterController>();
	}

	void LateUpdate () {
		RotateView ();
		MovePlayer ();
		controller.Move (moveDirection * Time.deltaTime);
	}

	void RotateView() {
		if (axes == RotationAxes.MouseXAndY)
		{
			float rotationX = transform.localEulerAngles.y + Input.GetAxis("Mouse X") * sensitivityX;

			rotationY += Input.GetAxis("Mouse Y") * sensitivityY;
			rotationY = Mathf.Clamp (rotationY, minimumY, maximumY);

			transform.localEulerAngles = new Vector3(-rotationY, rotationX, 0);
		}
		else if (axes == RotationAxes.MouseX)
		{
			transform.Rotate(0, Input.GetAxis("Mouse X") * sensitivityX, 0);
		}
		else
		{
			rotationY += Input.GetAxis("Mouse Y") * sensitivityY;
			rotationY = Mathf.Clamp (rotationY, minimumY, maximumY);

			transform.localEulerAngles = new Vector3(-rotationY, transform.localEulerAngles.y, 0);
		}
	}

	void MovePlayer() {
		moveDirection = new Vector3(Input.GetAxis("Horizontal") * movementSpeed, 0, Input.GetAxis("Vertical") * movementSpeed);
		moveDirection = transform.TransformDirection(moveDirection);
	}
}
