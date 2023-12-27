using System;
using System.Collections;
using UnityEngine;
using UnityEngine.iOS;
using UnityEngine.Networking;
using UnityEngine.UI;
using UniWebViewNamespace;

public class OptionsScreenController : UIController {
    [SerializeField] private float _duration = 1f;

    [SerializeField] private GameObject _privacyPolicyWebView;
    [SerializeField] private string _privacyPolicyURL;

    [SerializeField] private string _appID;

    [SerializeField] private Button _musicButton, _vibrationButton;
    [SerializeField] private Sprite _toogleOn, _toogleOff;

    [OPS.Obfuscator.Attribute.DoNotRename]
    public void OnEnable() {
        for (int i = 0; i < _tweenObjects.Count; i++)
            _tweenObjects[i].Appear(_duration);

        _musicButton.image.sprite = PlayerPrefs.GetInt("Music", 0) == 0 ? _toogleOn : _toogleOff;
        _vibrationButton.image.sprite = PlayerPrefs.GetInt("Vibration", 1) == 0 ? _toogleOn : _toogleOff;
        AudioListener.volume = 1 - PlayerPrefs.GetInt("Music", 0);
    }

    [OPS.Obfuscator.Attribute.DoNotRename]
    public void BackButton() {
        for (int i = 0; i < _tweenObjects.Count; i++)
            _tweenObjects[i].Disappear(_duration);
    }

    [OPS.Obfuscator.Attribute.DoNotRename]
    public void ToogleMusic() {
        PlayerPrefs.SetInt("Music", Convert.ToInt32(!Convert.ToBoolean(PlayerPrefs.GetInt("Music", 0))));

        _musicButton.image.sprite = PlayerPrefs.GetInt("Music", 0) == 0 ? _toogleOn : _toogleOff;

        AudioListener.volume = 1 - PlayerPrefs.GetInt("Music", 0);
    }

    [OPS.Obfuscator.Attribute.DoNotRename]
    public void ToogleVibration() {
        PlayerPrefs.SetInt("Vibration", Convert.ToInt32(!Convert.ToBoolean(PlayerPrefs.GetInt("Vibration", 1))));

        _vibrationButton.image.sprite = PlayerPrefs.GetInt("Vibration", 1) == 0 ? _toogleOn : _toogleOff;
    }

    [OPS.Obfuscator.Attribute.DoNotRename]
    public void ShareButton(){
        new NativeShare()
			.SetSubject("Aztec Civilizations!").SetText("Check this cool Aztec Civilizations game!").SetUrl("https://apps.apple.com/us/app/id" + _appID)
			.SetCallback((result, shareTarget) => Debug.Log("Share result: " + result + ", selected app: " + shareTarget))
			.Share();
    }

    [OPS.Obfuscator.Attribute.DoNotRename]
    public void RateUs(){
        Device.RequestStoreReview();
    }

    [OPS.Obfuscator.Attribute.DoNotRename]
    public void PrivacyPolicy(){
        GameObject webView = Instantiate(_privacyPolicyWebView);

        UniWebView uniWebView = webView.GetComponent<UniWebView>();

        StartCoroutine(GetText(uniWebView));
    }

    [OPS.Obfuscator.Attribute.DoNotRename]
    private IEnumerator GetText(UniWebView uniWebView) {
        UnityWebRequest www = UnityWebRequest.Get(_privacyPolicyURL);
        yield return www.SendWebRequest();
 
        if (www.result != UnityWebRequest.Result.Success)
            Debug.Log(www.error);

        else {
            string pageText = www.downloadHandler.text.Replace("\"", "");

            uniWebView.Show();
            uniWebView.Load(pageText);
        }
    }
}

