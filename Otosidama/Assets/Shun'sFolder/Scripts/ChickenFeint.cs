using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChickenFeint : MonoBehaviour
{

    // Start is called before the first frame update

    [SerializeField]private GameObject Chicken;
    float distance = 0.0f;
    Vector3 startPosition, targetPosition;
    void Start()
    {
        PosInit();
        startPosition = Chicken.transform.position;
        targetPosition = MknextPos();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(Chicken.transform.position.z < -2)
        {
            return;
        }
        MovePos();
    }

    void MovePos()
    {
        float sin = Mathf.Sin(Time.time * 5);
        

        //５秒で移動z軸を4～-2まで移動
        Chicken.transform.position += new Vector3(0, (sin / 100),
                                        (-1.2f * Time.deltaTime));
    }

    void PosInit()
    {
        Chicken.transform.position = new Vector3(3.0f, 0.3f, 4.0f);
    }

    Vector3 MknextPos()
    {
        Vector3 pos = Vector3.zero;

        pos = new Vector3(Chicken.transform.position.x,
                            Chicken.transform.position.y,
                            Chicken.transform.position.z - 2.0f);

        return pos;
    }
}
