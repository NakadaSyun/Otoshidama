using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Debugtest : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private Text text;
    [SerializeField] private Text text2;
    [SerializeField] private GameObject go;
    bool flg;
    bool flg2;
    void Start()
    {
        flg = go.GetComponent<MainManager>().IsRaid;
        //flg2 = go.GetComponent<MotherMove>().anima.GetBool("Transfer");
    }

    // Update is called once per frame
    void Update()
    {
        flg = go.GetComponent<MainManager>().IsRaid;
        //flg2 = go.GetComponent<MotherMove>().anima.GetBool("Transfer");
        text.text = "IsRaid\t" + flg.ToString();
        //text2.text = "Transfer\t" + flg2.ToString();
    }
}
