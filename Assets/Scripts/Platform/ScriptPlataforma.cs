using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptPlataforma : MonoBehaviour
{
    private Vector2 posInicial;

    private float cont;

    public float deslocamento = 1;

    public float raioLargura = 1;

    public float raioAltura = 1;


    // Start is called before the first frame update
    void Start()
    {
        posInicial = transform.position;
        cont = 0;
    }

    // Update is called once per frame
    void Update()
    {
        cont += deslocamento * Time.deltaTime;

        float posX = Mathf.Sin(cont) * raioLargura;
        float posY = Mathf.Cos(cont) * raioAltura;

        transform.position = new Vector2(posInicial.x + posX, posInicial.y + posY);

        if (cont > 2 * Mathf.PI)
        {
            cont = 0;
        }
    }
}
