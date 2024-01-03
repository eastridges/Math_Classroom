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
}
