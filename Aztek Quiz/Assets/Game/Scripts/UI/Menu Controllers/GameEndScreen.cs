using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameEndScreen : UIController {
    [SerializeField] private float _duration = 1f;

    [SerializeField] private TextMeshProUGUI _scoreText, _earnedText, _player1Name, _player2Name, _winPlayerName;
    [SerializeField] private Image _stars, _winLooseText, _masks;
    [SerializeField] private GameObject _reloadButton, _nextLevelButton, _winSingleScreen, _winMultiplayerScreen;
    [SerializeField] private Sprite[] _starsSprites;
    [SerializeField] private Sprite _winText, _looseText, _winMasks, _looseMasks;

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
    public void SetUpEndText(int score){
        _scoreText.text = "Your Score: " + score.ToString() + "/" + "10";

        switch(score){
            case 7:
                _earnedText.text = "You Earned: " + 7.ToString();

                _stars.sprite = _starsSprites[1];

                Win(1, PlayerPrefs.GetInt("Current Level", 0));

                PlayerPrefs.SetInt("Coins", PlayerPrefs.GetInt("Coins") + 7);
            break;

            case 8:
                _earnedText.text = "You Earned: " + 8.ToString();

                _stars.sprite = _starsSprites[2];

                Win(2, PlayerPrefs.GetInt("Current Level", 0));

                PlayerPrefs.SetInt("Coins", PlayerPrefs.GetInt("Coins") + 8);
            break;

            case 9:
                _earnedText.text = "You Earned: " + 9.ToString();

                _stars.sprite = _starsSprites[2];

                Win(2, PlayerPrefs.GetInt("Current Level", 0));

                PlayerPrefs.SetInt("Coins", PlayerPrefs.GetInt("Coins") + 9);
            break;                

            case 10:
                _earnedText.text = "You Earned: " + 10.ToString();

                _stars.sprite = _starsSprites[3];

                Win(3, PlayerPrefs.GetInt("Current Level", 0));

                PlayerPrefs.SetInt("Coins", PlayerPrefs.GetInt("Coins") + 10);
            break;

            default:
                _earnedText.gameObject.SetActive(false);

                _stars.sprite = _starsSprites[0];
                _winLooseText.sprite = _looseText;
                _masks.sprite = _looseMasks;

                _nextLevelButton.SetActive(false);
                _reloadButton.SetActive(true);
            break;
        }
    }

    private void Win(int stars, int level){
        if(PlayerPrefs.GetInt("Current Type", 0) == 0) {
            PlayerPrefs.SetInt("Level Aztec " + level.ToString(), 1);
            PlayerPrefs.SetInt("Level Aztec " + (level + 1).ToString(), 1);

            PlayerPrefs.SetInt("Stars Aztec " + level.ToString(), stars);

            if(PlayerPrefs.GetInt("Level Aztec " + 1.ToString(), 0) == 1){
                PlayerPrefs.SetInt("Show Reward", 1);
                PlayerPrefs.GetInt("Current Reward", 0);
            }
            else if(PlayerPrefs.GetInt("Level Aztec " + 5.ToString(), 0) == 1){
                PlayerPrefs.SetInt("Show Reward", 1);
                PlayerPrefs.GetInt("Current Reward", 1);
            }
            else if(PlayerPrefs.GetInt("Level Aztec " + 9.ToString(), 0) == 1){
                PlayerPrefs.SetInt("Show Reward", 1);
                PlayerPrefs.GetInt("Current Reward", 4);
            }
            else if(PlayerPrefs.GetInt("Level Aztec " + 12.ToString(), 0) == 1){
                PlayerPrefs.SetInt("Show Reward", 1);
                PlayerPrefs.GetInt("Current Reward", 5);
            }
            else if(PlayerPrefs.GetInt("Level Aztec " + 18.ToString(), 0) == 1){
                PlayerPrefs.SetInt("Show Reward", 1);
                PlayerPrefs.GetInt("Current Reward", 8);
            }

            Debug.Log("AZTEC NEXT LEVEL");
        } else{
            PlayerPrefs.SetInt("Level Mayan " + level.ToString(), 1);
            PlayerPrefs.SetInt("Level Mayan " + (level + 1).ToString(), 1);

            PlayerPrefs.SetInt("Stars Mayan " + level.ToString(), stars);

            if(PlayerPrefs.GetInt("Level Mayan " + 3.ToString(), 0) == 1){
                PlayerPrefs.SetInt("Show Reward", 1);
                PlayerPrefs.GetInt("Current Reward", 2);
            }
            else if(PlayerPrefs.GetInt("Level Mayan " + 7.ToString(), 0) == 1){
                PlayerPrefs.SetInt("Show Reward", 1);
                PlayerPrefs.GetInt("Current Reward", 3);
            }
            else if(PlayerPrefs.GetInt("Level Mayan " + 14.ToString(), 0) == 1){
                PlayerPrefs.SetInt("Show Reward", 1);
                PlayerPrefs.GetInt("Current Reward", 6);
            }
            else if(PlayerPrefs.GetInt("Level Mayan " + 16.ToString(), 0) == 1){
                PlayerPrefs.SetInt("Show Reward", 1);
                PlayerPrefs.GetInt("Current Reward", 7);
            }

            Debug.Log("MAYAN NEXT LEVEL");
        }

        PlayerPrefs.Save();
    }

    [OPS.Obfuscator.Attribute.DoNotRename]
    public void SetUpMultiplayerScreenText() {
        _winSingleScreen.SetActive(false);
        _winMultiplayerScreen.SetActive(true);

        int player1Score = PlayerPrefs.GetInt("Player 1 Score", 0);
        int player2Score = PlayerPrefs.GetInt("Player 2 Score", 0);

        _player1Name.text = PlayerPrefs.GetString("Player 1", "Player 1") + " Score " + player1Score.ToString() + "/10";
        _player2Name.text = PlayerPrefs.GetString("Player 2", "Player 2") + " Score " + player2Score.ToString() + "/10";

        if(player1Score > player2Score)
            _winPlayerName.text = PlayerPrefs.GetString("Player 1", "Player 1") + " Wins";
        else
            _winPlayerName.text = PlayerPrefs.GetString("Player 2", "Player 2") + " Wins"; 

    }
}
