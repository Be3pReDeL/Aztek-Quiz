using UnityEngine;
using UnityEngine.UI;

public class TutorialScreenController : UIController {
    [SerializeField] private float _duration = 1f;

    [SerializeField] private Image _tutorialText, _tutorialButton;
    [SerializeField] private Sprite[] _tutorialTextSprites, _tutorialButtonSprites;

    [SerializeField] private GameObject _menuScreen;

    private int _counter = 0;

    [OPS.Obfuscator.Attribute.DoNotRename]
    public void OnEnable() {
        for (int i = 0; i < _tweenObjects.Count; i++)
            _tweenObjects[i].Appear(_duration);
    }

    [OPS.Obfuscator.Attribute.DoNotRename]
    public void StartButton() {
        for(int i = 0; i < _tweenObjects.Count; i++)
            _tweenObjects[i].Disappear(_duration);

        _menuScreen.SetActive(true);
    }

    [OPS.Obfuscator.Attribute.DoNotRename]
    public void NextButton(){
        if(_counter < 2) {
            OnEnable();

            _tutorialText.sprite = _tutorialTextSprites[_counter];
            _tutorialButton.sprite = _tutorialButtonSprites[_counter];

            _counter++;
        }
        
        else
            StartButton();
    }
}
