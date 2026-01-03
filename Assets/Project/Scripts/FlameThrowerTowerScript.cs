using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlameThrowerTowerScript : TowerScript // Inherits original tower behaviour
{
    public FlameThrowerTowerStats flameThrowerTowerStats;

    public float burstInterval;
    public float burstDuration;
    public float coneAngle;
    public float flameDamagePerTick;
    public float flameTickLength;

    bool flameBurstActive;
    float flameTimer;

    protected override void Start()
    {
        base.Start();
        burstInterval = flameThrowerTowerStats.burstInterval;
        burstDuration = flameThrowerTowerStats.burstDuration;
        coneAngle = flameThrowerTowerStats.coneAngle;
        flameDamagePerTick = flameThrowerTowerStats.flameDamagePerTick;
        flameTickLength = flameThrowerTowerStats.flameTickLength;

        flameBurstActive = false;
        flameTimer = 0f;
    }

    protected override void Update(){
        base.Update();
        flameTimer += Time.deltaTime;
    }

    protected override void AttackTiming(){
        if (flameBurstActive == false && timeForNextAttack <= 0f){ // waiting for next attack
            flameBurstActive = true;
            timeForNextAttack = burstDuration;
        }
        else if (flameBurstActive == true &&  timeForNextAttack > 0f){
            AttackEnemies();
            LookAtEnemies();     
        }else if (flameBurstActive == true && timeForNextAttack <= 0f){
            flameBurstActive = false;
            timeForNextAttack = burstInterval;
        }
    }

    protected override void AttackEnemies(){

        if (currentEnemy != null && activeTower==true) //Only attack if tower is active
        {
            if (flameTimer>= flameTickLength){
                currentEnemy.TakeDamage(towerDamage);
                flameTimer = 0f;
            }
        }
    }
}
