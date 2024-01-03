using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pointerControl : MonoBehaviour
{
    public string currentLetter = "";

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "key")
        {
            keyInfo currentKey = col.gameObject.GetComponent<keyInfo>();
            currentKey.currentMaterial=currentKey.onMaterial;
            currentLetter = currentKey.keyName;
        }
    }
    
    public void OnTriggerExit(Collider col)
    {
        if (col.gameObject.tag == "key")
        {
            keyInfo currentKey = col.gameObject.GetComponent<keyInfo>();
            currentKey.currentMaterial = currentKey.offMaterial;
            currentLetter = "";
        }
    }
}
