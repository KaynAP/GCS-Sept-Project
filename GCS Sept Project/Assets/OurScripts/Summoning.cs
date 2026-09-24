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

        if (Input.GetMouseButtonDown(0))
        {
            Instantiate(Summon, mousePos, Quaternion.identity);
        }

        //Vector3 rotation = mousePos - transform.position;

        //float rotZ = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;

        //transform.rotation = Quaternion.Euler(0, 0, rotZ);

        //if (Input.GetMouseButton(0) && canSummon)
        //{
        //    Instantiate(Summon, summonTransform.position, Quaternion.identity);
        
    }
}