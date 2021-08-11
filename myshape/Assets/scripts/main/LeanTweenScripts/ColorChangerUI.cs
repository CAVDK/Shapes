using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColorChangerUI : MonoBehaviour
{
    // Start is called before the first frame update
    public Color[] color;
    public RectTransform goSprite;
    Color endColor;
    [SerializeField] float colorChangeTime;
   [SerializeField] int i;
    void Start()
    {
        ChangeColors();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeColors()
    {
        endColor = color[i];
        i++;
        if (i >= color.Length) i = 0;
        LeanTween.color(goSprite, endColor, colorChangeTime).setOnComplete(ChangeColors);
    }
}
