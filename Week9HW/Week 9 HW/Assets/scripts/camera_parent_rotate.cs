using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camera_parent_rotate : MonoBehaviour
{
	public Transform lookAtPlayer; //follow the player
	public float offsetZ; //how far away the camera is from the player
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
        //the x and y of the where the mouse is moving only moving as fast as i want it
        //the max range that is allowed
	}

	void LateUpdate() //make sure this happens last
	{
		offsetVector = new Vector3(0, offsetY, -offsetZ); // how offset the camera is
		rotation = Quaternion.Euler(currentY, currentX, 0); //the roation of the camera
		transform.position = lookAtPlayer.position + rotation * offsetVector; //new pos
		transform.LookAt(lookAtPlayer.position); //move with player
	}
}
