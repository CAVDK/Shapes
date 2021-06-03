
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameObject[] shapes;
    int i = 0;
    int previousIndex;
    public float moveSpeed = 3f;
    public Vector2 offset;
    Vector3 directionToMoveIn;
    private void Start()
    {
        i = 0;
        shapes[i].SetActive(true);
        shapes[1].SetActive(false);
        shapes[2].SetActive(false);
    }
   
    
    private void Update()
    {
        if(Input.GetButtonDown("Fire1"))
        {
            previousIndex = i;
            shapes[previousIndex].SetActive(false);
            i++;
            if (i > shapes.Length - 1 || i < 0)
            {
                i = 0;
              //  previousIndex = shap
            }
            
            shapes[i].SetActive(true);
            
          
           

           

        }

        if(Input.GetButton("Fire1"))
        {
            Vector3 tempPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 _2dTempPosition = new Vector2(tempPosition.x, tempPosition.y); //+ offset;

            // transform.position = Vector3.Lerp(transform.position, _2dTempPosition,Time.deltaTime*moveSpeed);
            tempPosition.z = 0f;
            directionToMoveIn = tempPosition - transform.position;

        }
        
        transform.Translate(directionToMoveIn.normalized*moveSpeed * Time.deltaTime);

    }
   
}
