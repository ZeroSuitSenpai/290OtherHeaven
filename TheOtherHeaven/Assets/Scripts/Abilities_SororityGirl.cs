﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Abilities_SororityGirl : MonoBehaviour {

    public GameObject pGM;
    public GameMaster GM2;

    public float fashionModifier;
    public Vector3 startingPos;

    //call daddy prefabs
    public GameObject P1CD;
    public GameObject P2CD;
    GameObject callDaddyProj;

    public bool usedChauffer;

    public Material colorBlue;
    public Material colorRed;

    // Use this for initialization
    void Start () {
        fashionModifier = 0.75f;
        startingPos = gameObject.transform.position;

        if (gameObject.tag == "P1")
        {
            callDaddyProj = P1CD;
            GetComponent<MeshRenderer>().material = colorRed;

        }
        else if (gameObject.tag == "P2")
        {
            callDaddyProj = P2CD;
            GetComponent<MeshRenderer>().material = colorBlue;

        }
        else
        {
            Debug.Log("ERROR:  Unable to determine tag for sorority girl.");
        }
        GM2 = pGM.GetComponent<GameMaster>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    //Moves from sorority girl deal increasingly large amounts of damage based on the last time they're used
    public void FashionablyLate()
    {
        fashionModifier += 0.25f;
    }

    //Chauffer:  For this turn, movement costs for sorority girl are halveds
    public void Chauffer()
    {
        GM2.CB.SpendAP(1);
        if (GM2.CB.enoughAP)
        {
            pGM.GetComponent<GameMaster>().CB.moveSpeedModifier = 0.5f;
            usedChauffer = true;
            GM2.CL.PostCombatMsg("Tiffany casts Chauffer.");
            GM2.CL.PostCombatMsg("Her movement speed costs are halved until the end of the turn.");
        }

    }

    //Call Daddy:  A huge single-target, melee-ranged nuke
    public void CallDaddy()
    {
        GM2.CB.SpendAP(3);
        if (GM2.CB.enoughAP)
        {
            Vector3 sp = Camera.main.WorldToScreenPoint(transform.position);
            Vector3 dir = (Input.mousePosition - sp).normalized;
            Instantiate(callDaddyProj, this.transform.position + (1.5f * dir), Quaternion.identity);
            callDaddyProj.GetComponent<Ability_CallDaddy>().SetOwner(this.gameObject);
            GM2.CL.PostCombatMsg("Tiffany casts Call Daddy.");
        }
    }

    //Throwback Thursday:  Sorority girl teleports to her starting position
    public void TBT()
    {
        GM2.CB.SpendAP(6);
        if (GM2.CB.enoughAP)
        {
            gameObject.transform.position = startingPos;
            pGM.GetComponent<GameMaster>().CB.navAgent.SetDestination(this.gameObject.transform.position);
            GM2.CL.PostCombatMsg("Tiffany casts #TBT.");
            GM2.CL.PostCombatMsg("Tiffany returns to her starting position!");
        }
    }

    public void Tick()
    {
        FashionablyLate();
        if (usedChauffer)
        {
            usedChauffer = false;
            pGM.GetComponent<GameMaster>().CB.moveSpeedModifier = 1.0f;
        }
    }
}
