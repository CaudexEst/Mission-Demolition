//Created by: Ben Jenkins
//Date created: 2/9/2022
//Last edited: NA
//Last edited by: NA
//Makes camera follow projectile
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCam : MonoBehaviour
{
    static public GameObject POI;

    public float camZ;//the desired z position fo the camera

    private void Awake()
    {
        camZ = this.transform.position.z;

    }
    private void FixedUpdate()
    {
        if (POI == null) return;
        Vector3 destination = POI.transform.position;
        destination.z = camZ;
        transform.position = destination;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
