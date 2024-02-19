using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CrashTestScript : MonoBehaviour
{
    [TextArea(minLines: 4, maxLines: 10)]
    public string Readme ="This is the script for crash Test" +
        "Press 'L' for setActive OBI RROPE" +
        "Press 'K' for resetting active scene" +
        "It is crashing after ";
    public GameObject obiParent;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            obiParent.SetActive(true);
        }
        if (Input.GetKeyDown(KeyCode.K))
        {
            ReloadScene();
        }
    }
    public void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
