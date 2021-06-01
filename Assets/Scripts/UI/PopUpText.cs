using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopUpText : MonoBehaviour
{

    private static Text popupText;

    void Start()
    {
        popupText = GetComponent<Text>();
    }

    public static void fillPopUp(string textToPop)
    {
        popupText.text = textToPop;
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

}
