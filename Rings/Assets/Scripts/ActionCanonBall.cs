using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionCanonBall : MonoBehaviour {
    public float speed = 10;
    GameObject parent;
    GameObject clone;
    GameObject score;
    bool isThrow = false;
    float timer = 0.0f;

	// Use this for initialization
	void Start () {
        GetComponent<Rigidbody>().isKinematic = true;
        parent = GameObject.Find("Canon");
        score = GameObject.Find("Score");
	}
	
	// Update is called once per frame
	void Update () {
        if(timer > 3){
            GetComponent<Rigidbody>().isKinematic = true;
            transform.parent = parent.transform;
            transform.position = parent.transform.position;
            isThrow = false;
            timer = 0.0f;
        }
        if(isThrow == true){
            timer += Time.deltaTime;
            //print(timer);
        }
        //Comprueba si la mano esta apretada
        bool isReadyThrow = parent.GetComponent<ActionCanon>().readyThrow();
        if (Input.GetKey(KeyCode.Space) || isReadyThrow)
        {
            //the clone variable holds our instantiate action
            //clone = Instantiate(GameObject.Find("CanonBall"), transform.position, transform.rotation); 
            if(isThrow == false){
                GetComponent<Rigidbody>().isKinematic = false;
                transform.parent = null;
                float angle = (parent.transform.eulerAngles.x - 90) * (40 - 0) / (0 - 90) + 0;
                GetComponent<Rigidbody>().velocity = new Vector3(0,angle,  50);
                isThrow = true;
                timer = 0.0f;
            }
        }
	}

    private void OnTriggerExit(Collider other)
    {
        //Toca el agua
        if(other.gameObject.name == "Tile"){
            GetComponent<Rigidbody>().isKinematic = true;
            transform.parent = parent.transform;
            transform.position = parent.transform.position;
            isThrow = false;
            timer = 0.0f;
        }
        //Destruye el anillo que toca
        if (other.gameObject.name == "Ring")
        {
            Destroy(other.gameObject);
            score.GetComponent<ActionScore>().AddRings();        
        }
    }
}
