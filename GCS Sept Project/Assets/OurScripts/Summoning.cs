using UnityEngine;
using System.Collections.Generic;
public class Summoning : MonoBehaviour
{
    public GameObject Summon;
    public GameObject playerSummon;
    public float timeBetweenSummoning;
    private Camera mainCam;
    private Vector3 mousePos;
    
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
            }
        }
        
    }
}