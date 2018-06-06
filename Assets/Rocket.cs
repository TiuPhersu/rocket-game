using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour
{

    Rigidbody rigidBody;
    AudioSource audioSource;

    // Use this for initialization
    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        Thrust();
        Rotate();
    }

    private void Thrust(){
        // thrust while rotating
        if (Input.GetKey(KeyCode.Space))
        {
            //moves up
            print("boost");
            rigidBody.AddRelativeForce(Vector3.up);
            if (!audioSource.isPlaying)
            {
                audioSource.Play();
            }
        }
        else
        {
            audioSource.Stop();
        }
    }
    private void Rotate(){
        rigidBody.freezeRotation = true; //take manual control of rotation

        // control directions
        if (Input.GetKey(KeyCode.A)){
            //move to the left
            transform.Rotate(Vector3.forward);
            print("Rotating left");
        }
        else if (Input.GetKey(KeyCode.D))
        {
            //move to the right
            transform.Rotate(Vector3.back);
            print("Rotating right");
        }
        rigidBody.freezeRotation = false; //resume physics control of rotation
    }
}

 
