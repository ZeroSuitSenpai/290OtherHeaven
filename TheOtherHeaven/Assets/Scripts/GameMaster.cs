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
        GameObject characterToSet = GameObject.Find("Player_FratBoy");
        MU = GameObject.Find("MouseInfoUpdater").GetComponent<MouseInfoUpdater>();
        ChangeCurrentCharacter(characterToSet);
	}
	
    void TakeTurn()
    {
        charIndex += 1;
        if (charIndex >= 4)
        {
            charIndex = 0;
        }
        currentCharacter = charactersArray[charIndex];
    }

	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.W))
        {
            currentCharacter.GetComponent<Abilities_FratBoy>().ThirstyThursday();
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

        //If it's a new turn, and the frat boy already has affluenza, then he activated it last turn so switch it off.
        if (currentCharacter.GetComponent<Abilities_FratBoy>())
        {
            if (currentCharacter.GetComponent<Abilities_FratBoy>().hasAffluenza)
            {
                currentCharacter.GetComponent<Abilities_FratBoy>().hasAffluenza = false;
            }
        }

        if (currentCharacter.GetComponent<Abilities_SororityGirl>())
        {
            currentCharacter.GetComponent<Abilities_SororityGirl>().fashionModifier += 0.5f;
        }

        if (currentCharacter.GetComponent<Abilities_BotoxMom>())
        {
            currentCharacter.GetComponent<Abilities_BotoxMom>().newYearModifier += 0.25f;
        }
    }
}
