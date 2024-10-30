using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class End_Script : MonoBehaviour
{
    public void play2()
    {
        SceneManager.LoadSceneAsync(1);
        Debug.Log("pressed");
    }
}