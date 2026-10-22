using UnityEngine;

public class Summoning : MonoBehaviour
{
    private Camera mainCam;
    private Vector3 mousePos;
    public GameObject Summon;
    private Transform summonTransform;
    public bool canSummon;
    private float timer;
    public float timeBetweenSummoning;
    void Start()
    {
        mainCam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        mousePos = mainCam.ScreenToWorldPoint(Input.mousePosition);
        var summonPos = new Vector3(mousePos.x, mousePos.y, 1);
        if (Input.GetMouseButtonDown(0))
        {

            if (EntityManager.Instance.CanSummonMore())
            {
                GameObject summon = Instantiate(Summon, summonPos, Quaternion.identity);
                EntityManager.Instance.AddToSpawnList(summon);
            } else
            {
                Debug.Log("Cannot summon more thingies.");
            }
            
        }

        //Vector3 rotation = mousePos - transform.position;

        //float rotZ = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;

        //transform.rotation = Quaternion.Euler(0, 0, rotZ);

        //if (Input.GetMouseButton(0) && canSummon)
        //{
        //    Instantiate(Summon, summonTransform.position, Quaternion.identity);
        
    }
}