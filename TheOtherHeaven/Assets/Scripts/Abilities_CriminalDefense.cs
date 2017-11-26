using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Abilities_CriminalDefense : MonoBehaviour {

    public GameObject[] team;
    public GameObject teammate;

	// Use this for initialization
	void Start () {
        team = GameObject.FindGameObjectsWithTag(gameObject.tag);

        for (int i = 0; i < team.Length; i++)
        {
            if (team[i] == gameObject)
            {
                continue;
            }
            else
            {
                teammate = team[i];
            }
        }
        if (team.Length <= 1)
        {
            Debug.Log("No teammates found for criminal defense besides himself.");
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    //Basic attack:  Heals allies and damages enemies in an area
    void CrossExamination()
    {

    }

    //False Witness:  Grants invisibility to all allies and enemies in the area
    void FalseWitness()
    {

    }

    //The defense rests:  Stuns target enemy for one turn
    void TheDefenseRests()
    {

    }

    //Not Guilty:  Revives a dead teammate
    void NotGuilty()
    {

    }
}
