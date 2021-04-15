 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptCamera : MonoBehaviour
{

    public Transform target;
    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 startPosition = new Vector3(target.position.x, transform.position.y, transform.position.z);
        //Vector3 posicao = new Vector3(player.transform.position.x, 0, -10);
        // this.transform.position = posicao;
        transform.position = startPosition;
    }
}
