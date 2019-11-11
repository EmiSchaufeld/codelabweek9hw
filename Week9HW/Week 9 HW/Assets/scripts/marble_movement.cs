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
		cameraDirection = Camera.main.transform.TransformDirection(movement);
		GroundControl();
		if (Input.GetKey(KeyCode.Space) && onGround)
		{
			jumpInput = true;
		}

	}



	void FixedUpdate()
	{

		marbleBody.AddForce(cameraDirection * moveSpeed);

		if (jumpInput)
		{
			Jump();
			jumpInput = false;
		}





		Ray ramano = new Ray(transform.position, Vector3.down);
		Physics.Raycast(ramano, out hitEm, 1000f);
		Debug.Log(hitEm.point);
		Debug.DrawLine(transform.position, hitEm.point, Color.red);
		Debug.Log(onGround);

		if (onGround == false)
		{
			marbleBody.AddTorque(movement);
		}



	}

	void Jump()
	{

		marbleBody.velocity = new Vector3(marbleBody.velocity.x, 0f, marbleBody.velocity.z);
		marbleBody.AddForce(jumpNormal * jumpStrength);








	}


	void GroundControl()
	{
		if (Vector3.Distance(transform.position, hitEm.point) <= groundControlHeight)
		{
			onGround = true;
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
