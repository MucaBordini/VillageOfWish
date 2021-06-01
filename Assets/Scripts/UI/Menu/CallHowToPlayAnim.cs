using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CallHowToPlayAnim : MonoBehaviour
{
    public Animator anim;
    public void showHowToPlay()
    {
        anim.SetBool("showHTP", true);
    }

    public void hideHowToPlay()
    {
        anim.SetBool("showHTP", false);
    }
}
