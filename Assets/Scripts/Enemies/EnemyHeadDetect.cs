using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHeadDetect : MonoBehaviour
{

    GameObject Enemy;
    // Start is called before the first frame update
    void Start()
    {
        Enemy = gameObject.transform.parent.gameObject;
    }

    // Update is called once per frame
    private void OnCollisionEnter2D(Collision2D collision)
    {
        GetComponent<Collider2D>().enabled = false;
        Enemy.GetComponent<BoxCollider2D>().enabled = false;
        Enemy.GetComponent<CircleCollider2D>().enabled = false;
        Enemy.transform.Rotate(0, 0, 180);
        Vector3 movement = new Vector3(15, 80, 0f);
        Enemy.transform.position += movement * Time.deltaTime;
    }
}
