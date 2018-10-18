using UnityEngine;

public class PlayerMovement : MonoBehaviour {

	public Rigidbody rigidBody;
	public float forwardForce = 2000f;
	public float sidewaysForce = 500f;
	public bool isRunnerGameplay = true;

	// Use this for initialization
	void Start () {



	}
	
	// Update is called once per frame
	void FixedUpdate () {
	
		if (isRunnerGameplay) {
			rigidBody.AddForce (0, 0, forwardForce * Time.deltaTime);
		}

		if (Input.GetKey ("w")) {
			rigidBody.AddForce (0, 0, forwardForce * Time.deltaTime);
		}

		if (Input.GetKey ("s")) {
			rigidBody.AddForce (0, 0, -forwardForce * Time.deltaTime);
		}

		if (Input.GetKey ("d")) {
			rigidBody.AddForce (sidewaysForce * Time.deltaTime, 0, 0);
		}

		if (Input.GetKey ("a")) {
			rigidBody.AddForce (-sidewaysForce * Time.deltaTime, 0, 0);
		}
	}
}
