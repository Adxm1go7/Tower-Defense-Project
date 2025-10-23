using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private float timeToAttack = 2;
    [SerializeField] private float damage = 2;
    private Collider lockedOnEnemy;

    private float time;

    private void OnTriggerEnter(Collider other){
        if (other.tag == "Enemy" && lockedOnEnemy == null){
            time = 0;
            lockedOnEnemy = other;
        }
    }

    private void OnTriggerExit(Collider other){
        if (other == lockedOnEnemy){
            lockedOnEnemy = null;
        }
    }

    private void OnTriggerStay(Collider other){
        if (other.tag == "Enemy"){
            if (other == lockedOnEnemy){
                if (time <= 0){
                    //Attack
                    Debug.Log(lockedOnEnemy);
                    time = timeToAttack;
                }else{
                    time -= Time.deltaTime;
                }
            } else if (lockedOnEnemy == null){
                lockedOnEnemy = other;
            }
        }
    }
}
