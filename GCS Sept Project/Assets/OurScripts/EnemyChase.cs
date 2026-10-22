using UnityEngine;

public class EnemyChase : MonoBehaviour
{

    [SerializeField] private float enemySpeed = 14f;

    private void Update()
    {
        float step = enemySpeed * Time.deltaTime;
        transform.position = Vector2.MoveTowards(transform.position, EntityManager.Instance.GetPlayer().GetPlayerPosition(), step);
    }


}
