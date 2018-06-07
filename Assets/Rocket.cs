using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour
{
    [SerializeField] float rcsThrust = 100f;
    [SerializeField] float mainThrust = 100f;
    Rigidbody rigidBody;
    AudioSource audioSource;

    // Use this for initialization
    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update(){
        Thrust();
        Rotate();
    }

    void OnCollisionEnter(Collision collision){
        switch (collision.gameObject.tag) {
            case "Friendly":
                // do nothing
                break;
            case "Fuel":
                print("Fuel");
                break;
            default:
                print("Dead");
                //Kill the player
                break;
        }
    }

    private void Thrust(){
        // thrust while rotating
        if (Input.GetKey(KeyCode.Space)){
            //moves up
            print("boost");
            rigidBody.AddRelativeForce(Vector3.up * mainThrust);
            if (!audioSource.isPlaying){//so it doesn't layer
                audioSource.Play();
            }
        }
        else{
            audioSource.Stop();
        }
    }
    private void Rotate(){
        rigidBody.freezeRotation = true; //take manual control of rotation

        float rotationThisFrame = rcsThrust * Time.deltaTime;

        // control directions
        if (Input.GetKey(KeyCode.A)){
            //move to the left
            transform.Rotate(Vector3.forward * rotationThisFrame);
            print("Rotating left");
        }
        else if (Input.GetKey(KeyCode.D))
        {
            //move to the right
            transform.Rotate(Vector3.back * rotationThisFrame);
            print("Rotating right");
        }
        rigidBody.freezeRotation = false; //resume physics control of rotation
    }
}

 
