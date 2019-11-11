using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class marble_movement : MonoBehaviour
{
	float inputX;
	float inputY;

	Rigidbody marbleBody;

	public float moveSpeed;
	public float jumpStrength;
	public float groundControlHeight; 
	public float midAirDrag;

	float defaultAirDrag;



	Vector3 movement;
	Vector3 cameraDirection;
	Vector3 jumpNormal;


	public Transform mainCamera;
	RaycastHit hitEm;
	bool onGround;
	bool jumpInput = false;

	// Use this for initialization
	void Start()
	{
		marbleBody = GetComponent<Rigidbody>();
		defaultAirDrag = marbleBody.angularDrag;
	}


	// Update is called once per frame
	void Update()
	{
		inputX = Input.GetAxis("Horizontal");
		inputY = Input.GetAxis("Vertical");
		movement = new Vector3(inputX, 0f, inputY);
		cameraDirection = Camera.main.transform.TransformDirection(movement); // the camera is controlling the movement
		GroundControl();
		if (Input.GetKey(KeyCode.Space) && onGround) //can only jump if you're already on the ground
		{
			jumpInput = true;
		}

	}



	void FixedUpdate()
	{

		marbleBody.AddForce(cameraDirection * moveSpeed);

		if (jumpInput)
		{
			Jump(); //calling function
			jumpInput = false; //if yer jumpin you can't be jumpin lol
		}





		Ray ramano = new Ray(transform.position, Vector3.down);
		Physics.Raycast(ramano, out hitEm, 1000f);
		Debug.Log(hitEm.point);
		Debug.DrawLine(transform.position, hitEm.point, Color.red);
		Debug.Log(onGround); //telling if the marble is on the ground or not

		if (onGround == false)
		{
			marbleBody.AddTorque(movement); //the marble moves with torque
		}



	}

	void Jump()
	{

		marbleBody.velocity = new Vector3(marbleBody.velocity.x, 0f, marbleBody.velocity.z);
		marbleBody.AddForce(jumpNormal * jumpStrength);
        //keep the velocity that you had before jumping and jump up








	}


	void GroundControl()
	{
		if (Vector3.Distance(transform.position, hitEm.point) <= groundControlHeight)
		{
			onGround = true; //changing the air drag based on if the marble is in the air or not
			marbleBody.angularDrag = defaultAirDrag;
		}
		else
		{
			onGround = false;

			marbleBody.angularDrag = midAirDrag;

		}

	}

    void OnCollisionStay(Collision collision)
    {
        jumpNormal = collision.contacts[0].normal;
    }
}
