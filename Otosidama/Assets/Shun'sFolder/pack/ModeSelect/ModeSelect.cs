using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ModeSelect : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
    }

    public void OfflineButton()
    {
        SceneManager.LoadScene("Main");
    }

    public void OnlineButton()
    {
        SceneManager.LoadScene("Lobby");
    }

}
