using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChickenFeint : MonoBehaviour
{

    // Start is called before the first frame update

    [SerializeField]private GameObject Chicken;
    void Start()
    {
        PosInit();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(transform.position.z < -2.5f)
        {
            return;
        }
        MovePos();
    }

    void MovePos()
    {
        float sin = Mathf.Sin(Time.time * 5);
        

        //５秒で移動z軸を4～-2まで移動
        transform.position += new Vector3(0, (sin / 100),
                                        (-1.2f * Time.deltaTime));
    }

    public void PosInit()
    {
        transform.position = new Vector3(3.0f, 0.3f, 4.0f);
    }
}
