using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Rocket : MonoBehaviour
{
    [SerializeField] float rcsThrust = 100f;
    [SerializeField] float mainThrust = 100f;

    Rigidbody rigidBody;
    AudioSource audioSource;

    enum State { Alive, Dying, Transcending };
    State state = State.Alive;
    // Use this for initialization
    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update(){
        // todo somewhere stop sound on death
        Thrust();
        Rotate();
    }

    void OnCollisionEnter(Collision collision){
        if (state != State.Alive) {
            return;//ensures that the the collision happens once
        }
        switch (collision.gameObject.tag) {
            case "Friendly":
                // do nothing
                break;
            case "Finish":
                state = State.Transcending;
                Invoke("LoadNextLevel", 1f);//load to next scene after 1 second (paramerise time)
                break;
            default:
                state = State.Dying;
                Invoke("LoadFirstLevel", 1f);//load to first scene after 1 second (paramerise time)
                break;
        }
    }

    private void LoadNextLevel() {
        SceneManager.LoadScene(1);// todo allow for more than 2 levels
    }

    private void LoadFirstLevel()
    {
        SceneManager.LoadScene(0);// load first level
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

 
