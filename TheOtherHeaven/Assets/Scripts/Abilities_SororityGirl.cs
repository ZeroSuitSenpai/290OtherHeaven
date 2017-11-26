using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Abilities_SororityGirl : MonoBehaviour {

    public GameObject pGM;
    public float fashionModifier;
    public Vector3 startingPos;

	// Use this for initialization
	void Start () {
        fashionModifier = -0.5f;
        startingPos = gameObject.transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    //Moves from sorority girl deal increasingly large amounts of damage based on the last time they're used
    void FashionablyLate()
    {
        //See the change character section of GameMaster for implementation
    }

    //Chauffer:  For this turn, movement costs for sorority girl are halved
    void Chauffer()
    {
        pGM.GetComponent<GameMaster>().CB.moveSpeedModifier = 0.5f;
    }

    //Call Daddy:  A huge single-target, melee-ranged nuke
    void CallDaddy()
    {

    }

    //Throwback Thursday:  Sorority girl teleports to her starting position
    void TBT()
    {
        gameObject.transform.position = startingPos;
    }
}
