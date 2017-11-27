using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Abilities_BotoxMom : MonoBehaviour {

    public float newYearModifier;

	// Use this for initialization
	void Start () {
        newYearModifier = 0.25f;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    //New Year New Me:  Passive, Botox Mom moves twice as fast as other characters, decreasing by 50% for each turn
    public void NewYearNewMe()
    {
        //Implemented in mouse info updater
    }

    //Young Again:  Botox mom refreshes her passive (new year new me)
    public void YoungAgain()
    {
        newYearModifier = 0.25f;
    }


    //RBF:  A long-ranged line nuke
    public void RestingBitchFace()
    {

    }

    //Character Assassination:  Botox mom targets an enemy anywhere on the map and deals damage to them
    public void CharacterAssassination()
    {

    }

    public void Tick()
    {
        newYearModifier += 0.25f;
    }
}
