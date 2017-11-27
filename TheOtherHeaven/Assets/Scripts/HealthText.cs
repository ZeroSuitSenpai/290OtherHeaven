using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthText : MonoBehaviour {

    public GameObject mainCam;
    public GameObject myCharacter;
    public CharacterBase myCB;

	// Use this for initialization
	void Start () {
        myCB = myCharacter.GetComponent<CharacterBase>();
	}
	
	// Update is called once per frame
	void Update () {
        FaceCamera();
        if (myCB.health <= 100)
        {
            gameObject.GetComponent<TextMesh>().text = myCB.health + "/100";
        }
        else
        {
            gameObject.GetComponent<TextMesh>().text = myCB.health + "/" + myCB.health;
        }
    }

    void FaceCamera()
    {
        gameObject.transform.LookAt(mainCam.transform);
        gameObject.transform.Rotate(Vector3.up - new Vector3(0, 180, 0));
    }
}
