using DG.Tweening;
using UnityEngine;
using TMPro;

public class TextTween : MonoBehaviour, ITweanable
{
    [SerializeField] private UIController _UIController;

    private TextMeshProUGUI _text;
    private RectTransform _rectTransform;

    private float _startPosY;

    [SerializeField] private bool _movable = true;
    [SerializeField] private float _finalAlpha = 1f;

    private void Awake()
    {
        _text = GetComponent<TextMeshProUGUI>();
        _rectTransform = GetComponent<RectTransform>();

        _startPosY = _rectTransform.anchoredPosition.y;

        _UIController.AddTweenObjects(this);
    }

    public void Appear(float duration)
    {
        Color tempColor = _text.color;
        tempColor.a = 0f;

        _text.color = tempColor;

        if (_movable)
        {
            _rectTransform.anchoredPosition = _rectTransform.anchoredPosition + Vector2.up * 25f;

            _rectTransform.DOAnchorPosY(_startPosY, duration);
        }

        _text.DOFade(_finalAlpha, duration);
    }

    public void Disappear(float duration)
    {
        _text.DOFade(0f, duration).OnComplete(() =>
        {
            _UIController.gameObject.SetActive(false);
        });
    }
}
