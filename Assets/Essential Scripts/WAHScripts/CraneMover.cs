/*-----------------------------------------------------

PEILABS LLC.
info@peilabs.com

Creation Date: 01.01.2023
Author: Halil Doğan
Description: This script handles the crane control with a lever. Which is an easter egg feature.

-----------------------------------------------------*/

using UnityEngine;
using Autohand;

namespace Autohand.Demo{
    public class CraneMover : PhysicsGadgetHingeAngleReader{
        public Transform move;
        public Vector3 axis;
        public float speed = 1;

        public EasterEggScript EasterEggScript;
    
        void Update(){
            if(Mathf.Abs(GetValue()) > 0.1f && EasterEggScript.isKeycardPickedUp == true){
                //move.position = Vector3.MoveTowards(move.position, move.position-axis, Time.deltaTime*speed*(GetValue()));

                Debug.Log("X Pos: " + move.transform.position.x);

                if(move.transform.position.x > -6 && move.transform.position.x < 20){
                    move.position = Vector3.MoveTowards(move.position, move.position-axis, 5*Time.deltaTime*speed*(GetValue()));
                }else if(move.transform.position.x < -6){
                    move.position = Vector3.MoveTowards(move.position,new Vector3(-5.9f , move.position.y , move.position.z), Time.deltaTime*speed);
                }else if(move.transform.position.x > 20){
                    move.position = Vector3.MoveTowards(move.position,new Vector3(19.9f , move.position.y , move.position.z), Time.deltaTime*speed);
                }
            }
        }
    }
}
