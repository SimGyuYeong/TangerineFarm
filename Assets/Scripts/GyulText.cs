using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class GyulText : MonoBehaviour
{
    private Text gyulText = null;
    [SerializeField]
    private Transform gyulTextPosition = null;

    public void Show(Vector2 mousePosition, int check, long addGyul)
    {
        gyulText = GetComponent<Text>();
        gameObject.SetActive(true);

        gyulText.text = string.Format("+{0}", GameManager.Instance.UI.GetUnit(addGyul));
        switch (check)
        {
            case 1:
                gyulText.color = Color.white;
                break;
            case 2:
                gyulText.color = Color.yellow;
                break;
        }
        transform.position = gyulTextPosition.transform.position;
        transform.position = new Vector3(transform.position.x, transform.position.y, 0f);

        RectTransform rectTransform = GetComponent<RectTransform>();
        float targetPositionY = rectTransform.anchoredPosition.y + 150f;

        rectTransform.DOAnchorPosY(targetPositionY, 0.5f);
        gyulText.DOFade(0f, 0.5f).OnComplete(() => Despawn());
    }

    private void Despawn()
    {
        gyulText.DOFade(1f, 0f);
        gameObject.SetActive(false);
        transform.SetParent(GameManager.Instance.Pool);
    }
}
