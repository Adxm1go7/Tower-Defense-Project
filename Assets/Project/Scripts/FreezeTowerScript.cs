using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreezeTowerScript : TowerScript // Inherits original tower behaviour
{
    public FreezeTowerStats FreezeTowerStats;

    [SerializeField] private ParticleSystem freezeParticles; 
    private Animator anim;

    public float coneAngle;
    public float slowDownMult;
    public float slowDuration;

    protected override void Start()
    {
        base.Start();
        anim = GetComponentInChildren<Animator>();
        coneAngle = FreezeTowerStats.coneAngle;
        slowDownMult = FreezeTowerStats.slowDownMult;
        slowDuration = FreezeTowerStats.slowDuration;
    }

    protected override void Update(){
        base.Update();
    }

    protected override IEnumerator AttackSequence(){
        if (currentEnemy != null && activeTower==true) //Only attack if tower is active
        {
            if (anim != null) anim.SetTrigger("Attack");
            if (freezeParticles != null) freezeParticles.Play();
            yield return new WaitForSeconds(0.4f);
            foreach (Collider enemyInRange in enemiesInRange){
                if (enemyInRange == null) continue;
                Vector3 directionToEnemy = (enemyInRange.transform.position - transform.position).normalized;
                float angleToEnemy = Vector3.Angle(transform.forward, directionToEnemy);
                
                if (angleToEnemy <= coneAngle / 2f){
                    enemyInRange.GetComponentInParent<Enemy>().TakeDamage(towerDamage);
                    enemyInRange.GetComponentInParent<Enemy>().ApplySlowDown(slowDownMult, slowDuration);
                }
                    
            }
        }
        yield break;
    }
}
