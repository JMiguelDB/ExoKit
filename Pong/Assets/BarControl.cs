using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO.Ports;
using System.IO;


public class BarControl : MonoBehaviour {
    float speed = 5f;
    SerialPort serialPort;


	// Use this for initialization
	void Start () {
        //Inicializacion de la conexion puerto serie con la nucleo
        serialPort = new SerialPort("/dev/tty.usbmodem1413", 9600);
        serialPort.ReadTimeout = 5;
        serialPort.WriteTimeout = 5;
        serialPort.Open();
	}
	
	// Update is called once per frame
	void Update () {
        //Control del movimiento con las teclas del ordenador
        if(Input.GetKey(KeyCode.LeftArrow)){
            transform.position += Vector3.left * speed * Time.deltaTime;
        }else if(Input.GetKey(KeyCode.RightArrow)){
            transform.position += Vector3.right * speed * Time.deltaTime;
        }
        if(serialPort.IsOpen){
            try{
                //Control del movimiento a partir del servo del codo
                //int value = serialPort.ReadByte();
                string[] data = serialPort.ReadLine().Split(' ');
                int value = int.Parse(data[0]);
                float positionUnity = (value - 0) * (4 - -4) / (105 - 0) + -4;
                print(value + ", " + positionUnity);
                transform.position = new Vector3(positionUnity, transform.position.y, transform.position.z);
                serialPort.DiscardInBuffer();
            }catch (System.Exception e) { /*print(e);*/ }
                //Envia al microcontrolador la posicion en la que deberia colocarse el servo
                try{
                    float positionDifference = transform.position.x - GameObject.Find("Ball").transform.position.x;
                    print("Posicion: "+positionDifference);
                    if(positionDifference < -2.5){
                        serialPort.WriteLine("R");
                    }else if(positionDifference > 2.5){
                        serialPort.WriteLine("L");
                    }else{
                        serialPort.WriteLine("O");
                    }
                    serialPort.DiscardOutBuffer();
                }catch(System.Exception e){}
        }
		
	}
}
