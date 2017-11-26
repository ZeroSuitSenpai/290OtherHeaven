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

    // Use this for initialization
    void Start()
    {
        GM = gameMast.GetComponent<GameMaster>();
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
                    CB.navAgent.destination = hit.point;
                    CB.SpendAP(Mathf.RoundToInt(GM.MU.lengthSoFar));
                }
            }
        }
    }
}

