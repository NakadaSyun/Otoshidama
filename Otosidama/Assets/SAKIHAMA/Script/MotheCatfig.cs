using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MotheCatfig: MonoBehaviour
{
    public GameObject Mother;   //母親のオブジェクト

    public void OpenDoorMove()
    {
        Mother.SetActive(true);
    }

    public void CloseDoorMove()
    {
        Mother.SetActive(false);
    }
}
