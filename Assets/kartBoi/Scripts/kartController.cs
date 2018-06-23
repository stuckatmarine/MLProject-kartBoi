using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class kartController : MonoBehaviour {


    public Text xLocationText;
    public Text zLocationText;
    public Text distToTargetText;

    public Transform currentTarget; //position of target
    public float distToTarget; //distance to target (x & y)

    public bool objectForward = false; //detect if objects around cart
    public bool objectBehind = false;
    public bool objectRight = false;
    public bool objectLeft = false;

    // Use this for initialization
    void Start () {
        xLocationText.text = (transform.position.x).ToString();
        zLocationText.text = (transform.position.z).ToString();
        UpdateDistToTarget();
    }
	
	// Update is called once per frame
	void Update () {
        
	}

    // Update called once physics engine frame (24/sec)
    private void FixedUpdate()
    {
        
    }

    public void moveX(int amount)
    {
        if (amount > 0 && Physics.Linecast(transform.position, new Vector3(transform.position.x + 1f, transform.position.y, transform.position.z)))
        {
            Debug.Log("blockedRight");
        }
        else if (amount < 0 && Physics.Linecast(transform.position, new Vector3(transform.position.x - 1f, transform.position.y, transform.position.z)))
        {
            Debug.Log("blockedLeft");
        }
        else
        {
            this.transform.position = new Vector3(this.transform.position.x + amount, this.transform.position.y, this.transform.position.z);
            xLocationText.text = ((int)transform.position.x).ToString();
            UpdateDistToTarget();
        }
    }

    public void moveZ(int amount)
    {
        if (amount > 0 && Physics.Linecast(transform.position, new Vector3(transform.position.x, transform.position.y, transform.position.z + 1f)))
        {
            Debug.Log("blockedForward");
        }
        else if (amount < 0 && Physics.Linecast(transform.position, new Vector3(transform.position.x, transform.position.y, transform.position.z - 1f)))
        {
            Debug.Log("blockedReverse");
        }
        else
        {
            this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z + amount);
            zLocationText.text = ((int)transform.position.z).ToString();

            UpdateDistToTarget();
        }
    }

    private void UpdateDistToTarget()
    {
        distToTarget = Vector3.Distance(currentTarget.position, transform.position);
        distToTargetText.text = distToTarget.ToString();
    }
}
