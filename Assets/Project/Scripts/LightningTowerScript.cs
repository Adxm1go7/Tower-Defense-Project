using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningTowerScript : TowerScript
{
    public LightningTowerStats lightningTowerStats;

    public float chainRadius;
    public int maxChains;
    public int damageFallOff;
    public float chainInterval;

    private Coroutine lightningCoroutine;

    public Animator animator;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        chainRadius = lightningTowerStats.chainRadius;
        maxChains = lightningTowerStats.maxChains;
        damageFallOff = lightningTowerStats.damageFallOff;
        chainInterval = lightningTowerStats.chainInterval;
    }

    protected override IEnumerator AttackSequence()
    {
        if (currentEnemy != null && activeTower==true) //Only attack if tower is active
        {
            FireLightning(currentEnemy);

        }
        yield break;
    }


    void FireLightning (Enemy initialTarget){
        animator.SetTrigger("Attack");
        //animator.Play("Attack");
        Debug.Log("Attack Animation Triggered");
        HashSet<Enemy> hitEnemies = new HashSet<Enemy>();
        if (lightningCoroutine == null){
            lightningCoroutine = StartCoroutine(ChainLightning(initialTarget, towerDamage, maxChains, hitEnemies));
        }
    }


    private IEnumerator ChainLightning(Enemy currentTarget, int currentDamage,
    int remainingChains, HashSet<Enemy> hitEnemies){

        if (currentTarget == null || remainingChains <= 0 || currentDamage <= 0){
            lightningCoroutine = null;
            yield break;
        }

        hitEnemies.Add(currentTarget);

        currentTarget.TakeDamage(currentDamage);

        // Find next target

        Collider[] nearbyEnemies = Physics.OverlapSphere(
            currentTarget.transform.position,
            chainRadius,
            enemyLayer
        );

        Enemy nextTarget = null;
        float closestDistance = float.MaxValue;

        foreach (Collider col in nearbyEnemies){
            Enemy enemy = col.GetComponentInParent<Enemy>();

            if (hitEnemies.Contains(enemy)) // prevents same enemy being struck again
                continue;

            float distance = Vector3.Distance(
                currentTarget.transform.position,
                enemy.transform.position
            );

            if (distance < closestDistance)
            {
                closestDistance = distance;
                nextTarget = enemy;
            }
        }

        if (nextTarget != null)
        {
            yield return new WaitForSeconds(chainInterval);
            yield return StartCoroutine(ChainLightning(nextTarget, currentDamage - damageFallOff,
                remainingChains - 1, hitEnemies));
        }
        lightningCoroutine = null;
    }
}
