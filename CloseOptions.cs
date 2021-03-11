using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseOptions : MonoBehaviour
{
    //panel to close
    public GameObject panel;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //set panel to inactive
    public void closeOptions()
    {
        panel.SetActive(false);
    }
}
