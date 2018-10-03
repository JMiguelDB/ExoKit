using System.Collections;
using System.Collections.Generic;
using System.IO.Ports;
using UnityEngine;

public class ActionCanon : MonoBehaviour
{
    float minX = -21;
    float maxX = 21;
    float speedMovement = 5f;
    float speedRotation = 15f;
    //False izquierda, true derecha
    bool direccion = true;
    bool isThrow = false;
    SerialPort serialPort;

    // Use this for initialization
    void Start()
    {
        serialPort = new SerialPort("/dev/tty.usbmodem1413", 9600);
        serialPort.Open();
        serialPort.ReadTimeout = 5;
        serialPort.WriteTimeout = 5;
    }

    // Update is called once per frame
    void Update()
    {
        //Movimiento predefinido de izquierda a derecha
        if (transform.position.x > maxX)
        {
            direccion = false;
        }
        else if (transform.position.x < minX)
        {
            direccion = true;
        }
        if (direccion == true)
        {
            transform.position += Vector3.right * speedMovement * Time.deltaTime;
        }
        else
        {
            transform.position += Vector3.left * speedMovement * Time.deltaTime;
        }
        //---- Movimiento de rotacion del canon ---------------------
        //
        if (Input.GetKey(KeyCode.UpArrow) && transform.eulerAngles.x > 20)
        {
            transform.Rotate(Vector3.left * speedRotation * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.DownArrow) && transform.eulerAngles.x < 85)
        {
            transform.Rotate(Vector3.right * speedRotation * Time.deltaTime);
        }
        if(serialPort.IsOpen){
            try
            {
                //int value = int.Parse(serialPort.ReadLine());
                string[] splitSerial = serialPort.ReadLine().Split(' ');
                //print(splitSerial[0] + " " + splitSerial[1]);
                int value = int.Parse(splitSerial[0]);
                if(int.Parse(splitSerial[1]) == 0){
                    isThrow = true;
                }else{
                    isThrow = false;
                }
                //(x - in_min) * (out_max - out_min) / (in_max - in_min) + out_min;
                float positionUnity = (value - 0) * (20 - 85) / (105 - 0) + 85;

                Quaternion target = Quaternion.Euler(positionUnity, 0, 0);
                // Dampen towards the target rotation
                transform.rotation = Quaternion.Slerp(transform.rotation, target, Time.deltaTime * speedMovement);
                serialPort.DiscardInBuffer();
            }
            catch (System.Exception e) { /*print(e);*/ }
        }
    }

    public bool readyThrow()
    {
        return isThrow;
    }
}
