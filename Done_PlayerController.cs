using UnityEngine;
using System.Collections;

[System.Serializable] // serialise class Done_Boundary for class to be visible in inspector
public class Done_Boundary
{
	public float xMin, xMax, zMin, zMax;
}

public class Done_PlayerController : MonoBehaviour
{
	public float speed;
	public float tilt;
	public Done_Boundary boundary;

	public GameObject shot; // Variable shot for game object
	public Transform shotSpawn; // access the transform cordinates of the bullet shot with variable shotSpawn
	public float fireRate;

	private float nextFire;

	void Update ()
	{
		if (Input.GetButton("Fire1") && Time.time > nextFire)
		{
			nextFire = Time.time + fireRate;
			Instantiate(shot, shotSpawn.position, shotSpawn.rotation); // Used to instantiate the quartanion rotation and the position of the bullet shots upon click of fire button
			GetComponent<AudioSource>().Play ();
		}
	}

	void FixedUpdate ()
	{
		float moveHorizontal = Input.GetAxis ("Horizontal"); //change player axis to mbed bluetooth controller
		float moveVertical = Input.GetAxis ("Vertical"); //change player axis to mbed bluetooth vertical

		Vector3 movement = new Vector3 (moveHorizontal, 0.0f, moveVertical);//negate 3-D movement on y-axis
		GetComponent<Rigidbody>().velocity = movement * speed;

		GetComponent<Rigidbody>().position = new Vector3
		(
			Mathf.Clamp (GetComponent<Rigidbody>().position.x, boundary.xMin, boundary.xMax), // bound the spaceship to within dimensions of screen X-axis
			0.0f,
			Mathf.Clamp (GetComponent<Rigidbody>().position.z, boundary.zMin, boundary.zMax) //bound the spaceship to within dimension of screen for z-axis
		);

		GetComponent<Rigidbody>().rotation = Quaternion.Euler (0.0f, 0.0f, GetComponent<Rigidbody>().velocity.x * -tilt); // unioty quarternion for tilt of spaceship on the z axis whilst moving along the x axis
	}
}
