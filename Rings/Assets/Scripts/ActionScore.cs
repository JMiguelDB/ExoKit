using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class ActionScore : MonoBehaviour {

    //Variable compartida asociada al numero de anillos destruidos
    public int numRings;
    //Variable asociada al texto UI
    Text scoreTextUI;


	// Use this for initialization
	void Start () {
        numRings = 0;
        scoreTextUI = GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update () {
        scoreTextUI.text = "Rings: " + numRings + "/10";
        //Si se destruyen todos los anillos, se termina la partida
        if(numRings == 10){
            Application.Quit();
        }
	}
    public void AddRings()
    {
        numRings++;
    }
}
