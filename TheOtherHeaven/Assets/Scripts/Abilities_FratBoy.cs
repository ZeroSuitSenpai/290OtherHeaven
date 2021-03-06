﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Abilities_FratBoy : MonoBehaviour {

    public bool hasAffluenza;
    public GameObject P1TT;
    public GameObject P2TT;

    public GameObject DrunkHitbox;

    public GameObject thirstyThursdayProj;

    public Material colorBlue;
    public Material colorRed;

    public GameObject pGM;
    public GameMaster GM2;

    // Use this for initialization
    void Start ()
    {
        hasAffluenza = false;
        if (gameObject.tag == "P1")
        {
            thirstyThursdayProj = P1TT;
            GetComponent<MeshRenderer>().material = colorRed;

        }
        else if (gameObject.tag == "P2")
        {
            thirstyThursdayProj = P2TT;
            GetComponent<MeshRenderer>().material = colorBlue;
        }
        else
        {
            Debug.Log("ERROR:  Unable to determine tag for frat boy.");
        }
        DrunkHitbox = gameObject.transform.FindChild("DrunkHitbox").gameObject;
        DrunkHitbox.SetActive(false);

        GM2 = pGM.GetComponent<GameMaster>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    //Drunk AF DGAF:  Movement doubles as an attack
    public void DrunkAFDGAF()
    {
        //Implemented in a separate script:  Ability_DrunkAFDGAF.cs
    }

    //Affluenza Defense:  Frat boy becomes immune to all damage for a turn, but cannot do anything.
    public void AffluenzaDefense()
    {
        GM2.CB.SpendAP(5);
        if (GM2.CB.enoughAP)
        {
            hasAffluenza = true;
            GM2.CL.PostCombatMsg("Chad calls his lawyers, gaining Affluenza Defense for 1 turn!");
            GM2.CL.PostCombatMsg("Chad is immune from damage until the start of his next turn.");
        }
    }
    //Thirsty Thursday:  Frat boy slams a keg on the ground, damaging and silencing all around him
    public void ThirstyThursday()
    {
        GM2.CB.SpendAP(4);
        if (GM2.CB.enoughAP)
        {
            Instantiate(thirstyThursdayProj, this.transform.position, Quaternion.identity);
            GM2.CL.PostCombatMsg("Chad casts Thirsty Thursday!");
        }
    }

    public void Tick()
    {
        if (hasAffluenza)
        {
           hasAffluenza = false;
            GM2.CL.PostCombatMsg("Chad's Affluenza Defense buff has expired!");
        }
        DrunkHitbox.SetActive(true);
    }
}
