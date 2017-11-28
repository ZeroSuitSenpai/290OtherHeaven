using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMaster : MonoBehaviour {

    bool p1Turn;
    bool p2Turn;

    public int charIndex;
    public List <GameObject> cList;
    public GameObject currentCharacter;
    public CharacterBase CB;

    public GameObject mouseInfo;
    public MouseInfoUpdater MU;
    public MouseInput MI;

    public CombatLog CL;

    [SerializeField]
    int livingCharacters;

	// Use this for initialization
	void Start () {
        CL = GetComponent<CombatLog>();
        charIndex = 0;
        livingCharacters = 4;
        p2Turn = !p1Turn;
        GameObject characterToSet = GameObject.Find("Player_CriminalLawyer");
        MU = GameObject.Find("MouseInfoUpdater").GetComponent<MouseInfoUpdater>();
        ChangeCurrentCharacter(characterToSet);
        NewTurn();
    }

    void NewTurn()
    {
        for (int i = 0; i < cList.Count; i++)
        {
            cList[i].GetComponent<MouseInput>().enabled = false;
            cList[i].GetComponentInChildren<APtext>().isDisplayed = false;
            cList[i].GetComponentInChildren<SelectionIndicator>().isDisplayed = false;
            if (!cList[i].GetComponent<CharacterBase>().isAlive)
            {
                Destroy(cList[i]);
                cList.Remove(cList[i]);
                livingCharacters -= 1;
            }

        }
        //Done before the character swap
        if (CB.isFratBoy)
        {
            CB.GetComponent<Abilities_FratBoy>().DrunkHitbox.SetActive(false);
        }

        charIndex += 1;
        if (charIndex >= livingCharacters)
        {
            charIndex = 0;
        }

        ChangeCurrentCharacter(cList[charIndex]);
        //Call tick on a character when it's their turn
        CB.TickBase();
        currentCharacter.GetComponent<MouseInput>().enabled = true;
        currentCharacter.GetComponentInChildren<APtext>().isDisplayed = true;
        currentCharacter.GetComponentInChildren<SelectionIndicator>().isDisplayed = true;

        Debug.Log("Current character:  " + currentCharacter.gameObject.name);
        MU.ResetPosition();

        if (!CB.isAlive)
        {
            for (int i = 0; i < cList.Count; i++)
            {
                if (cList[i] == CB.gameObject)
                {
                    Destroy(cList[i]);
                    cList.Remove(cList[i]);
                    livingCharacters -= 1;
                }
            }
            NewTurn();
        }
    }

	// Update is called once per frame
	void Update ()
    {
        TestInputs();
    }

    void ChangeCurrentCharacter(GameObject newCurrentCharacter)
    {
        currentCharacter = newCurrentCharacter;
        MI = currentCharacter.GetComponent<MouseInput>();
        MI.currentCharacter = newCurrentCharacter;
        CB = currentCharacter.GetComponent<CharacterBase>();
        MI.CB = currentCharacter.GetComponent<CharacterBase>();
        MU.transform.position = currentCharacter.transform.position;
    }

    void TestInputs()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            if (CB.isBotoxMom)
            {
                CB.GetComponent<Abilities_BotoxMom>().RestingBitchFace();
                Debug.Log("Botox Mom casts Resting Bitch Face");
            }
            else if (CB.isCriminalLawyer)
            {
                CB.GetComponent<Abilities_CriminalDefense>().CrossExamination();
                Debug.Log("Criminial Lawyer casts Cross Examination");
            }
            else if (CB.isSororityGirl)
            {
                CB.GetComponent<Abilities_SororityGirl>().CallDaddy();
                Debug.Log("Sorority Girl casts Call Daddy0");

            }
            else if (CB.isFratBoy)
            {
                CB.GetComponent<Abilities_FratBoy>().AffluenzaDefense();
                Debug.Log("Frat Bot casts Affluenza");
            }
            else
            {
                Debug.Log("ERROR:  Something is wrong with CB being set.");
            }
        }
        if (Input.GetKeyDown(KeyCode.X))
        {
            if (CB.isBotoxMom)
            {
                CB.GetComponent<Abilities_BotoxMom>().YoungAgain();
                Debug.Log("Botox Mom casts Young Again");
            }
            else if (CB.isCriminalLawyer)
            {
                CB.GetComponent<Abilities_CriminalDefense>().TheDefenseRests();
                Debug.Log("Criminial Lawyer casts Defense Rests");
            }
            else if (CB.isSororityGirl)
            {
                CB.GetComponent<Abilities_SororityGirl>().Chauffer();
                Debug.Log("Sorority Girl casts Chauffer");

            }
            else if (CB.isFratBoy)
            {
                CB.GetComponent<Abilities_FratBoy>().ThirstyThursday();
                Debug.Log("Frat Boy cast Thirsty Thursday");
            }
            else
            {
                Debug.Log("ERROR:  Something is wrong with CB being set.");
            }
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            if (CB.isBotoxMom)
            {
                CB.GetComponent<Abilities_BotoxMom>().CharacterAssassination();
                Debug.Log("Botox Mom casts Character Assassination");

            }
            else if (CB.isCriminalLawyer)
            {
                CB.GetComponent<Abilities_CriminalDefense>().NotGuilty();
                Debug.Log("Criminial Lawyer casts Not Guilty");
            }
            else if (CB.isSororityGirl)
            {
                CB.GetComponent<Abilities_SororityGirl>().TBT();
                Debug.Log("Sorority Girl casts TBT");
            }
            else if (CB.isFratBoy)
            {
                Debug.Log("No ability bound to C for Frat Boy");
            }
            else
            {
                Debug.Log("ERROR:  Something is wrong with CB being set.");
            }
        }

        if (CB.isBotoxMom)
        {
            CB.moveSpeedModifier = CB.GetComponent<Abilities_BotoxMom>().newYearModifier;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            NewTurn();
        }
    }
}
