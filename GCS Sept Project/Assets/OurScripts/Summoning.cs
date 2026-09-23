using UnityEngine;

public class Summoning : MonoBehaviour
{
    private Camera mainCam;
    private Vector3 mousePos;
    public GameObject summon;
    public Transform summonTransform;
    public bool canFire;
    private float timer;
    public float timeBetweenFiring;
    void Start()
    {
        mainCam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        mousePos = mainCam.ScreenToWorldPoint(Input.mousePosition);

        Vector3 rotation = mousePos - transform.position;

        float rotZ = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;

        transform.rotation = Quaternion.Euler(0, 0, rotZ);

        if (Input.GetMouseButton(0) && canFire)
        {
            Instantiate(summon, summonTransform.position, Quaternion.identity);
        }
    }
}