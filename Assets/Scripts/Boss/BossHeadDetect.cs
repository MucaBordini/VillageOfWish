using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHeadDetect : MonoBehaviour
{
    GameObject Boss;
    int life;
    // Start is called before the first frame update
    void Start()
    {
        Boss = gameObject.transform.parent.gameObject;
        life = 2;
    }

    // Update is called once per frame
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(life > 0)
        {
            life--;
            Vector3 movementAlive = new Vector3(70, 50, 0f);
            Boss.transform.position += movementAlive * Time.deltaTime;
        } else
        {
            GetComponent<Collider2D>().enabled = false;
            Boss.GetComponent<BoxCollider2D>().enabled = false;
            Boss.GetComponent<CircleCollider2D>().enabled = false;
            Boss.transform.Rotate(0, 0, 180);
            Vector3 movement = new Vector3(15, 80, 0f);
            Boss.transform.position += movement * Time.deltaTime;
        }
        
    }
}
