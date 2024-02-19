using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RespawnTrigger : MonoBehaviour
{

    private Scene activeScene;
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            RestartActiveScene();
        }
    }

    public void RestartActiveScene()
    {
        activeScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(activeScene.name);
    }
}
