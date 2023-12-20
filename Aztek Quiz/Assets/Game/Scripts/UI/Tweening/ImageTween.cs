using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class ImageTween : MonoBehaviour, ITweanable
{
    [SerializeField] private UIController _UIController;

    private Image _image;
    private RectTransform _rectTransform;

    private float _startPosY;

    [SerializeField] private bool _movable = true;
    [SerializeField] private float _finalAlpha = 1f;

    private void Awake()
    {
        _image = GetComponent<Image>();
        _rectTransform = GetComponent<RectTransform>();

        _startPosY = _rectTransform.anchoredPosition.y;

        _UIController.AddTweenObjects(this);
    }

    public void Appear(float duration)
    {
        Color tempColor = _image.color;
        tempColor.a = 0f;

        _image.color = tempColor;

        if (_movable)
        {
            _rectTransform.anchoredPosition = _rectTransform.anchoredPosition + Vector2.up * 25f;

            _rectTransform.DOAnchorPosY(_startPosY, duration);
        }

        _image.DOFade(_finalAlpha, duration);
    }

    public void Disappear(float duration)
    {
        _image.DOFade(0f, duration).OnComplete(() =>
        {
            _UIController.gameObject.SetActive(false);
        });
    }
}
