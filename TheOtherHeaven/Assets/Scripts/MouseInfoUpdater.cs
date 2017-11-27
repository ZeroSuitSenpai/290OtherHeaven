using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MouseInfoUpdater : MonoBehaviour
{

    public GameObject gameMast;
    GameMaster GM;
    public GameObject currentCharacter;
    public NavMeshAgent mouseInfoAgent;

    public float lengthSoFar;

    public bool validMove;

    // Use this for initialization
    void Start()
    {
        GM = gameMast.GetComponent<GameMaster>();
        mouseInfoAgent = GetComponent<NavMeshAgent>();
        validMove = false;
    }

    // Update is called once per frame
    void Update()
    {
        CheckIfValidMove();
        CalculatePathDistance();

        currentCharacter = GM.currentCharacter;
        //this.transform.position = currentCharacter.transform.position;
    }

    void CalculatePathDistance()
    {
        RaycastHit hit;

        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 100))
        {
            mouseInfoAgent.destination = hit.point;
            mouseInfoAgent.Stop();

            NavMeshPath path = mouseInfoAgent.path;

            Vector3 previousCorner = path.corners[0];
            lengthSoFar = 0.0F;
            int i = 1;
            while (i < path.corners.Length)
            {
                Vector3 currentCorner = path.corners[i];
                lengthSoFar += Vector3.Distance(previousCorner, currentCorner);
                previousCorner = currentCorner;
                i++;
            }
            if (!mouseInfoAgent.pathPending)
            {
               // Debug.Log("Path distance:  " + lengthSoFar);
            }
        }
    }

    void CheckIfValidMove()
    {
        if (GM.currentCharacter.GetComponent<Abilities_BotoxMom>())
        {
            if (GM.CB.actionPoints - (lengthSoFar * GM.CB.moveSpeedModifier * GM.currentCharacter.GetComponent<Abilities_BotoxMom>().newYearModifier) >= 0)
            {
                validMove = true;
            }
            else
            {
                validMove = false;
            }
        }
        else if (GM.CB.actionPoints - (lengthSoFar * GM.CB.moveSpeedModifier) >= 0)
        {
            validMove = true;
        }
        else
        {
            validMove = false;
        }
    }
}

