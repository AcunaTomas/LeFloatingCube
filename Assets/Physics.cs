using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class Physics : MonoBehaviour
{
    public TextMeshProUGUI texto1;
    public TextMeshProUGUI texto2;
    private ConstantForce force;

    private float fan;

    private float objectiveY = 3.35f;

    void Start()
    {
        force = GetComponent<ConstantForce>();
    }

    void Update() //Donde la magia es llamada
    {
        force.relativeForce = new Vector3(0,fuzzy() + Random.Range(-3, 0) ,0); //Aplica movimiento junto con valor de caos, los efectos pueden llegar a ser muy caoticos gracias al sistema de fisicas de Unity.
        texto1.text = "YPos: " + transform.position.y.ToString();
        texto2.text = "Fuerza aplicada: "+ force.relativeForce.y.ToString();
    }

    float trapezoide(float value,float cotaInfIzq,float cotaSupIzq,float cotaSupDer,float cotaInfDer) //Trapezoide
    {
        if (value <= cotaInfIzq)
        {
            return 0;
        }
        else if (value > cotaInfIzq && value <= cotaSupIzq)
        {
            return ((value /(cotaSupIzq-cotaInfIzq)));
        }
        else if (value > cotaSupIzq && value <= cotaSupDer)
        {
            return 1;
        }
        else if (value > cotaSupDer && value <= cotaInfDer)
        {
            return ((value /(cotaSupDer-cotaInfDer)));
        }
        return 0;
    }

    float grade(float x, float y, float z) //Funcion de grado
    {
        if (x <= y) {
            return 0;
        } 
        else if(x > y && x < z){
                return (x/(z-y))-(y/(z-y));
        }
        else{
            return 1;
            }
        }

    float reversegrade(float x, float y, float z) //funcion de grado, reverse edition
    {
        if (x <= y) {
            return 1;
        } 
        else if(x > y && x < z){
                return (x/(z-y))-(z/(z-y));
        }
        else{
            return 0;
            }
        }


    private float fuzzy() //donde la magia sucede
    {
        float distanceFromObj = transform.position.y - objectiveY;  //sacamos distancia del objeto
        
        float center = trapezoide(distanceFromObj, -4, 0,0, 4); //Protip: Si las 2 cotas superiores son iguales, efectivamente estas realizando una funcion triangular

        float close1 = trapezoide(distanceFromObj,1,2,3,4);
        float normal1 = trapezoide(distanceFromObj,3,4,5,6);
        float far1 =  grade(distanceFromObj,5,7);

        float close2 = trapezoide(distanceFromObj,-4,-3,-2,-1);
        float normal2 = trapezoide(distanceFromObj,-6,-5,-4,-3);
        float far2 =  reversegrade(distanceFromObj,-7,-5);

        float sum = center*9.8f + close1*4 + normal1*2 + far1 + close2*15.5f + normal2 * 18f + far2 * 24;
        
        Debug.Log((close1,normal1,far1,close2,normal2,far2));
        
        Debug.Log((close1+normal1+far1+close2+normal2+far2));

        return (sum/(close1+normal1+far1+close2+normal2+far2));
        
    }

}
