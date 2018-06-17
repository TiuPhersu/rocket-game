using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//attribute (can't add 2 scripts)
[DisallowMultipleComponent]
public class Oscillator : MonoBehaviour {
    [SerializeField] Vector3 movementVector = new Vector3(10f,10f,10f);
    [SerializeField] float period = 2f;
    // todo remove from inspector later
    //set the movement factor range to 0 not move to 1 to fully move
    [Range(0,1)][SerializeField]float movementFactor;

    Vector3 startingPos; // must be stored for absolute 

	// Use this for initialization
	void Start () {
        startingPos = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
        // todo protect against divide by zero
        float cycles = Time.time / period;
        const float tau = Mathf.PI * 2;//val is 6.28 or 2pi
        float rawSinWave = Mathf.Sin(cycles * tau);

        movementFactor = rawSinWave / 2f + 0.5f;//cut the sine wave in half and move up by 0.5
        Vector3 offset = movementVector * movementFactor;
        transform.position = startingPos + offset;
	}
}
