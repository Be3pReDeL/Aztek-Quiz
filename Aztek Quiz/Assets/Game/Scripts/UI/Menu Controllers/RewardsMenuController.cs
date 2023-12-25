using UnityEngine;
using UnityEngine.UI;

public class RewardsMenuController : UIController {
    [SerializeField] private float _duration = 1f;

    [SerializeField] private Button[] _rewards;

    [OPS.Obfuscator.Attribute.DoNotRename]
    public void OnEnable() {
        for (int i = 0; i < _tweenObjects.Count; i++)
            _tweenObjects[i].Appear(_duration);

        for(int i = 0; i < _rewards.Length; i++){
            if(PlayerPrefs.GetInt("Reward " + i.ToString(), 0) == 0)
                _rewards[i].interactable = false;
            else
                _rewards[i].interactable = true;
        }
    }

    [OPS.Obfuscator.Attribute.DoNotRename]
    public void BackButton(){
        for (int i = 0; i < _tweenObjects.Count; i++)
            _tweenObjects[i].Disappear(_duration);
    }
}
