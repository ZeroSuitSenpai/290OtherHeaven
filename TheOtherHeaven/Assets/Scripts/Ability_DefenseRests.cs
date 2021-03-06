﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ability_DefenseRests: MonoBehaviour {

    string enemyTag;
    float timer = 0;
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
        timer += Time.deltaTime;
        if (timer >= 0.25f)
        {
            Destroy(this.gameObject);
        }
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.name.Contains("Player_") && other.tag == enemyTag)
        {
            other.GetComponent<CharacterBase>().isStunned = true;
            //Debug.Log("Defense Rests stunned " + other.gameObject.name);
            owner.GetComponent<CombatLog>().PostCombatMsg("Defense Rests stunned " + other.gameObject.GetComponent<CharacterBase>().myName);
        }
    }
}
