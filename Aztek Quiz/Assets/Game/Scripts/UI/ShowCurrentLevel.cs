using UnityEngine;
using TMPro;

public class ShowCurrentLevel : MonoBehaviour
{
    private TextMeshProUGUI _text;

    private void Awake(){
        _text = GetComponent<TextMeshProUGUI>();
    }

    private void Start(){
        _text.text = "Level " + PlayerPrefs.GetInt("Current Level", 1).ToString();
    }
}
