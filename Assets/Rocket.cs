using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour {

    Rigidbody rigidBody;

	// Use this for initialization
	void Start () {
        rigidBody = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
        ProcessInput();
	}

    private void ProcessInput() {
        // thrust while rotating
        if (Input.GetKey(KeyCode.Space)) {
            print("boost");
            rigidBody.AddRelativeForce(Vector3.up);
        }

        // control directions
        if (Input.GetKey(KeyCode.A)) {
            print("Rotating left");
        }
        else if (Input.GetKey(KeyCode.D)) {
            print("Rotating right");
        }
    }
}
