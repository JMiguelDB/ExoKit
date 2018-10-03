#include "mbed.h"
//#include "MyoWare.h"
//#include "HBridgeDCMotor.h"

//Conexion serie para imprimir desde el ordenador
Serial pc(SERIAL_TX, SERIAL_RX);

//----SENSORES---------------
AnalogIn emg_sensor_antebrazo(A0); //Sensor muscular asociado al movimiento del antebrazo
AnalogIn emg_sensor_brazo(A1);
AnalogIn posServo(A2); //Potenciometro integrado en el servo que lee la posicion
//AnalogIn force_sensor(A0); //Sensor que mide la fuerza del movimiento del antebrazo
//---------------------------

//----SERVOMOTORES-----------
//--- Dedo pulgar -----
DigitalOut arriba_pulgar(D5);
DigitalOut abajo_pulgar(D4);
//--- Dedo indice -----
DigitalOut arriba_indice(D2);
DigitalOut abajo_indice(D3);
//--- Dedo medio -----
DigitalOut arriba_medio(D11);
DigitalOut abajo_medio(D10);
//--- Dedo anular -----
DigitalOut arriba_anular(D8);
DigitalOut abajo_anular(D9);
//--- Dedo menique -----
DigitalOut arriba_menique(D6);
DigitalOut abajo_menique(D7);
//--- Codo ------------
PwmOut servo_codo(D12); //Servo encargado del movimiento del codo
//---------------------------

//const int forceOffset = 439;
//float logic = 0.7f; 
//int steps = 20;
//MyoWare ms(A2, logic, steps);

void mueve_mano(int movimiento);

int main()
{
    /*
    if(arriba_pulgar.is_connected()) {
        printf("D6 is initialized and connected!\n\r");
    }
    */
    int posDegree;
    //float valor;
    float valor_emg_antebrazo;
    wait(1);
    float valor_emg_inicial = emg_sensor_antebrazo.read();
    // Set PWM servo
    servo_codo.period_ms(20); // servo requires a 20ms period
    //---- Configuracion movimiento en brazo IZQUIERDO ---
    // 0.00182 -> maxima extension del brazo
    // 0.0001 -> maxima flexion del brazo
    //----------------------------------------------------
    //---- Valor analogico potenciometro posicion servo --
    // 0.545 -> Maxima extension del brazo
    // 0.271 -> Flexion del brazo 90 grados
    /*
    for(float i = 0.0000;i < 0.00185;i = i + 0.0001){
        servo_codo.pulsewidth(i); 
        wait(0.25);
    }
    */
    while(1){
        char unityCommand;
        if(pc.writeable()){
            //-- Codigo posicion servo potenciometro --
            posDegree = (90.0f/(0.271f-0.545f))*(posServo.read()-0.545f);
            if(posDegree < 0){
                posDegree = 0;   
            }
            //pc.printf("%d\n",posDegree); 
            //-- Codigo movimiento emg antebrazo ---
            valor_emg_antebrazo = emg_sensor_antebrazo.read();
            //pc.printf("Valor emg: %f \n",valor_emg_antebrazo);
            //pc.printf("Valor emg inicial: %f \n",valor_emg_inicial);
            if(valor_emg_antebrazo > valor_emg_inicial*1.5f){
                mueve_mano(0);
                pc.printf("%d %d\n",posDegree,0); 
            }else{
                mueve_mano(1);
                pc.printf("%d %d\n",posDegree,1); 
            }
        }
        wait(0.05);
        if(pc.readable()){
            unityCommand = pc.getc();
            if(unityCommand == 'L'){
                servo_codo.pulsewidth(0.00182);
            }else if(unityCommand == 'R'){ 
                servo_codo.pulsewidth(0.0001);
            }
        }
        wait(0.05);
        //while (pc.readable()) { unityCommand = pc.getc(); } 
        //pc.printf("pos: %f \n",pos);
        //pc.printf("Degree: %f \n",posDegree);
        //pc.printf("---\n");

        //---------------------------------------
        //-- Codigo sensor de fuerza -------
        /*
        servo.pulsewidth(0.0009); 
        valor = force_sensor.read();
        float calibracion = (force_sensor.read()*100.0f)-forceOffset;
        float voltage =(force_sensor.read()*(5.0/ 4096.0)); 
        pc.printf("Valor: %f \n", valor);
        pc.printf("Calibracion: %f \n", calibracion);
        pc.printf("Voltaje: %f \n", voltage);
        pc.printf("---\n");
        wait(0.5);
        */
        //---------------------------------------
        /*
        float sig_val = ms.read();
        bool onOff = ms.control();
        int stepNum = ms.magnitude();
        
        pc.printf("Sig Value: %f\n\r", sig_val);  
        pc.printf("Logic Value: %f\n\r", logic);  
        pc.printf("Digital Value: %d\n\r", onOff); 
        pc.printf("Total Steps: %d\n\r", steps);
        pc.printf("Step Value Value: %d\n\r", stepNum); 
        pc.printf("\n\r"); 
        */
        //---------------------------------------
    }
}

void mueve_mano(int movimiento){
    if(movimiento == 0){
        arriba_pulgar = 0;
        abajo_pulgar = 1;
        arriba_indice = 0;
        abajo_indice = 1;
        arriba_medio = 0;
        abajo_medio = 1;
        arriba_anular = 0;
        abajo_anular = 1;
        arriba_menique = 0;
        abajo_menique = 1;
    }else if(movimiento == 1){
        arriba_pulgar = 1;
        abajo_pulgar = 0;
        arriba_indice = 1;
        abajo_indice = 0;
        arriba_medio = 1;
        abajo_medio = 0;
        arriba_anular = 1;
        abajo_anular = 0;
        arriba_menique = 1;
        abajo_menique = 0;
    } 
}
