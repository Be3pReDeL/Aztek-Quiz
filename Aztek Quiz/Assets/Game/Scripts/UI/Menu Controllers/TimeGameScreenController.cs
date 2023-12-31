using UnityEngine;

public class TimeGameScreenController : UIController {
    [SerializeField] private float _duration = 1f;

    [OPS.Obfuscator.Attribute.DoNotRename]
    public void OnEnable() {
        for (int i = 0; i < _tweenObjects.Count; i++)
            _tweenObjects[i].Appear(_duration);
    }

    [OPS.Obfuscator.Attribute.DoNotRename]
    public void BackButton(){
        for (int i = 0; i < _tweenObjects.Count; i++)
            _tweenObjects[i].Disappear(_duration);
    }

    [OPS.Obfuscator.Attribute.DoNotRename]
    public void SetTimer(int timerTime) {
        PlayerPrefs.SetInt("Timer", timerTime);
        PlayerPrefs.SetString("Mode", "Timer");
    }
}
