using System;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    public float enemySpeed;
    public float enemySpeedVariability;
    public int hitDamage;
    public int enemyHealth;
    private HealthSystem enemyHealthSystem;

    private void Start()
    {
        enemyHealthSystem = new HealthSystem(enemyHealth);
        enemyHealthSystem.OnDeath += ThisEnemyDeath;

        bool faster = UnityEngine.Random.Range(0, 2) == 0;
        float variabilityMultiplier = UnityEngine.Random.Range(0, 4);
        enemySpeed = faster ? enemySpeed + (enemySpeedVariability * variabilityMultiplier) : enemySpeed - (enemySpeedVariability * variabilityMultiplier);
    }

    private void Update()
    {
        float step = enemySpeed * Time.deltaTime;
        transform.position = Vector2.MoveTowards(transform.position, EntityManager.Instance.GetPlayer().GetPlayerPosition(), step);
    }

    public void Damage(int damageAmount)
    {
        enemyHealthSystem.Damage(damageAmount);
    }

    private void ThisEnemyDeath(object sender, EventArgs e)
    {
        EntityManager.Instance.KillEnemy(this);
    }

}
