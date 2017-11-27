using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectionIndicator : MonoBehaviour {

    public bool isDisplayed;
    public MeshRenderer myMesh;

	// Use this for initialization
	void Start () {
        myMesh = gameObject.GetComponent<MeshRenderer>();
	}
	
	// Update is called once per frame
	void Update () {
		if (isDisplayed)
        {
            myMesh.enabled = true;
        }
        else
        {
            myMesh.enabled = false;
        }
	}
}
