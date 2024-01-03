using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewGrabbable : MonoBehaviour
{
    public InputReader inputs;
    public Transform rh;
    public Transform lh;

    private bool rightHandTouch;
    private bool leftHandTouch;

    // Start is called before the first frame update
    void Start()
    {
        leftHandTouch=false;
        rightHandTouch=false;
    }

    // Update is called once per frame
    void Update()
    {
        //To allow the object to be picked up by the left hand
        if (leftHandTouch && inputs.LeftGrip)
        {
            this.transform.SetParent(lh);
        }
        if (inputs.LeftGripUp && leftHandTouch)
        {
            this.transform.SetParent(null);
        }

        
        //to allow the object to be picked up by the right hand
        if (rightHandTouch && inputs.RightGrip)
        {
            this.transform.SetParent(rh);
        }
        if (inputs.RightGripUp && rightHandTouch)
        {
            this.transform.SetParent(null);
        }

        //just a double check to make sure the object doesn't stick to the controller
        if (!inputs.RightGrip && !inputs.LeftGrip)
        {
            this.transform.SetParent(null);
        }
    }

    public void Setup(InputReader importInputs, Transform ImportRh, Transform ImportLh)
    {
        inputs=importInputs;
        rh=ImportRh;
        lh=ImportLh;
    }

    //these detect whether the controller is touching object
    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "RH")
        {
            rightHandTouch=true;
        }
        else if (col.gameObject.tag == "LH")
        {
            Debug.Log("left hand touch");
            leftHandTouch=true;
        }
    }

    void OnTriggerExit(Collider col)
    {
        if (col.gameObject.tag == "RH")
        {
            rightHandTouch=false;
        }
        else if (col.gameObject.tag == "LH")
        {
            leftHandTouch=false;
        }
    }
}
