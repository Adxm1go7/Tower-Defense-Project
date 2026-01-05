using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlameThrowerTowerScript : TowerScript // Inherits original tower behaviour
{
    public FlameThrowerTowerStats flameThrowerTowerStats;

    public float burstInterval;
    public float burstDuration;
    public float coneAngle;
    public float flameTickLength;

    bool flameBurstActive;
    float flameTimer;

    public int flameBurnDamagePerTick;
    public float flameBurnTickInterval;
    public float flameBurnTickDuration;

    protected override void Start()
    {
        base.Start();
        burstInterval = flameThrowerTowerStats.burstInterval;
        burstDuration = flameThrowerTowerStats.burstDuration;
        coneAngle = flameThrowerTowerStats.coneAngle;
        flameTickLength = flameThrowerTowerStats.flameTickLength;

        flameBurstActive = false;
        flameTimer = 0f;

        flameBurnTickDuration = flameThrowerTowerStats.flameBurnTickDuration;
        flameBurnTickInterval = flameThrowerTowerStats.flameBurnTickInterval;
        flameBurnDamagePerTick = flameThrowerTowerStats.flameBurnDamagePerTick;
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
            if (flameTimer >= flameTickLength){
                foreach (Collider enemyInRange in enemiesInRange){
                    Vector3 directionToEnemy = (enemyInRange.transform.position - transform.position).normalized;
                    float angleToEnemy = Vector3.Angle(towerDirection, directionToEnemy);
                    
                    if (angleToEnemy <= coneAngle / 2f){

                        enemyInRange.GetComponentInParent<Enemy>().TakeDamage(towerDamage);
                        enemyInRange.GetComponentInParent<Enemy>().ApplyBurn(flameBurnDamagePerTick, flameBurnTickInterval, flameBurnTickDuration);
                    }
                        
                }
                flameTimer = 0f;
            }
        }
    }
}
