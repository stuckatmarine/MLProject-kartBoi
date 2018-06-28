using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class kartController : MonoBehaviour {

    // Gui textbox objects
    public Text xLocationText;
    public Text zLocationText;
    public Text distToTargetText;

    // Class for each target to be reached
 /*   public class Target
    {
        public GameObject GameObject;
        public Transform transform;
        public bool found = false;
        public bool seen = false;
        public float distance;
    }
    public List<Target> targetList; // list of targets to be reached
*/
    public Transform currentTarget; //position of target
    public bool targetFound = false;
    public float distToTarget; //distance to target (x & y)

    public int currentTargetIndex; // List index of current target
 //   public Target exit; // Target after all other targets are found

    public bool objectForward = false; //detect if objects around cart
    public bool objectBehind = false;
    public bool objectRight = false;
    public bool objectLeft = false;

    // Use this for initialization
    void Start () {
        xLocationText.text = (transform.position.x).ToString();
        zLocationText.text = (transform.position.z).ToString();
        UpdateDistToTarget();
        //CheckForObstacles();
     //   exit.GameObject = GameObject.Find("Exit"); 
    }
	
	// Update is called once per frame
	void Update () {
        
	}

    // Update called once physics engine frame (24/sec)
    private void FixedUpdate()
    {
    //    CheckForObstacles();  // will lov eupdate if moving objects, requires more resources
    }

    public void moveX(int amount)
    {
        if (amount > 0 && objectRight)
        {
            Debug.Log("blockedRight");
        }
        else if (amount < 0 && objectLeft)
        {
            Debug.Log("blockedLeft");
        }
        else
        {
            this.transform.position = new Vector3((int)(this.transform.position.x + 0.5) + amount, this.transform.position.y, this.transform.position.z);
            xLocationText.text = ((int)transform.position.x).ToString();
            UpdateDistToTarget();
           // CheckForObstacles();
        }
    }

    public void moveZ(int amount)
    {
        if (amount > 0 && objectForward)
        {
            Debug.Log("blockedForward");
        }
        else if (amount < 0 && objectBehind)
        {
            Debug.Log("blockedReverse");
        }
        else
        {
            this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, (int)(this.transform.position.z + 0.5) + amount);
            zLocationText.text = ((int)transform.position.z).ToString();
            UpdateDistToTarget();
        }
    }

    private void UpdateDistToTarget()
    {
        distToTarget = Vector3.Distance(currentTarget.position, transform.position);
        distToTargetText.text = distToTarget.ToString();
        LookForTarget();
    }

    private void CheckForObstacles()
    {
        objectRight = Physics.Linecast(transform.position, new Vector3(transform.position.x + 1f, transform.position.y, transform.position.z));
        objectLeft = Physics.Linecast(transform.position, new Vector3(transform.position.x - 1f, transform.position.y, transform.position.z));
        objectForward = Physics.Linecast(transform.position, new Vector3(transform.position.x, transform.position.y, transform.position.z + 1f));
        objectBehind = Physics.Linecast(transform.position, new Vector3(transform.position.x, transform.position.y, transform.position.z - 1f));
    }

    // raycast from kart to target to confirm a line of sign and travel towards
    private void LookForTarget()
    {
        //requires check for tag
        targetFound = Physics.Linecast(transform.position, new Vector3(currentTarget.position.x, currentTarget.position.y, currentTarget.position.z));
    }
}
