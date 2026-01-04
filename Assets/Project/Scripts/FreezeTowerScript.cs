using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreezeTowerScript : TowerScript // Inherits original tower behaviour
{
    public FreezeTowerStats FreezeTowerStats;

    public float coneAngle;
    public float slowDownMult;
    public float slowDuration;

    protected override void Start()
    {
        base.Start();
        coneAngle = FreezeTowerStats.coneAngle;
        slowDownMult = FreezeTowerStats.slowDownMult;
        slowDuration = FreezeTowerStats.slowDuration;
    }

    protected override void Update(){
        base.Update();
    }

    protected override void AttackEnemies(){
        if (currentEnemy != null && activeTower==true) //Only attack if tower is active
        {
 
            foreach (Collider enemyInRange in enemiesInRange){
                Vector3 directionToEnemy = (enemyInRange.transform.position - transform.position).normalized;
                float angleToEnemy = Vector3.Angle(towerDirection, directionToEnemy);
                
                if (angleToEnemy <= coneAngle / 2f){
                    enemyInRange.GetComponentInParent<Enemy>().TakeDamage(towerDamage);
                    enemyInRange.GetComponentInParent<Enemy>().ApplySlowDown(slowDownMult, slowDuration);
                }
                    
            }
        }
    }
}
