using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TransicaoController : MonoBehaviour
{

    public int sceneNumber;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(WaitTransition());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator WaitTransition()
    {

        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene(sceneNumber);
    }
}
