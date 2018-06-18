using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Rocket : MonoBehaviour{
    [SerializeField] float rcsThrust = 100f;
    [SerializeField] float mainThrust = 100f;
    [SerializeField] float levelLoadDelay = 2f;

    [SerializeField] AudioClip mainEngine;
    [SerializeField] AudioClip death;
    [SerializeField] AudioClip success;

    [SerializeField] ParticleSystem mainEngineParticles;
    [SerializeField] ParticleSystem deathParticles;
    [SerializeField] ParticleSystem successParticles;

    Rigidbody rigidBody;
    AudioSource audioSource;

    bool isTransitioning = false;
    bool collisionAreEnabled = true;
    // Use this for initialization
    void Start(){
        rigidBody = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update(){
        if (isTransitioning){
            RespondToThrustInput();
            RespondToRotateInput();
        }
        if (Debug.isDebugBuild){
            RespondToDebugKeys();
        }
    }

    private void RespondToDebugKeys(){
        if (Input.GetKeyDown(KeyCode.L)) {
            LoadNextLevel();
        } else if (Input.GetKeyDown(KeyCode.C)) {
            //toggle 
            collisionAreEnabled = !collisionAreEnabled;
        }
    }

    void OnCollisionEnter(Collision collision){
        if (isTransitioning || !collisionAreEnabled) {
            return;//ensures that the the collision happens once
        }
        switch (collision.gameObject.tag) {
            case "Friendly":
                // do nothing
                break;
            case "Finish":
                StartSuccessSequence();
                break;
            default:
                StartDeathSequence(); break;
        }
    }

    private void StartSuccessSequence(){
        isTransitioning = true;
        audioSource.Stop();
        audioSource.PlayOneShot(success);
        successParticles.Play();
        Invoke("LoadNextLevel", levelLoadDelay);//load to next scene after 1 second (parameterise time)
    }

    private void StartDeathSequence(){
        isTransitioning = true;
        audioSource.Stop();
        audioSource.PlayOneShot(death);
        deathParticles.Play();
        Invoke("LoadFirstLevel", levelLoadDelay);//load to first scene after 1 second (parameterise time)
    }

    private void LoadNextLevel() {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;
        if (nextSceneIndex >= SceneManager.sceneCountInBuildSettings) {
            nextSceneIndex = 0;
        }
        SceneManager.LoadScene(nextSceneIndex);// todo allow for more than 2 levels
        successParticles.Stop();
    }

    private void LoadFirstLevel(){
        SceneManager.LoadScene(0);// load first 
        deathParticles.Stop();

    }

    private void RespondToThrustInput(){
        // thrust while rotating
        if (Input.GetKey(KeyCode.Space)) {
            ApplyThrust();
        }
        else{
            StopApplyingThrust();
        }
    }

    private void StopApplyingThrust(){
        audioSource.Stop();
        mainEngineParticles.Stop();
    }

    private void ApplyThrust(){
        //moves up
        rigidBody.AddRelativeForce(Vector3.up * mainThrust);
        if (!audioSource.isPlaying)
        {//so it doesn't layer
            audioSource.PlayOneShot(mainEngine);
        }
        mainEngineParticles.Play();
    }

    private void RespondToRotateInput(){
        rigidBody.angularVelocity = Vector3.zero; //take manual control of rotation

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

 
