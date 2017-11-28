using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class CombatLog : MonoBehaviour {

    public GameObject box1;
    public GameObject box2;
    public GameObject box3;
    public GameObject box4;

    public Text txt1;
    public Text txt2;
    public Text txt3;
    public Text txt4;

    // Use this for initialization
    void Start ()
    {
        txt1 = box1.GetComponent<Text>();
        txt2 = box2.GetComponent<Text>();
        txt3 = box3.GetComponent<Text>();
        txt4 = box4.GetComponent<Text>();
    }

    // Update is called once per frame
    void Update ()
    {
        PostCombatMsg("");
    }

    public void PostCombatMsg(string inMessage)
    {
        txt4.text = txt3.text;
        txt3.text = txt2.text;
        txt2.text = txt1.text;
        txt1.text = inMessage;
    }
}
