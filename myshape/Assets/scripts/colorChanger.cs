using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class colorChanger : MonoBehaviour
{
    MeshRenderer mr;
    public Color[] color;
    Color endColor;
    public float updateRate;

    private void Start()
    {
        mr = GetComponent<MeshRenderer>();
        endColor = mr.sharedMaterial.color;
    }

    private void Update()
    {
        mr.sharedMaterial.color = Color.Lerp(mr.sharedMaterial.color, endColor, Time.deltaTime);
        if (endColor == mr.sharedMaterial.color)
            StartCoroutine(ChangeColor());
    }

    IEnumerator ChangeColor()
    {
        endColor = color[Random.Range(0, color.Length)];
        yield return new WaitForSeconds(updateRate);

    }
}