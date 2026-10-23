using UnityEngine;

public class Coin : MonoBehaviour
{
    
    public int coinIncrement;

    public void OnCollisionEnter2D(UnityEngine.Collision2D collision)
    {
        var PlayerHitting = collision.gameObject.GetComponent<PlayerControl>();
        if (PlayerHitting != null)
        {
            Debug.Log("Picking up coin!");
            GameManager.Instance.IncreaseCoins(coinIncrement);
            Destroy(this.gameObject);
        }
    }



}
