using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallControl : MonoBehaviour {

	// Use this for initialization
	void Start () {
        //Generamos velocidades aleatorias para que cada inicio sea diferente.
        float speedx = Random.Range(5, 7);
        float speedy = Random.Range(2, 5);
        //Asignamos esas velocidades al elemento que conforma a la esfera.
        GetComponent<Rigidbody>().velocity = new Vector3(speedx, speedy, 0);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
