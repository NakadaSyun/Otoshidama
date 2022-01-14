using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImageChange : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] GameObject[] obj;
    float time;
    [SerializeField]Slider slider;

    void Start()
    {
        obj[0].SetActive(true);
        time = 0.0f;
        slider.value = 1.0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            obj[0].SetActive(false);
            obj[1].SetActive(true);
        }
        else
        {
            obj[0].SetActive(true);
            obj[1].SetActive(false);
        }

        if (obj[0].activeSelf)
        {
            time += Time.deltaTime;
        }

        if(time > 10.0f)
        {
            obj[0].SetActive(false);
            obj[1].SetActive(false);
            obj[2].SetActive(true);
        }

        slider.value = 1.0f - (time / 10.0f);
    }
}
