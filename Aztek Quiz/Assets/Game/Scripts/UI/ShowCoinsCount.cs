using UnityEngine;
using TMPro;

public class ShowCoinsCount : MonoBehaviour
{
    private TextMeshProUGUI _text;

    private void Awake(){
        _text = GetComponent<TextMeshProUGUI>();
    }

    private void Start(){
        _text.text = PlayerPrefs.GetInt("Coins", 0).ToString();
    }
}
