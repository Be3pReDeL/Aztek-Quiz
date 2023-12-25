using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class MultiplayerScreenController : UIController {
    [SerializeField] private float _duration = 1f;

    [SerializeField] private TMP_InputField _player1InputField, _player2InputField;
    [SerializeField] private GameObject _playerInputFieldScreen, _timerScreen;

    [SerializeField] private ObjectTween[] _timersMenu;

    [OPS.Obfuscator.Attribute.DoNotRename]
    public void OnEnable() {
        for (int i = 0; i < _tweenObjects.Count; i++)
            _tweenObjects[i].Appear(_duration);
    }

    [OPS.Obfuscator.Attribute.DoNotRename]
    public void CloseScreen(){
        for (int i = 0; i < _tweenObjects.Count; i++)
            _tweenObjects[i].Disappear(_duration);
    }

    [OPS.Obfuscator.Attribute.DoNotRename]
    public void RedactingEnded(){
        if(_player1InputField.text != string.Empty && _player2InputField.text != string.Empty) {
            PlayerPrefs.SetString("Player 1", _player1InputField.text);
            PlayerPrefs.SetString("Player 2", _player2InputField.text);

            _playerInputFieldScreen.SetActive(false);
            _timerScreen.SetActive(true);

            for(int i = 0; i < _timersMenu.Length; i++)
                _timersMenu[i].Appear(0.3f);
        }
    }

    [OPS.Obfuscator.Attribute.DoNotRename]
    public void SetTimer(int timerTime) {
        PlayerPrefs.SetInt("Timer", timerTime);
        PlayerPrefs.SetString("Mode", "Multiplayer");
    }
}
