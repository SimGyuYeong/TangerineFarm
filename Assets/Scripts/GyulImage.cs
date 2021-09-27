using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class GyulImage : MonoBehaviour
{
    private Text gyulText = null;
    [SerializeField]
    private Transform gyulTextPosition = null;

    public void Show(Vector2 mousePosition)
    {
        gyulText = GetComponent<Text>();
        gameObject.SetActive(true);

        gyulText.text = string.Format("+{0}", GameManager.Instance.UI.GetUnit(GameManager.Instance.CurrentUser.mouseGpC));
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
