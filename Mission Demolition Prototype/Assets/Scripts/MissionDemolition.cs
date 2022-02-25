//Created by: Ben Jenkins
//Date created: 2/24/2022
//Last edited: NA
//Last edited by: NA
//Description: controls slinghsot and projectile movement
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public enum GameMode
{
    idle,playing,levelEnd
}

public class MissionDemolition : MonoBehaviour
{
    static public MissionDemolition S; //singleton

    public GameObject[] castles;
    public Text gtLevel;
    public Text gtScore;
    public Vector3 castlePos;

    public int level;
    public int levelMax;
    public int shotsTaken;
    public GameObject castle;
    public GameMode mode = GameMode.idle;
    public string showing = "Slingshot";
    
    // Start is called before the first frame update
    void Start()
    {
        S = this;
        level = 0;
        levelMax = castles.Length;
        StartLevel();
    }

    void StartLevel()
    {
        if (castle != null)//get rid of old castle
        {
            Destroy(castle);
        }

        //destroy old projectiles if they exost
        GameObject[] gos = GameObject.FindGameObjectsWithTag("Projectile");
        foreach(GameObject pTemp in gos)
        {
            Destroy(pTemp);
        }

        //instantiate the new castle
        castle = Instantiate(castles[level]) as GameObject;
        castle.transform.position = castlePos;
        shotsTaken = 0;

        //reset the camera
        SwitchView("Both");
        ProjectileLine.S.Clear();

        //reset the goal
        Goal.goalMet = false;

        ShowGt();

        mode = GameMode.playing;
    }

    void ShowGt()
    {
        gtLevel.text = "Level: " + (level + 1) + " of " + levelMax;
        gtScore.text = "Shots Taken: " + shotsTaken;
    }

    // Update is called once per frame
    void Update()
    {
        ShowGt();

        //check for level end
        if (mode == GameMode.playing && Goal.goalMet)
        {
            //change mode to stop checking for level end
            mode = GameMode.levelEnd;
            //zoom out
            SwitchView("Both");
            Invoke("NextLevel", 2f);

        }
    }

    void NextLevel()
    {
        level++;
        if (level == levelMax)
        {
            level = 0;
        }
        StartLevel();
    }

    void OnGUI()
    {
        Rect buttonRect = new Rect((Screen.width / 2) - 50, 10, 100, 24);
        switch (showing)
        {
            case "Slingshot":
                if(GUI.Button(buttonRect,"Show Castle"))
                {
                    SwitchView("Castle");
                } break;
            case "Castle":
                if (GUI.Button(buttonRect, "Show Both"))
                {
                    SwitchView("Both");
                }
                break;
            case "Both":
                if (GUI.Button(buttonRect, "Show Slingshot"))
                {
                    SwitchView("Slingshot");
                }
                break;
        }
    }
    static public void SwitchView(string eView)
    {
        S.showing = eView;
        switch (S.showing)
        {
            case "Slingshot":
                FollowCam.POI = null;
                break;
            case "Castle":
                FollowCam.POI = S.castle;
                break;
            case "Both":
                FollowCam.POI = GameObject.Find("ViewBoth");
                break;
        }
    }

    public static void ShotFired()
    {
        S.shotsTaken++;
    }
}
