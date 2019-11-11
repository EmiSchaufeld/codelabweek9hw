using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camera_parent_rotate : MonoBehaviour
{
	public Transform lookAtPlayer;
	public float offsetZ;
	public float offsetY;
	float currentX = 0f;
	float currentY = 0f;

	public float speedX;
	public float speedY;
	public float angleMinY;
	public float angleMaxY;

	Vector3 offsetVector;
	Quaternion rotation;

	// Update is called once per frame
	void Update()
	{
		currentX += Input.GetAxis("Mouse X") * speedX;
		currentY += Input.GetAxis("Mouse Y") * speedY;
		currentY = Mathf.Clamp(currentY, angleMinY, angleMaxY);
	}

	void LateUpdate()
	{
		offsetVector = new Vector3(0, offsetY, -offsetZ);
		rotation = Quaternion.Euler(currentY, currentX, 0);
		transform.position = lookAtPlayer.position + rotation * offsetVector; //(x,y,z)
		transform.LookAt(lookAtPlayer.position);
	}
}
