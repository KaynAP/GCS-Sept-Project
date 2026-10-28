using UnityEngine;

public class Coin : MonoBehaviour
{
    
    public int coinIncrement;
    public AudioClip coinPickUpSound;
    public float coinPickUpVolume;

    public void OnTriggerEnter2D(UnityEngine.Collider2D collision)
    {
        var PlayerHitting = collision.gameObject.GetComponent<PlayerControl>();
        if (PlayerHitting != null)
        {
            AudioManager.Instance.PlaySound(coinPickUpSound, coinPickUpVolume);
            GameManager.Instance.IncreaseCoins(coinIncrement);
            EntityManager.Instance.RemoveCoinFromList(this.gameObject);
            Destroy(this.gameObject);
        }
    }



}
