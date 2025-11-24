using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack2 : MonoBehaviour
{
    [SerializeField] private float areaAttackRadius = 3f;
    [SerializeField] private int areaDamage = 2;
    [SerializeField] private float areaTimeToAttack = 1f;

    [SerializeField] private float singleAttackRadius = 6f;
    [SerializeField] private int singleDamage = 10;
    [SerializeField] private float singleTimeToAttack = 2f;
    public LayerMask enemyLayer;
    private float time;

    private int attackType; // 1 = area, 2 = single
    // Start is called before the first frame update
    void Start()
    {
        time = 0;
        attackType = 2;
    }

    // Update is called once per frame
    void Update()
    {
        if (time <= 0)
        {
            if (attackType == 1)
            {
                AreaAttack();
                time = areaTimeToAttack;
            }
            else if (attackType == 2)
            {
                SingleAttack();
                time = singleTimeToAttack;
            }
        }
            else
            {
                time -= Time.deltaTime;
            }
        
    }

    public void AreaAttack() // Fireboots attacks area around hero, multi target
    {
        Collider[] enemiesInRange = Physics.OverlapSphere(transform.position, areaAttackRadius, enemyLayer);

        foreach (Collider enemyInRange in enemiesInRange)
        {
            Enemy enemy = enemyInRange.GetComponentInParent<Enemy>();
            if (enemy != null)
            {
                enemy.TakeDamage(areaDamage);
            }
        }
    }

    public void SingleAttack() // Attacks single target that is furthest along the track
    {
        Collider[] enemiesInRange = Physics.OverlapSphere(transform.position, singleAttackRadius, enemyLayer);
        Enemy furthestEnemy = null;
        float bestProgress = 0f;
        foreach (Collider enemyInRange in enemiesInRange)
        {
            Enemy enemy = enemyInRange.GetComponentInParent<Enemy>();
            if (enemy != null)
            {
                if (enemy.GetComponentInParent<EnemyMovement>().getEnemyMovementProgress() > bestProgress)
                {
                    furthestEnemy = enemy;
                    bestProgress = enemy.GetComponentInParent<EnemyMovement>().getEnemyMovementProgress();
                }
            }
        }

        if (furthestEnemy != null)
        {
            furthestEnemy.TakeDamage(singleDamage);
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, singleAttackRadius);
    }
}
