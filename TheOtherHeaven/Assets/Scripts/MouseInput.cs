using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MouseInput : MonoBehaviour
{

    public GameObject gameMast;
    GameMaster GM;
    public GameObject currentCharacter;
    public CharacterBase CB;

    public bool moveToolSelected;

    // Use this for initialization
    void Start()
    {
        GM = gameMast.GetComponent<GameMaster>();
        moveToolSelected = true;
    }

    // Update is called once per frame
    void Update()
    {
        MoveToMousePoint();
    }

    void MoveToMousePoint()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;

            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 100))
            {
                if (GM.MU.validMove)
                {
                    if (CB.isBotoxMom)
                    {
                        CB.SpendAP(Mathf.RoundToInt(GM.MU.lengthSoFar * GM.CB.moveSpeedModifier * GM.currentCharacter.GetComponent<Abilities_BotoxMom>().newYearModifier));
                        if (CB.enoughAP)
                        {
                            CB.navAgent.destination = hit.point;
                        }
                    }
                    else
                    {
                        CB.SpendAP(Mathf.RoundToInt(GM.MU.lengthSoFar * GM.CB.moveSpeedModifier));
                        if (CB.enoughAP)
                        {
                            CB.navAgent.destination = hit.point;
                        }
                    }
                }
            }
        }
    }
}


