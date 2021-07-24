using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIANimations : MonoBehaviour
{
    // Start is called before the first frame update
    public float rotateTime=5f;
    public float rotaionAroundZ = 180f;
    public float scaleX, scaleY;
    public float scaleTime;
    private float lastscaledTime;
    public Sprite[] sprites;
    public float spriteChangeInterval;
    [SerializeField] Image img;
    public Color[] color;
    public float colorInterval;
    float lastColorChanged;
    void Start()
    {
        lastscaledTime = Time.time-spriteChangeInterval;
        lastColorChanged = Time.time;
        Tween();
    }

    // Update is called once per frame
    int j=0;
    void Update()
    {

        img.color = Color.Lerp(img.color, color[j], Time.deltaTime * colorInterval);
        if (Time.time - lastColorChanged > colorInterval)
        {

            j = Random.Range(0, color.Length);
            lastColorChanged = Time.time;
        }
       
        if (!(Time.time - lastscaledTime > spriteChangeInterval)) return;
        changeShape();



    }

    public void Tween()
    {
        transform.LeanRotateZ(rotaionAroundZ, rotateTime).setLoopPingPong();
        transform.LeanScaleX(scaleY,  scaleTime).setLoopPingPong();
        transform.LeanScaleY(scaleX,  scaleTime).setLoopPingPong();
        
         
       
    }

    int i = 0;
   
    void changeShape()
    {
        lastscaledTime = Time.time;
        img.sprite = sprites[i];
        i++;
        if (i >= sprites.Length) i = 0;
       

    }
}
