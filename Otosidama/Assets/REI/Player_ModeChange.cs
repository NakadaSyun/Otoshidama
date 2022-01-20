using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_ModeChange : MonoBehaviour
{
    Animator animator;
    public void OnClick()
    {
        //if (animator.ModeChange == false)
        //{
        //}
    animator.SetBool("ModeChange", false);
    }
}
