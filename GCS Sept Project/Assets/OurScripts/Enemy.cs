using UnityEngine;

public class Enemy : MonoBehaviour
{

    [SerializeField] private float enemySpeed = 14f;
    public int hitDamage;

    private void Update()
    {
        float step = enemySpeed * Time.deltaTime;
        transform.position = Vector2.MoveTowards(transform.position, EntityManager.Instance.GetPlayer().GetPlayerPosition(), step);
    }

}
