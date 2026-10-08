using UnityEngine;
using System.Collections.Generic;
public class Summoning : MonoBehaviour
{
    public GameObject Summon;
    public GameObject playerSummon;
    public float timeBetweenSummoning;
    private Camera mainCam;
    private Vector3 mousePos;
    private Transform summonTransform;
    private bool canSummon;
    private float timer;
    
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
            RemoveSummons(playerSummon);
            Instantiate(Summon, summonPos, Quaternion.identity, parent: playerSummon.transform);
        }

        //Vector3 rotation = mousePos - transform.position;

        //float rotZ = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;

        //transform.rotation = Quaternion.Euler(0, 0, rotZ);

        //if (Input.GetMouseButton(0) && canSummon)
        //{
        //    Instantiate(Summon, summonTransform.position, Quaternion.identity);
        
    }

    private static void RemoveSummons(GameObject gameObject)
    {
        foreach (Transform transform in gameObject.transform)
        {
            Debug.Log(transform.gameObject);
            Destroy(transform.gameObject);
            
        }
    }
}