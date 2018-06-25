using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hitBox : MonoBehaviour {

    public GameObject target;
    public string thisPosition = "";

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("hitBox Entered by " + other.name);
        switch (thisPosition)
        {
            case ("right"):
                target.GetComponent<kartController>().objectRight = true;
                break;
            case ("left"):
                target.GetComponent<kartController>().objectLeft = true;
                break;
            case ("forward"):
                target.GetComponent<kartController>().objectForward = true;
                break;
            case ("behind"):
                target.GetComponent<kartController>().objectBehind = true;
                break;
        }
    }

    void OnTriggerExit(Collider other)
    {
        Debug.Log("hitBox Exited by " + other.name);
        switch (thisPosition)
        {
            case ("right"):
                target.GetComponent<kartController>().objectRight = false;
                break;
            case ("left"):
                target.GetComponent<kartController>().objectLeft = false;
                break;
            case ("forward"):
                target.GetComponent<kartController>().objectForward = false;
                break;
            case ("behind"):
                target.GetComponent<kartController>().objectBehind = false;
                break;
        }
    }
}
