using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    public string sceneName;

    // Update is called once per frame
    public void changeScene()
    {
        SceneManager.LoadSceneAsync(sceneName);
    }
}
