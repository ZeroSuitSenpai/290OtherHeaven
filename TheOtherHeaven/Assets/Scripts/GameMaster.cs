using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameMaster : MonoBehaviour
{

    bool p1Turn;
    bool p2Turn;

    public int charIndex;
    public List<GameObject> cList;
    public GameObject currentCharacter;
    public CharacterBase CB;

    public GameObject mouseInfo;
    public MouseInfoUpdater MU;
    public MouseInput MI;

    public CombatLog CL;

    [SerializeField]
    int livingCharacters;

    public GameObject fratButtons;
    public Button b_Drunk;
    public Button b_Thirsty;
    public Button b_Affluenza;

    public GameObject sororityButtons;
    public Button b_CallDaddy;
    public Button b_Chauffer;
    public Button b_TBT;

    public GameObject botoxButtons;
    public Button b_Young;
    public Button b_RBF;
    public Button b_Assassinate;

    public GameObject lawyerButtons;
    public Button b_Cross;
    public Button b_DefenseRests;

    public Button b_MoveButton;

    // Use this for initialization
    void Start()
    {
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
    void Update()
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

        if (CB.isFratBoy)
        {
            b_Drunk.enabled = true;
            b_Thirsty.enabled = true;
            b_Affluenza.enabled = true;

            b_CallDaddy.enabled = false;
            b_Chauffer.enabled = false;
            b_TBT.enabled = false;

            b_Young.enabled = false;
            b_RBF.enabled = false;
            b_Assassinate.enabled = false;

            b_Cross.enabled = false;
            b_DefenseRests.enabled = false;

            fratButtons.SetActive(true);
            sororityButtons.SetActive(false);
            botoxButtons.SetActive(false);
            lawyerButtons.SetActive(false);
        }
        else if (CB.isBotoxMom)
        {
            b_Drunk.enabled = false;
            b_Thirsty.enabled = false;
            b_Affluenza.enabled = false;

            b_CallDaddy.enabled = false;
            b_Chauffer.enabled = false;
            b_TBT.enabled = false;

            b_Young.enabled = true;
            b_RBF.enabled = true;
            b_Assassinate.enabled = true;

            b_Cross.enabled = false;
            b_DefenseRests.enabled = false;

            fratButtons.SetActive(false);
            sororityButtons.SetActive(false);
            botoxButtons.SetActive(true);
            lawyerButtons.SetActive(false);
        }
        else if (CB.isCriminalLawyer)
        {
            b_Drunk.enabled = false;
            b_Thirsty.enabled = false;
            b_Affluenza.enabled = false;

            b_CallDaddy.enabled = false;
            b_Chauffer.enabled = false;
            b_TBT.enabled = false;

            b_Young.enabled = false;
            b_RBF.enabled = false;
            b_Assassinate.enabled = false;

            b_Cross.enabled = true;
            b_DefenseRests.enabled = true;

            fratButtons.SetActive(false);
            sororityButtons.SetActive(false);
            botoxButtons.SetActive(false);
            lawyerButtons.SetActive(true);
        }
        else if (CB.isSororityGirl)
        {
            b_Drunk.enabled = false;
            b_Thirsty.enabled = false;
            b_Affluenza.enabled = false;

            b_CallDaddy.enabled = true;
            b_Chauffer.enabled = true;
            b_TBT.enabled = true;

            b_Young.enabled = false;
            b_RBF.enabled = false;
            b_Assassinate.enabled = false;

            b_Cross.enabled = false;
            b_DefenseRests.enabled = false;

            fratButtons.SetActive(false);
            sororityButtons.SetActive(true);
            botoxButtons.SetActive(false);
            lawyerButtons.SetActive(false);
        }
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
