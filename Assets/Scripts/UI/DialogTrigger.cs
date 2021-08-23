using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogTrigger : MonoBehaviour
{
    public Dialog dialog;

	GameObject buttonStart;
    public void Start()
    {
        buttonStart = GameObject.Find("Button");
    }
    public void TriggerDialog()
	{
		FindObjectOfType<DialogManager>().StartDialog(dialog);
        buttonStart.SetActive(false);

	}

}
