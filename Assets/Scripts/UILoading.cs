using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UILoading : MonoBehaviour
{
    void Start()
    {
        StartCoroutine(changeScene());
    }

    IEnumerator changeScene()
    {
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene(1);
    }
}
