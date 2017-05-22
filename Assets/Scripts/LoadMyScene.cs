using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadMyScene : MonoBehaviour {

    public enum MyScenes
    {
        E_QUIT = 0
    }

    public MyScenes destinationScene;

    public void LoadScene()
    {
        switch(destinationScene)
        {
            case MyScenes.E_QUIT:
                Application.Quit();
                break;
        }
    }
}
