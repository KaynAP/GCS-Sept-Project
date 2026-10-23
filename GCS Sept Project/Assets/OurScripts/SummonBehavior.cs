using System;
using UnityEngine;

public class SummonBehavior : MonoBehaviour
{

    public int summonSpeed;
    public int summonDamage;
    public int summonHealth;
    private Enemy enemyTarget = null;
    private HealthSystem summonHealthSystem;


    void Start()
    {
        summonHealthSystem = new HealthSystem(summonHealth);
        summonHealthSystem.OnDeath += SummonDeathEvent;
    }



    void Update()
    {
        if (enemyTarget == null) // Need to find a first / new enemy target.
        {
            Enemy closestEnemy = FindAnEnemy();
            if (closestEnemy != null)
            {
                enemyTarget = closestEnemy;
            }
        } else // Go towards that target! 
        {
            float step = summonSpeed * Time.deltaTime;
            transform.position = Vector2.MoveTowards(transform.position, enemyTarget.transform.position, step);
        }
    }


    private Enemy FindAnEnemy()
    {
        float currentLowestDistance = 1000f;
        Enemy closestEnemy = null;
        foreach (Enemy enemy in EntityManager.Instance.GetEnemiesList())
        {
            float thisEnemyDistance = Vector2.Distance(enemy.transform.position, transform.position);
            if (currentLowestDistance > thisEnemyDistance)
            {
                currentLowestDistance = thisEnemyDistance;
                closestEnemy = enemy;
            }
        }

        return closestEnemy;
    }

    public void OnTriggerEnter2D(UnityEngine.Collider2D collision)
    {
        var enemyHitting = collision.GetComponent<Enemy>();
        if (enemyHitting != null)
        {
            Debug.Log("Summoning Hit the Enemy!");
            enemyHitting.Damage(summonDamage); // Summon hits the enemy
            Damage(enemyHitting.hitDamage); // Enemy damages the summon back.
        }
    }

    private void SummonDeathEvent(object sender, EventArgs e)
    {
        Debug.Log("Your summon has died!");
        EntityManager.Instance.RemoveFromSummonsList(this.gameObject);
        Destroy(this.gameObject);
        //TODO: Add any polish you want!
    }

    public void Damage(int damageAmount)
    {
        summonHealthSystem.Damage(damageAmount);
    }

}
