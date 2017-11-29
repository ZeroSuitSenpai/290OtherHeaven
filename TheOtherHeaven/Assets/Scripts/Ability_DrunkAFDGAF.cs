using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ability_DrunkAFDGAF : MonoBehaviour {

    string enemyTag;
    public GameObject owner;

    // Use this for initialization
    void Start () {
        if (this.tag == "P1")
        {
            enemyTag = "P2";
        }
        else if (this.tag == "P2")
        {
            enemyTag = "P1";
        }
        else
        {
            Debug.Log("ERROR: Unable to set enemy tag for DrunkAFDGAF");
        }
        owner = GameObject.Find("Main Camera");

    }

    // Update is called once per frame
    void Update () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.name.Contains("Player_") && other.tag == enemyTag)
        {
            other.GetComponent<CharacterBase>().TakeDamage(25);
            //Debug.Log("DrunkAF DGAF dealt 25 damage to " + other.gameObject.name);
            owner.GetComponent<CombatLog>().PostCombatMsg("DrunkAF DGAF dealt 25 damage to " + other.gameObject.GetComponent<CharacterBase>().myName);
        }
    }
}
