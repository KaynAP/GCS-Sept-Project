using UnityEngine;

public class Coin : MonoBehaviour
{
    
    public int scoreIncreasePerCoin;

    public void OnCollisionEnter2D(UnityEngine.Collision2D collision)
    {
        var PlayerHitting = collision.gameObject.GetComponent<PlayerControl>();
        if (PlayerHitting != null)
        {
            Debug.Log("Picking up coin!");
            GameManager.Instance.IncreaseScore(scoreIncreasePerCoin);
            Destroy(this.gameObject);
        }
    }



}
