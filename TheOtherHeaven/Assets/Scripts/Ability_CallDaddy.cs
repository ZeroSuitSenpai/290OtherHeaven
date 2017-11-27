using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ability_CallDaddy : MonoBehaviour {

    string enemyTag;
    float timer = 0;
    public GameObject owner;

    // Use this for initialization
    void Start () {
        if (this.tag == "P1")
        {
            enemyTag = "P2";
        }
        else if (this.tag == "P2")
        {
            enemyTag = "P1";
        }
        else
        {
            Debug.Log("ERROR: Unable to set enemy tag for DrunkAFDGAF");
        }
	}
	
	// Update is called once per frame
	void Update () {
        timer += Time.deltaTime;
        if (timer >= 0.25f)
        {
            Destroy(this.gameObject);
        }
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.name.Contains("Player_") && other.tag == enemyTag)
        {
            other.GetComponent<CharacterBase>().TakeDamage(Mathf.RoundToInt(35 * owner.GetComponent<Abilities_SororityGirl>().fashionModifier));
            other.GetComponent<CharacterBase>().moveSpeedModifier = 1.5f;
            Debug.Log("CallDaddy dealt " + Mathf.RoundToInt(35 * owner.GetComponent<Abilities_SororityGirl>().fashionModifier) +  " damage to " + other.gameObject.name);
        }
    }

    public void SetOwner(GameObject theOwner)
    {
        owner = theOwner;
    }
}
