using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CharacterBase : MonoBehaviour
{

    public int actionPoints;
    public int health;
    public bool isAlive;
    public NavMeshAgent navAgent;
    public MouseInput playerMI;

    [SerializeField]
    bool isFratBoy = false;
    [SerializeField]
    bool isSororityGirl = false;
    [SerializeField]
    bool isCriminalLawyer = false;
    [SerializeField]
    bool isBotoxMom = false;

    [SerializeField]
    public float moveSpeedModifier = 1.0f;


    // Use this for initialization
    void Start ()
    {
        actionPoints = 10;
        health = 100;
        isAlive = true;
        navAgent = GetComponent<NavMeshAgent>();
        playerMI = GetComponent<MouseInput>();

        if (GetComponent<Abilities_FratBoy>())
        {
            isFratBoy = true;
        }
        else if (GetComponent<Abilities_SororityGirl>())
        {
            isSororityGirl = true;
        }
        else if (GetComponent<Abilities_CriminalDefense>())
        {
            isCriminalLawyer = true;
        }
        else if (GetComponent<Abilities_BotoxMom>())
        {
            isBotoxMom = true;
        }
        else if (gameObject.name == "Player_TestDummy")
        {
            Debug.Log("No abilities set on test dummy, but that's probably okay");
        }
        else
        {
            Debug.Log("ERROR:  No abilities set for " + this.gameObject.name + " but it has a CB script.");
        }
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    public void SpendAP(int amtToSpend)
    {
        if (actionPoints - amtToSpend >= 0)
        {
            Debug.Log("Amount to spend: " + amtToSpend);
            actionPoints = actionPoints - amtToSpend;
        }
    }

    public void TakeDamage(int amtToTake)
    {
        if (!isFratBoy)
        {
            health -= amtToTake;
            if (health <= 0)
            {
                isAlive = false;
                Debug.Log(this.gameObject.name + "died!");
            }
        }
        else
        {
            if (!GetComponent<Abilities_FratBoy>().hasAffluenza)
            {
                health -= amtToTake;
                if (health <= 0)
                {
                    isAlive = false;
                    Debug.Log(this.gameObject.name + "died!");
                }
            }
            else
            {
                Debug.Log("Damage to Frat Boy prevented by affluenza");
            }
        }
    }
}
