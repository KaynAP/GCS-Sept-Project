using System;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    public float movSpeed;
    private float speedX, speedY;
    private Rigidbody2D rb;
    private HealthSystem playerHealth;
    public int baseMaxHealth;

    private const string enemyTag = "Enemy"; 

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        playerHealth = new HealthSystem(baseMaxHealth);
        playerHealth.OnDeath += PlayerDeathEvent;
    }

    private void PlayerDeathEvent(object sender, EventArgs e)
    {
        // TODO: Make like a uhhhhh game ending state.
        Debug.Log("Game Over!");
    }

    // Update is called once per frame
    void Update()
    {
        speedX = Input.GetAxisRaw("Horizontal") * movSpeed;
        speedY = Input.GetAxisRaw("Vertical") * movSpeed;
        rb.linearVelocity = new Vector2(speedX, speedY);
    }

    public Vector2 GetPlayerPosition() {
        return transform.position;
    }

    public void OnTriggerEnter2D(UnityEngine.Collider2D collision)
    {
        var enemyHitting = collision.GetComponent<Enemy>();
        if (enemyHitting != null)
        {
            Debug.Log("Enemy hit me !!!!");
            Damage(enemyHitting.hitDamage);
        }
    }

    public void Damage(int damageAmount)
    {
        // Add camera shake.
        playerHealth.Damage(damageAmount);
        // Add SFX to show damage.
    }
}
