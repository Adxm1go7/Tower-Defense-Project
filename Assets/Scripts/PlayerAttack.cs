using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private float timeToAttack = 2;
    private float time;

    private void OnTriggerEnter(Collider other){
        if (other.tag == "Enemy"){
            time = 0;
        }
    }

    private void OnTriggerStay(Collider other){
        if (other.tag == "Enemy"){
            if (time <= 0){
                //Attack
                Debug.Log("DAMAGE");
                time = timeToAttack;
            }else{
                time -= Time.deltaTime;
            }
        }
    }
}
