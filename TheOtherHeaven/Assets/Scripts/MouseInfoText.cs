using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MouseInfoText : MonoBehaviour {

    public bool isDisplayed;
    public Text txtComponent;

    public GameObject gMast;
    public GameMaster mGM;

    public float potentialCost;

    // Use this for initialization
    void Start()
    {
        txtComponent = gameObject.GetComponent<Text>();
        mGM = gMast.GetComponent<GameMaster>();
    }

    // Update is called once per frame
    void Update()
    {
        MoveTextWithMouse();
        DisplayMouseText();
        CalculateMoveCost();
    }

    void MoveTextWithMouse()
    {
        transform.position = Input.mousePosition;
    }

    void DisplayMouseText()
    {
        if (isDisplayed)
        {
            txtComponent.enabled = true;
        }
        else
        {
            txtComponent.enabled = false;
        }
    }

    void CalculateMoveCost()
    {
        if (mGM.CB.isBotoxMom)
        {
            potentialCost = Mathf.RoundToInt(mGM.MU.lengthSoFar * mGM.CB.moveSpeedModifier * mGM.currentCharacter.GetComponent<Abilities_BotoxMom>().newYearModifier);
            txtComponent.text = "AP Cost: " + potentialCost.ToString();
        }
        else
        {
            potentialCost = Mathf.RoundToInt(mGM.MU.lengthSoFar * mGM.CB.moveSpeedModifier);
            txtComponent.text = "AP Cost: " + potentialCost.ToString();
        }

        if (mGM.CB.isStunned)
        {
            txtComponent.text = "ROOTED! (1 turn remaining)";
        }
    }

}
