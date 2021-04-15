using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float leftWalk;
    public float rightWalk;
    float initialPosition;
    float movement;
    float velocity = 1f;
    bool moveRight = true;
    // Start is called before the first frame update
    void Start()
    {
        initialPosition = transform.position.x;
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.position.x >= initialPosition + rightWalk)
        {
            moveRight = false;
        }
        if(transform.position.x < initialPosition - leftWalk)
        {
            moveRight = true;
        }
        if(moveRight)
        {
            movement = velocity * 1 * Time.deltaTime;
            transform.rotation = movement > 0 ? Quaternion.Euler(0, 180, 0) : Quaternion.identity;
            transform.position += new Vector3(movement, 0, 0);
            
        } else
        {
            movement = velocity * -1 * Time.deltaTime;
            transform.rotation = movement > 0 ? Quaternion.Euler(0, 180, 0) : Quaternion.identity;
            transform.position += new Vector3(movement, 0, 0);
        }

        if (transform.position.y < -6)
        {
            Destroy(this.gameObject);
        }
    }
}
