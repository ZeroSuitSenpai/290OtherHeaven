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

    public bool isFratBoy = false;
    public bool isSororityGirl = false;
    public bool isCriminalLawyer = false;
    public bool isBotoxMom = false;

    public bool isStunned;
    public bool alreadyStunned;

    [SerializeField]
    public float moveSpeedModifier = 1.0f;

    public bool enoughAP;

    public GameObject gMast;

    public string myName;

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
            myName = "Chad";
        }
        else if (GetComponent<Abilities_SororityGirl>())
        {
            isSororityGirl = true;
            myName = "Tiffany";
        }
        else if (GetComponent<Abilities_CriminalDefense>())
        {
            isCriminalLawyer = true;
            myName = "Mr. Manchester";
        }
        else if (GetComponent<Abilities_BotoxMom>())
        {
            isBotoxMom = true;
            myName = "Claudia";
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
        enoughAP = false;
        if (actionPoints - amtToSpend >= 0)
        {
            enoughAP = true;
            Debug.Log("SpentAP: " + amtToSpend);
            actionPoints = actionPoints - amtToSpend;
        }
        else
        {
            enoughAP = false;
            //Debug.Log("ERROR:  Not enough AP to complete move");
            gMast.GetComponent<CombatLog>().PostCombatMsg("Not enough AP to perform action.");
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
                //Debug.Log(this.gameObject.name + "died!");
                gMast.GetComponent<CombatLog>().PostCombatMsg(myName + " died!");

                gameObject.GetComponent<MeshRenderer>().enabled = false;
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
                    //Debug.Log(this.gameObject.name + "died!");
                    gMast.GetComponent<CombatLog>().PostCombatMsg(myName + " died!");
                    gameObject.GetComponent<MeshRenderer>().enabled = false;
                }
            }
            else
            {
                //Debug.Log("Damage to Frat Boy prevented by affluenza");
                gMast.GetComponent<CombatLog>().PostCombatMsg("Legal damages to Chad prevented by Affluenza Defense!");
            }
        }
    }

    public void TickBase()
    {
        if (isFratBoy)
        {
            gMast.GetComponent<CombatLog>().PostCombatMsg("");
            gMast.GetComponent<CombatLog>().PostCombatMsg("");
            gMast.GetComponent<CombatLog>().PostCombatMsg("Chad's turn!");
            GetComponent<Abilities_FratBoy>().Tick();
            HandleStun();
            TurnStartAPGain();
        }
        else if (isSororityGirl)
        {
            gMast.GetComponent<CombatLog>().PostCombatMsg("");
            gMast.GetComponent<CombatLog>().PostCombatMsg("");
            gMast.GetComponent<CombatLog>().PostCombatMsg("Tiffany's turn!");
            GetComponent<Abilities_SororityGirl>().Tick();
            HandleStun();
            TurnStartAPGain();
        }
        else if (isBotoxMom)
        {
            gMast.GetComponent<CombatLog>().PostCombatMsg("");
            gMast.GetComponent<CombatLog>().PostCombatMsg("");
            gMast.GetComponent<CombatLog>().PostCombatMsg("Claudia's turn!");
            GetComponent<Abilities_BotoxMom>().Tick();
            HandleStun();
            TurnStartAPGain();

        }
        else if (isCriminalLawyer)
        {
            gMast.GetComponent<CombatLog>().PostCombatMsg("");
            gMast.GetComponent<CombatLog>().PostCombatMsg("");
            gMast.GetComponent<CombatLog>().PostCombatMsg("Mr. Manchester's turn!");
            GetComponent<Abilities_CriminalDefense>().Tick();
            HandleStun();
            TurnStartAPGain();
        }
    }

    public void HandleStun()
    {
        if (isStunned && !alreadyStunned)
        {
            alreadyStunned = true;
            moveSpeedModifier *= 999.0f;
        }
        else if (isStunned && alreadyStunned)
        {
            isStunned = false;
            alreadyStunned = false;
            moveSpeedModifier = 1.0f;
        }
    }

    public void TurnStartAPGain()
    {
        actionPoints += 8;
        if (actionPoints > 10)
        {
            actionPoints = 10;
        }
        Debug.Log("Current AP:  " + actionPoints);
        gMast.GetComponent<CombatLog>().PostCombatMsg("You gain 8 AP for beginning your turn.");
    }
}
