using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class kartController : MonoBehaviour {

    [Header("GUI textboxes")]
    public Text xLocationText;
    public Text zLocationText;
    public Text distToTargetText;
    public Text currentTargetText;

    [Header("Mission Gameobjects")]
    public Transform currentTarget;     //position of target
    public Transform exit;              //position of exit

    [Header("Current Status")]
    public float distToTarget;          //distance to target (< 2 = adjacent or diagonal)
    public bool targetVisible = false;
    public bool targetReached = false;
    public bool objectForward = false;  //is an object blocking the forward direction
    public bool objectBehind = false;
    public bool objectRight = false;
    public bool objectLeft = false;
    public bool roundComplete = false;
    public int numActualMoves = 0;
    public int numRequestedMoves = 0;

    // Use this for initialization
    void Start () {
        xLocationText.text = (transform.position.x).ToString();
        zLocationText.text = (transform.position.z).ToString();
        UpdateDistToTarget();
        currentTargetText.text = currentTarget.name;
    }
	
	// Update is called once per frame (30/sec)
	void Update () {
        
	}

    // Update called once physics engine frame (24/sec)
    private void FixedUpdate()
    {
    }

    /* Move kart amount on X axis  of grid if not blocked by object, truncated to integer values */
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
            numActualMoves += Mathf.Abs(amount);
        }

        numRequestedMoves += Mathf.Abs(amount);
    }

    /* Move kart amount on Z axis grid if not blocked by object, truncated to integer values */
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
            transform.position = new Vector3(transform.position.x, transform.position.y, (int)(transform.position.z + 0.5) + amount);
            zLocationText.text = ((int)transform.position.z).ToString();
            UpdateDistToTarget();
            numActualMoves += Mathf.Abs(amount);
        }

        numRequestedMoves += Mathf.Abs(amount);
    }

    /* Get distance to current target for GUI */
    private void UpdateDistToTarget()
    {
        distToTarget = Vector3.Distance(currentTarget.position, transform.position);

        if (distToTarget < 2) // if close enough change target to exit
        {
            if (currentTarget.name == exit.name) //if close enough to target exit
            {
                roundComplete = true;
                Debug.Log("Round completed in " + numActualMoves + " moves with " + numRequestedMoves + " move requests");
                xLocationText.text = numActualMoves.ToString();
                zLocationText.text = numRequestedMoves.ToString();
                currentTargetText.text = "Done";
            }
            else
            {
                targetReached = true;
                currentTarget = exit;
                currentTargetText.text = currentTarget.name;
            }
        }

        distToTargetText.text = distToTarget.ToString();
        LookForTarget();
    }

    /* Raycast from kart to target to confirm a line of sight */
    private void LookForTarget()
    {
        Debug.DrawLine(transform.position, new Vector3(currentTarget.position.x, 0.25f, currentTarget.position.z), Color.red, 1.0f);
        RaycastHit hit;

        //requires check for tag
        if (Physics.Linecast(transform.position, new Vector3(currentTarget.position.x, 0.25f, currentTarget.position.z), out hit)){            
            Debug.Log(hit.collider.tag);
            if (hit.collider == currentTarget.GetComponent<Collider>())
                targetVisible = true;
        } else
            targetVisible = false;
    }

    /* Reload scene based on scene index number */
    public void reloadScene(int sceneIndex)
    {
        SceneManager.LoadScene(sceneIndex);
    }

/******** Partial functions not implemented/obsolete ***********/

// Class to be used in a list of sequential targets
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
/*
 * private void CheckForObstacles()
{
   objectRight = Physics.Linecast(transform.position, new Vector3(transform.position.x + 1f, transform.position.y, transform.position.z));
   objectLeft = Physics.Linecast(transform.position, new Vector3(transform.position.x - 1f, transform.position.y, transform.position.z));
   objectForward = Physics.Linecast(transform.position, new Vector3(transform.position.x, transform.position.y, transform.position.z + 1f));
   objectBehind = Physics.Linecast(transform.position, new Vector3(transform.position.x, transform.position.y, transform.position.z - 1f));
}
*/

}
