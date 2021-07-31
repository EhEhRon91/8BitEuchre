using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeBackgroundColour : MonoBehaviour
{
    private int colourID;
    private Color colorToChange;
    private GameObject background;
    // Start is called before the first frame update
    void Start()
    {
        //grab background and colour on start
        background = GameObject.FindGameObjectWithTag("Background");
        colorToChange = gameObject.GetComponent<Image>().color;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void setColor()
    {
        //set colors using playerpref
        background.GetComponent<Image>().color = colorToChange;
        PlayerPrefs.SetInt("backgroundColor", colourID);
    }

}
