using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMaster : MonoBehaviour {

    bool p1Turn;
    bool p2Turn;

    public int charIndex;
    public GameObject[] charactersArray;
    public GameObject currentCharacter;
    public CharacterBase CB;

    public GameObject mouseInfo;
    public MouseInfoUpdater MU;
    public MouseInput MI;

	// Use this for initialization
	void Start () {
        p2Turn = !p1Turn;
        /*
        GameObject[] p1Array = GameObject.FindGameObjectsWithTag("P1");
        GameObject[] p2Array = GameObject.FindGameObjectsWithTag("P2");
        for (int i = 0; i < p1Array.Length; i++)
        {
            charactersArray[i] = p1Array[i];
        }

        for (int j = 0; j < p2Array.Length; j++)
        {
            charactersArray[p1Array.Length + j] = p2Array[j];
        }
        */
        GameObject characterToSet = GameObject.Find("Player_CriminalLawyer");
        MU = GameObject.Find("MouseInfoUpdater").GetComponent<MouseInfoUpdater>();
        ChangeCurrentCharacter(characterToSet);
    }

    void NewTurn()
    {
        charIndex += 1;
        if (charIndex >= 4)
        {
            charIndex = 0;
        }
        currentCharacter = charactersArray[charIndex];
        //Call tick on a character when it's their turn
        CB.TickBase();

        for (int i = 0; i < charactersArray.Length; i++)
        {
            charactersArray[i].GetComponent<MouseInput>().enabled = false;
        }
        currentCharacter.GetComponent<MouseInput>().enabled = true;
    }

	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.W))
        {
            currentCharacter.GetComponent<Abilities_FratBoy>().ThirstyThursday();
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            currentCharacter.GetComponent<Abilities_CriminalDefense>().CrossExamination();
        }
        if (Input.GetKeyDown(KeyCode.L))
        {
            currentCharacter.GetComponent<Abilities_CriminalDefense>().TheDefenseRests();
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

    void ChangeCurrentCharacter(GameObject newCurrentCharacter)
    {
        currentCharacter = newCurrentCharacter;
        MI = currentCharacter.GetComponent<MouseInput>();
        MI.currentCharacter = newCurrentCharacter;
        CB = currentCharacter.GetComponent<CharacterBase>();
        MI.CB = currentCharacter.GetComponent<CharacterBase>();
        MU.transform.position = currentCharacter.transform.position;
    }
}
