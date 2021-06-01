using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PointsText : MonoBehaviour
{
    private Text pointText;
    // Start is called before the first frame update
    void Start()
    {
        pointText = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        pointText.text = " = " + PlayerStats.getIstance().getPoints();
    }
}
