using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DescriptionDisplay : MonoBehaviour {

    public GameObject descTxt;
    bool shoTxt;
    bool shownTxt;

    float timer = 1.0f;

	// Use this for initialization
	void Start ()
    {
        descTxt.SetActive(false);
        shoTxt = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (shoTxt)
        {
            timer = 3.0f;
            descTxt.SetActive(true);
        }
        else
        {
            timer -= Time.deltaTime;
            if (timer <= 0.0f)
            {
                descTxt.SetActive(false);
            }
        }
    }

    public void ShowText()
    {
        shoTxt = true;
    }

    public void HideText()
    {
        shoTxt = false;
    }
}
