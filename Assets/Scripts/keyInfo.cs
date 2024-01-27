using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class keyInfo : MonoBehaviour
{
    public string keyName = "1";
    public Material currentMaterial;
    public Material offMaterial;
    public Material onMaterial;
    public TextMeshPro keyLabel;
    public bool bigKey = false;

    // Start is called before the first frame update
    void Start()
    {
        currentMaterial = offMaterial;
        keyLabel.SetText(keyName);
    }

    // Update is called once per frame
    void Update()
    {
        this.GetComponent<MeshRenderer>().material = currentMaterial;
    }

    public void MakeBigger()
    {
        if (bigKey)
        {
            this.transform.localScale = new Vector3(.31f, .01f, .11f);
        }
        else
        {
            this.transform.localScale = new Vector3(.11f, .01f, .11f);
        }
    }

    public void MakeSmaller()
    {
        if (bigKey)
        {
            this.transform.localScale = new Vector3(.3f, .01f, .1f);
        }
        else
        {
            this.transform.localScale = new Vector3(.1f, .01f, .1f);
        }
        
    }
}
