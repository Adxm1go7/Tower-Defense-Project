using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlameThrowerTowerScript : TowerScript // Inherits original tower behaviour
{
    public FlameThrowerTowerStats flameThrowerTowerStats;
    [SerializeField] private ParticleSystem flameParticles;
    private Animator anim;

    public float burstInterval;
    [SerializeField] private float rotationSpeed = 5f;
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
        anim = GetComponentInChildren<Animator>();
        if (flameParticles != null) flameParticles.Stop();
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
        if (!activeTower && flameBurstActive)
        {
            StopFlame();
        }
    }

    void Look()
    {
        if (currentEnemy != null)
        {
            Vector3 direction = currentEnemy.transform.position - transform.position;
            direction.y = 0;
            if (direction != Vector3.zero)
            {
                Quaternion lookRotation = Quaternion.LookRotation(direction);
                transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * rotationSpeed);
            }

        }
    }

    protected override void AttackTiming(){
        if (flameBurstActive == false && timeForNextAttack <= 0f && enemiesInRange.Length > 0)
        { 
            StartFlame();
            timeForNextAttack = burstDuration;
        }
        else if (flameBurstActive == true &&  timeForNextAttack > 0f){
            Attack();
            Look();     
        }else if (flameBurstActive == true && timeForNextAttack <= 0f){
            StopFlame();
            timeForNextAttack = burstInterval;
        }
    }

    void StartFlame()
    {
        flameBurstActive = true;
        if (anim != null) anim.SetBool("IsFiring", true);
        if (flameParticles != null) flameParticles.Play();
    }

    void StopFlame()
    {
        flameBurstActive = false;
        if (anim != null) anim.SetBool("IsFiring", false);
        if (flameParticles != null) flameParticles.Stop();
    }

    protected void Attack(){
        if (activeTower==true)
        {
            if (flameTimer >= flameTickLength){
                foreach (Collider enemyInRange in enemiesInRange){
                    if (enemyInRange == null) continue;
                    Vector3 directionToEnemy = (enemyInRange.transform.position - transform.position).normalized;
                    float angleToEnemy = Vector3.Angle(transform.forward, directionToEnemy);
                    
                    if (angleToEnemy <= coneAngle / 2f){
                        Enemy enemyScript = enemyInRange.GetComponentInParent<Enemy>();
                        if (enemyScript != null)
                        {
                            enemyScript.TakeDamage(towerDamage);
                            enemyScript.ApplyBurn(flameBurnDamagePerTick, flameBurnTickInterval, flameBurnTickDuration);
                        }
                    }  
                }
                flameTimer = 0f;
            }
        }
    }
}
