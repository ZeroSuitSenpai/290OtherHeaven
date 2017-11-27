using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Abilities_FratBoy : MonoBehaviour {

    public bool hasAffluenza;
    public GameObject P1TT;
    public GameObject P2TT;

    public GameObject thirstyThursdayProj;

	// Use this for initialization
	void Start ()
    {
        hasAffluenza = false;
        if (gameObject.tag == "P1")
        {
            thirstyThursdayProj = P1TT;
        }
        else if (gameObject.tag == "P2")
        {
            thirstyThursdayProj = P2TT;
        }
        else
        {
            Debug.Log("ERROR:  Unable to determine tag for frat boy.");
        }
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
        hasAffluenza = true;
    }

    //Thirsty Thursday:  Frat boy slams a keg on the ground, damaging and silencing all around him
    public void ThirstyThursday()
    {
        Instantiate(thirstyThursdayProj, this.transform.position, Quaternion.identity);
    }

    public void Tick()
    {
        if (hasAffluenza)
        {
           hasAffluenza = false;
        }
    }
}
