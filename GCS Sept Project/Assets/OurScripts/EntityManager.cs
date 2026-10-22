using UnityEngine;

public class EntityManager : MonoBehaviour
{
    public static EntityManager Instance { get; private set; }

    [SerializeField] private PlayerControl player;


    private void Awake()
    {
        if (Instance == null) {
            Instance = this;
        }
    }

    public PlayerControl GetPlayer() {
        return player;
    }


}
