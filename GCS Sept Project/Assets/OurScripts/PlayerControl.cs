using System;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    public AudioClip playerHurtSoundEffect;
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
        GameManager.Instance.SetGameState(GameStates.EndScreen);
        EntityManager.Instance.ClearAllEntities();
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.GetGameState() == GameStates.Gameplay)
        {
            speedX = Input.GetAxisRaw("Horizontal") * movSpeed;
            speedY = Input.GetAxisRaw("Vertical") * movSpeed;
            rb.linearVelocity = new Vector2(speedX, speedY);
        }
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
        AudioManager.Instance.PlaySound(playerHurtSoundEffect);
        playerHealth.Damage(damageAmount);
        // Add SFX to show damage.
    }

    public void HealToMax()
    {
        // TODO: Do we want max health to be something we can increase? Change here if so:
        playerHealth.Heal(baseMaxHealth);
    }


    public void ResetHealth()
    {
        playerHealth.Heal(baseMaxHealth);
    }
}
