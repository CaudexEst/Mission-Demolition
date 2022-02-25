//Created by: Ben Jenkins
//Date created: 2/9/2022
//Last edited: 2/24/2022
//Last edited by: NA
//Description: controls slinghsot and projectile movement

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slingshot : MonoBehaviour
{
    static public Slingshot S;

    [Header("Set in Inspector")]
    public GameObject prefabProjectile;
    public float velocityMultiplier = 10f;

    [Header("Set Dynamically")]
    public GameObject launchpoint;
    
    public Vector3 LaunchPos;
    public GameObject projectile;
    public bool aimingMode;
    public Rigidbody projectileRB;


    private void Awake()
    {
        S = this;
        Transform LaunchPointTrans = transform.Find("LaunchPoint");
        launchpoint = LaunchPointTrans.gameObject;

        launchpoint.SetActive(false);//disables launchpoint
        LaunchPos = LaunchPointTrans.position;

    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!aimingMode) return;//if not aiming, dont check

        //get mouse position from 2D coordinates
        Vector3 mousepos2D = Input.mousePosition;
        mousepos2D.z = -Camera.main.transform.position.z;
        Vector3 mousepos3D = Camera.main.ScreenToWorldPoint(mousepos2D);

        Vector3 mouseDelta = mousepos3D - LaunchPos;

        //limit the mouseDelta to slingshot collider radius
        float maxMagnitude = this.GetComponent<SphereCollider>().radius;

        if (mouseDelta.magnitude > maxMagnitude)
        {
            mouseDelta.Normalize();//sets the vector to the same direction but a length of 1
            mouseDelta *= maxMagnitude;
        }

        //move projectile
        Vector3 projectilePos = LaunchPos + mouseDelta;
        projectile.transform.position = projectilePos;

        if (Input.GetMouseButtonUp(0))
        {
            //check if mouse button has been released
            aimingMode = false;
            projectileRB.isKinematic = false;
            projectileRB.velocity = -mouseDelta * velocityMultiplier;
            FollowCam.POI = projectile;
            projectile = null;
            MissionDemolition.ShotFired();
        }
    }

    private void OnMouseEnter()
    {
        launchpoint.SetActive(true);
        //print("Slingshot: OnMouseEnter");
    }

    private void OnMouseExit()
    {
        launchpoint.SetActive(false);
        //print("Slingshot: OnMouseExit");
    }

    private void OnMouseDown()
    {
        aimingMode = true;
        projectile = Instantiate(prefabProjectile) as GameObject;
        projectile.transform.position = LaunchPos;
        projectileRB = projectile.GetComponent<Rigidbody>();
        projectileRB.isKinematic = true;

    }
}
