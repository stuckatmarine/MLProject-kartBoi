using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hitBox : MonoBehaviour {

    public GameObject kart;
    public string thisPosition = "";

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    /* When another collider enters the hitbox, set corresponding bool in kartController script */
    void OnTriggerEnter(Collider other)
    {
        Debug.Log("hitBox Entered by " + other.name);
        switch (thisPosition)
        {
            case ("right"):
                kart.GetComponent<kartController>().objectRight = true;
                break;
            case ("left"):
                kart.GetComponent<kartController>().objectLeft = true;
                break;
            case ("forward"):
                kart.GetComponent<kartController>().objectForward = true;
                break;
            case ("behind"):
                kart.GetComponent<kartController>().objectBehind = true;
                break;
        }
    }

    /* When another collider exits the hitbox, set corresponding bool in kartController script */
    void OnTriggerExit(Collider other)
    {
        Debug.Log("hitBox Exited by " + other.name);
        switch (thisPosition)
        {
            case ("right"):
                kart.GetComponent<kartController>().objectRight = false;
                break;
            case ("left"):
                kart.GetComponent<kartController>().objectLeft = false;
                break;
            case ("forward"):
                kart.GetComponent<kartController>().objectForward = false;
                break;
            case ("behind"):
                kart.GetComponent<kartController>().objectBehind = false;
                break;
        }
    }
}
