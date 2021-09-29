using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Loading : MonoBehaviour
{
    [SerializeField]
    private Slider loadingBar = null;
    [SerializeField]
    private Image padeImage = null;

    private void Start()
    {
        StartCoroutine(SceneLoading());
    }

    IEnumerator SceneLoading()
    {
        yield return null;
        AsyncOperation operation = SceneManager.LoadSceneAsync("Main");
        operation.allowSceneActivation = false;

        while (!operation.isDone)
        {
            yield return null;
            if(loadingBar.value < 1)
            {
                loadingBar.value = Mathf.MoveTowards(loadingBar.value, 150f, Time.deltaTime);
            }
            else
            {
                operation.allowSceneActivation = true;
            }
        }
    }
}
