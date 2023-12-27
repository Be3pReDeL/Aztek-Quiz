using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class LevelButton : MonoBehaviour {
    [SerializeField] private Image _starsIcon;
    [SerializeField] private Sprite[] _starsSprites;

    [SerializeField] private GameObject _lockedLevel;    
    [SerializeField] private TextMeshProUGUI _levelButtonText;
    [SerializeField] private int _levelIndex;

    public enum LevelType {
        aztec,
        mayan
    };

    public LevelType ThisLevelType = LevelType.aztec;

    private void Start() {
        int starsCount;

        switch (ThisLevelType){
            case LevelType.aztec:
                if(PlayerPrefs.GetInt("Level Aztec " + _levelIndex.ToString(), 0) == 0) {
                    _lockedLevel.SetActive(true);

                    GetComponent<Button>().interactable = false;

                    _starsIcon.gameObject.SetActive(false);
                } else{
                    _lockedLevel.SetActive(false);

                    GetComponent<Button>().interactable = true;

                    starsCount = PlayerPrefs.GetInt("Stars Aztec " + _levelIndex.ToString(), 0);

                    if(starsCount == 0)
                        _starsIcon.gameObject.SetActive(false);
                    
                    else 
                        _starsIcon.sprite = _starsSprites[starsCount - 1];
                }
            break;

            case LevelType.mayan:
                if(PlayerPrefs.GetInt("Level Mayan " + _levelIndex.ToString(), 0) == 0) {
                    _lockedLevel.SetActive(true);

                    GetComponent<Button>().interactable = false;

                    _starsIcon.gameObject.SetActive(false);
                } else{
                    _lockedLevel.SetActive(false);

                    GetComponent<Button>().interactable = true;
                    
                    starsCount = PlayerPrefs.GetInt("Stars Mayan " + _levelIndex.ToString(), 0);

                    if(starsCount == 0)
                        _starsIcon.gameObject.SetActive(false);
                    
                    else 
                        _starsIcon.sprite = _starsSprites[starsCount - 1];
                }
            break;

            default:
                goto case LevelType.aztec;
        }

        _levelButtonText.text = _levelIndex.ToString();
    }

    private void Awake() {
        PlayerPrefs.SetInt("Level Aztec 1", 1);
        PlayerPrefs.SetInt("Level Mayan 1", 1);
    }

    [OPS.Obfuscator.Attribute.DoNotRename]
    public void LoadLevel() {
        PlayerPrefs.SetInt("Current Level", _levelIndex);
        PlayerPrefs.SetInt("Current Type", (int) ThisLevelType);
        PlayerPrefs.SetString("Mode", "Campaign");

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
