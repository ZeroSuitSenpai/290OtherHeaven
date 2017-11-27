using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class APtext : MonoBehaviour {

    public GameObject mainCam;
    public GameObject gMast;
    public GameMaster aGM;

    public bool isDisplayed;

    public TextMesh myTextMesh;

	// Use this for initialization
	void Start () {
        aGM = gMast.GetComponent<GameMaster>();
        myTextMesh = gameObject.GetComponent<TextMesh>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (isDisplayed)
        {
           myTextMesh.text = "Current AP: " + aGM.CB.actionPoints.ToString();
        }
        else
        {
            myTextMesh.text = "";
        }

        FaceCamera();
	}


    void FaceCamera()
    {
        gameObject.transform.LookAt(mainCam.transform);
        gameObject.transform.Rotate(Vector3.up - new Vector3(0, 180, 0));
    }
}
