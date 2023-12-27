using UnityEngine;
using UnityEngine.UI;

public class ShowReward : MonoBehaviour {
    [SerializeField] private Sprite[] _rewardIconSprites;
    [SerializeField] private Image _rewardIcon;

    [SerializeField] private GameObject _rewardPopUp;

    private void Start(){
        if(PlayerPrefs.GetInt("Show Reward", 0) == 1){
            _rewardPopUp.SetActive(true);

            _rewardIcon.sprite = _rewardIconSprites[PlayerPrefs.GetInt("Current Reward", 0)];

            PlayerPrefs.SetInt("Reward " + PlayerPrefs.GetInt("Current Reward", 0), 1);
            PlayerPrefs.SetInt("Show Reward", 0);
        }
    }
}
