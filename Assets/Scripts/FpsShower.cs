using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FpsShower : MonoBehaviour
{
    private string fpsFixed;
    private Text fpsText;
    private void Start()
    {
        fpsText = GetComponent<Text>();
    }
    void Update()
    {
        fpsText.text = "Fps:  " + ((int)(1 / Time.deltaTime)).ToString();
    }
   
}
