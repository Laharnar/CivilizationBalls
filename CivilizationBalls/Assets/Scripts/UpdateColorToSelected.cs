using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpdateColorToSelected : MonoBehaviour
{
    public Image img;
    public Config config;
    public AllColors ballColorsRef;

    // Update is called once per frame
    void Update()
    {
        if(img)
            img.color = ballColorsRef.GetColor(config.currentColor);
    }
}
