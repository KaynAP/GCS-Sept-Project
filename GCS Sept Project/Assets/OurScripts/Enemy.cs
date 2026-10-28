using System;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    [SerializeField] private float enemySpeed = 14f;
    public int hitDamage;
    public int enemyHealth;
    private HealthSystem enemyHealthSystem;

    private void Start()
    {
        enemyHealthSystem = new HealthSystem(enemyHealth);
        enemyHealthSystem.OnDeath += ThisEnemyDeath;
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
