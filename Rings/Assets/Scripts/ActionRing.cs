using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionRing : MonoBehaviour {

	// Use this for initialization
	void Start () {
        //Generamos velocidades aleatorias para que cada inicio sea diferente.
        float posx = Random.Range(-20, 16);
        float posy = Random.Range(3.5f, 30);
        float posz = Random.Range(7, 60);
        GetComponent<Rigidbody>().position = new Vector3(posx,posy,posz);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
