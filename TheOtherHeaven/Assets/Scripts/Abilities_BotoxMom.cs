﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Abilities_BotoxMom : MonoBehaviour {

    public float newYearModifier;
    public GameObject pGM;
    public GameMaster GM2;

    public GameObject P1RBF;
    public GameObject P2RBF;
    GameObject RBFproj;

    public GameObject P1CA;
    public GameObject P2CA;
    GameObject assassinationProj;

    public Material colorBlue;
    public Material colorRed;

    // Use this for initialization
    void Start () {
        newYearModifier = 0.25f;

        if (gameObject.tag == "P1")
        {
            RBFproj = P1RBF;
            assassinationProj = P1CA;
            GetComponent<MeshRenderer>().material = colorRed;
        }
        else if (gameObject.tag == "P2")
        {
            RBFproj = P2RBF;
            assassinationProj = P2CA;
            GetComponent<MeshRenderer>().material = colorBlue;
        }
        else
        {
            Debug.Log("ERROR:  Unable to determine tag for botox mom.");
        }
        GM2 = pGM.GetComponent<GameMaster>();
    }
	
	// Update is called once per frame
	void Update () {
	}

    //New Year New Me:  Passive, Botox Mom moves twice as fast as other characters, decreasing by 50% for each turn
    public void NewYearNewMe()
    {
        newYearModifier += 0.25f;
        if (newYearModifier == 0.25f)
        {
            GM2.CL.PostCombatMsg("Apparent Age: 18.  Hip young thing!");
        }
        else if (newYearModifier == 0.5f)
        {
            GM2.CL.PostCombatMsg("Apparent Age: 21.  Looking good!");
        }
        else if (newYearModifier == 0.75f)
        {
            GM2.CL.PostCombatMsg("Apparent Age: 35.  Who invited Granny?");
        }
        else if (newYearModifier == 1.00f)
        {
            GM2.CL.PostCombatMsg("Apparent Age: 57.  Ew.");
        }
        else
        {
            GM2.CL.PostCombatMsg("Apparent Age: Ancient -- You need surgery!");
        }
    }

    //Young Again:  Botox mom refreshes her passive (new year new me)
    public void YoungAgain()
    {
        GM2.CB.SpendAP(8);
        if (GM2.CB.enoughAP)
        {
            newYearModifier = 0.25f;
            GM2.CL.PostCombatMsg("After a quick surgery, Claudia feels young again!");
            GM2.CL.PostCombatMsg("Claudia's original movement speed is restored.");
        }
    }

    //RBF:  A long-ranged line nuke in the direction you're facing
    public void RestingBitchFace()
    {
        GM2.CB.SpendAP(2);
        if (GM2.CB.enoughAP)
        {
            RaycastHit hit;
            Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 100);
            Vector3 sp = Camera.main.WorldToScreenPoint(transform.position);
            Vector3 dir = (Input.mousePosition - sp).normalized;
            Instantiate(RBFproj, this.transform.position + (this.transform.forward * 15), gameObject.transform.rotation);
            GM2.CL.PostCombatMsg("Claudia casts Resting Bitch Face.");
        }
    }
    //Character Assassination:  Botox mom targets an enemy anywhere on the map and deals damage to them
    public void CharacterAssassination()
    {
        GM2.CB.SpendAP(9);
        if (GM2.CB.enoughAP)
        {
            RaycastHit hit;
            Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 100);
            Instantiate(assassinationProj, hit.point, Quaternion.identity);
            GM2.CL.PostCombatMsg("Claudia spreads rumors about her opponents!");
        }
    }

    public void Tick()
    {
        NewYearNewMe();
    }
}
