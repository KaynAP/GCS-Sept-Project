using UnityEngine;

public class SummonBehavior : MonoBehaviour
{

    public int summonSpeed;
    public int summonDamage;
    private Enemy enemyTarget = null;



    void Start()
    {
        
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


    public void Dies()
    {
        Destroy(this.gameObject);
    }

    public void OnTriggerEnter2D(UnityEngine.Collider2D collision)
    {
        var enemyHitting = collision.GetComponent<Enemy>();
        if (enemyHitting != null)
        {
            Debug.Log("Summoning Hit the Enemy!");
            enemyHitting.Damage(summonDamage);
        }
    }

}
