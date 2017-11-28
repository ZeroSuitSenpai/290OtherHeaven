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
        if (GM.MI.moveToolSelected)
        {
            OnDrawGizmosSelected();
        }

        if (GM.CB.navAgent.remainingDistance <= 0)
        {
            gameObject.transform.position = currentCharacter.transform.position;
        }
        
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

    void OnDrawGizmosSelected()
    {

        var nav = GetComponent<NavMeshAgent>();
        if (nav == null || nav.path == null)
            return;

        var line = this.GetComponent<LineRenderer>();
        if (line == null)
        {
            line = this.gameObject.AddComponent<LineRenderer>();
            line.material = new Material(Shader.Find("Sprites/Default")) { color = Color.yellow };
            line.SetWidth(0.5f, 0.5f);
        }

        var path = nav.path;

        line.SetVertexCount(path.corners.Length);
        if (validMove)
        {
            line.SetColors(Color.green, Color.green);
        }
        else
        {
            line.SetColors(Color.red, Color.red);
        }
        for (int i = 0; i < path.corners.Length; i++)
        {
            line.SetPosition(i, path.corners[i] + new Vector3(0.0f, 0.5f, 0.0f));
        }
    }

    public void ResetPosition()
    {
        gameObject.transform.position = currentCharacter.transform.position;
    }
}

