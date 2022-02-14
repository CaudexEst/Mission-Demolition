//Created by: Ben Jenkins
//Date created: 2/14/2022
//Last edited: NA
//Last edited by: NA
//Description: Creates and controls lots of clouds
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudCrafter : MonoBehaviour
{
    [Header("Set in Inspector")]
    public int numberOfClouds = 40;
    public GameObject cloudPrefab;
    public Vector3 cloudPositionMin = new Vector3(-50, -5, 10);
    public Vector3 cloudPositionMax = new Vector3(150, 100, 10);
    public float cloudScaleMin = 1;
    public float cloudScaleMax = 3;
    public float cloudSpeedMultiplier = 0.5f;

    private GameObject[] cloudInstances;

    private void Awake()
    {
        cloudInstances = new GameObject[numberOfClouds];
        GameObject anchor = GameObject.Find("CloudAnchor");

        GameObject cloud;
        for(int i = 0; i < numberOfClouds; i++)
        {
            cloud = Instantiate<GameObject>(cloudPrefab);

            //position cloud
            Vector3 cpos = Vector3.zero;
            cpos.x = Random.Range(cloudPositionMin.x, cloudPositionMax.x);
            cpos.y = Random.Range(cloudPositionMin.y, cloudPositionMax.y);

            //scale clouds
            float scaleU = Random.value;
            float scaleVal = Mathf.Lerp(cloudScaleMin, cloudScaleMax,scaleU);
            cpos.y = Mathf.Lerp(cloudPositionMin.y, cpos.y, scaleU); //the closer to the ground, the smaller the cloud

        }
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
