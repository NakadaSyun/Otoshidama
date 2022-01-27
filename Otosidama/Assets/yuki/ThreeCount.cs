using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ThreeCount : MonoBehaviour
{
    [SerializeField] private GameObject CountBox;
    [SerializeField] private Text countText;

    public bool StartFlg;

    private void Start()
    {
        StartFlg = false;
    }

    public IEnumerator Count()
    {
        StartFlg = true;
        countText.text = "3";
        yield return new WaitForSecondsRealtime(1.0f);
        countText.text = "2";
        yield return new WaitForSecondsRealtime(1.0f);
        countText.text = "1";
        yield return new WaitForSecondsRealtime(1.0f);
        countText.text = "スタート";
        yield return new WaitForSecondsRealtime(0.5f);
        CountBox.SetActive(false);
        Time.timeScale = 1;
    }
}
