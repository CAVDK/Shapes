
using UnityEngine;

public class enemy : MonoBehaviour
{
    public float enemySpeed;

    private void Update()
    {
        transform.Translate(Vector2.down * enemySpeed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            Destroy(gameObject);
           // Debug.Log("playerTouch");
        }
    }
}
