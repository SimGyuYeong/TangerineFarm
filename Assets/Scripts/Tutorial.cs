using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Tutorial : MonoBehaviour
{

    [SerializeField] private Image[] page = null;
    private int pageNum = 0;

    public void OnNextPage()
    {
        page[pageNum].gameObject.SetActive(false);
        pageNum++;
        if(pageNum > 3)
        {
            page[3].gameObject.SetActive(false);
            page[0].gameObject.SetActive(true);
            pageNum = 0;
            GameManager.Instance.UI.helpCanvas.SetActive(false);
            return;
        }
        page[pageNum].gameObject.SetActive(true);
    }
}
