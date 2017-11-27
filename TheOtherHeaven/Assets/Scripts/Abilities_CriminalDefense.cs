using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Abilities_CriminalDefense : MonoBehaviour {

    public GameObject[] team;
    public GameObject teammate;

    public GameObject P1CE;
    public GameObject P2CE;
    GameObject crossProj;

    public GameObject P1DR;
    public GameObject P2DR;
    GameObject defenseProj;

    public Material colorBlue;
    public Material colorRed;

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

        if (gameObject.tag == "P1")
        {
            crossProj = P1CE;
            defenseProj = P1DR;
            GetComponent<MeshRenderer>().material = colorRed;
        }
        else if (gameObject.tag == "P2")
        {
            crossProj = P2CE;
            defenseProj = P2DR;
            GetComponent<MeshRenderer>().material = colorBlue;

        }
        else
        {
            Debug.Log("ERROR:  Unable to determine tag for criminal defense.");
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    //Basic attack:  Heals allies and damages enemies in an area
    public void CrossExamination()
    {
        Vector3 sp = Camera.main.WorldToScreenPoint(transform.position);
        Vector3 dir = (Input.mousePosition - sp).normalized;
        Instantiate(crossProj, this.transform.position, Quaternion.identity);
    }

    /*
    //False Witness:  Grants invisibility to all allies and enemies in the area
    void FalseWitness()
    {

    }
    */

    //The defense rests:  Stuns target enemy for one turn
    public void TheDefenseRests()
    {
        Vector3 sp = Camera.main.WorldToScreenPoint(transform.position);
        Vector3 dir = (Input.mousePosition - sp).normalized;
        Instantiate(defenseProj, this.transform.position + (1.5f * dir), Quaternion.identity);
    }

    //Not Guilty:  Revives a dead teammate
    public void NotGuilty()
    {
        Vector3 sp = Camera.main.WorldToScreenPoint(transform.position);
        Vector3 dir = (Input.mousePosition - sp).normalized;
        Instantiate(teammate, this.transform.position + (3.0f * dir), Quaternion.identity);
    }

    public void Tick()
    {

    }
}
