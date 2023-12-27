using UnityEngine;
using UnityEngine.UI;

public class MenuScreenController : UIController {
    [SerializeField] private float _duration = 1f;

    [SerializeField] private Button _mayansButton;

    private const int MAYANSCATEGORYCOST = 200;

    [OPS.Obfuscator.Attribute.DoNotRename]
    public void OnEnable() {
        for (int i = 0; i < _tweenObjects.Count; i++)
            _tweenObjects[i].Appear(_duration);

        if(PlayerPrefs.GetInt("Mayans", 0) == 1)
            ActivateMayansButton();
    }

    [OPS.Obfuscator.Attribute.DoNotRename]
    public void CloseScreen(){
        for (int i = 0; i < _tweenObjects.Count; i++)
            _tweenObjects[i].Disappear(_duration);
    }

    [OPS.Obfuscator.Attribute.DoNotRename]
    public void BuyMayans(){
        if(PlayerPrefs.GetInt("Coins", 0) >= MAYANSCATEGORYCOST){
            ActivateMayansButton();

            PlayerPrefs.SetInt("Mayans", 1);
        }
    }

    private void ActivateMayansButton(){
        Destroy(_mayansButton.transform.GetChild(0).gameObject);
        _mayansButton.interactable = true;
    }

    public void ChangeGameType(int type){
        PlayerPrefs.SetInt("Current Type", type);
    }
}
