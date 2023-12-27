using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.IO;
using System.Linq;

[System.Serializable]
public class QuizQuestion {
    public string question;
    public string[] options;
    public string correct_answer;
}

[System.Serializable]
public class QuizQuestions {
    public QuizQuestion[] questions;
}

public enum GameMode {
    Campaign,
    Timer,
    Multiplayer
}

public class QuizGame : MonoBehaviour {
    [SerializeField] private TextMeshProUGUI _questionText, _playerNameText, _timerText, _questionPanelText, _levelText;
    [SerializeField] private List<Button> _answerButtons;
    [SerializeField] private Button _hintButton;
    [SerializeField] private GameObject _timer, _notEnoughMoneyPlane;
    [SerializeField] private GameEndScreen _gameEndScreen;

    private List<QuizQuestion> _quizQuestions;
    private List<QuizQuestion> _selectedQuestions;
    private int _currentQuestionIndex = 0;
    private int _correctAnswerIndex;
    private int _score = 0;

    private GameMode _currentGameMode;
    private string _player1Name, _player2Name;
    private int _player1Score, _player2Score;
    private int _currentPlayer; // 0 для первого игрока, 1 для второго
    private float _timeRemaining;

    private int _player1QuestionsAnswered, _player2QuestionsAnswered;

    private void Start() {
        DetermineGameMode();
        InitializePlayers();
        LoadQuestionsFromJSON();
        _selectedQuestions = ChooseRandomQuestions(10);
        SetQuestion(_selectedQuestions[_currentQuestionIndex]);
        _hintButton.onClick.AddListener(OnHintButtonClicked);

        _player1QuestionsAnswered = 0;
        _player2QuestionsAnswered = 0;
    }

    private void Update() {
        if (_currentGameMode == GameMode.Timer || _currentGameMode == GameMode.Multiplayer) {
            HandleTimer();
        }
    }

    private void DetermineGameMode() {
        string mode = PlayerPrefs.GetString("Mode", "Campaign");
        switch (mode) {
            case "Campaign":
                _currentGameMode = GameMode.Campaign;
                break;
            case "Timer":
                _currentGameMode = GameMode.Timer;
                _timeRemaining = PlayerPrefs.GetInt("Timer", 30);
                _timer.SetActive(true);
                _levelText.text = "Timer Game";
                PlayerPrefs.SetInt("Current Level", -1);
                break;
            case "Multiplayer":
                _currentGameMode = GameMode.Multiplayer;
                _currentPlayer = 0;
                _timeRemaining = PlayerPrefs.GetInt("Timer", 30);
                _timer.SetActive(true);
                _playerNameText.gameObject.SetActive(true);
                _levelText.text = "Multiplayer Game";
                PlayerPrefs.SetInt("Current Level", -1);
                break;
        }
    }

    private void InitializePlayers() {
        if (_currentGameMode == GameMode.Multiplayer) {
            _player1Name = PlayerPrefs.GetString("Player 1", "Player 1");
            _player2Name = PlayerPrefs.GetString("Player 2", "Player 2");
            UpdatePlayerNameDisplay();
        }
    }

    private void UpdatePlayerNameDisplay() {
        if (_currentGameMode == GameMode.Multiplayer) {
            _playerNameText.text = _currentPlayer == 0 ? _player1Name : _player2Name;
        }
    }

    private void HandleTimer() {
        if (_timeRemaining > 0) {
            _timeRemaining -= Time.deltaTime;
            _timerText.text = Mathf.Round(_timeRemaining).ToString();
        } else {
            GoToNextQuestion();
        }
    }

    private void GoToNextQuestion() {
        if (_currentGameMode == GameMode.Multiplayer) {
            if (_currentPlayer == 0) {
                _player1QuestionsAnswered++;
                if (_player1QuestionsAnswered >= 10) {
                    // Первый игрок ответил на все вопросы, переключаемся на второго игрока
                    _currentPlayer = 1;
                    _currentQuestionIndex = 0; // Сбросить индекс вопроса для второго игрока
                    UpdatePlayerNameDisplay();
                    _timeRemaining = PlayerPrefs.GetInt("Timer", 30);
                    return; // Важно предотвратить увеличение _currentQuestionIndex в этом ходу
                }
            } else {
                _player2QuestionsAnswered++;
                if (_player2QuestionsAnswered >= 10) {
                    // Второй игрок ответил на все вопросы, игра завершается
                    EndGame();
                    return;
                }
            }
        }

        _currentQuestionIndex++;
        if (_currentQuestionIndex < _selectedQuestions.Count) {
            SetQuestion(_selectedQuestions[_currentQuestionIndex]);
        } else if (_currentGameMode != GameMode.Multiplayer) {
            EndGame();
        }
    }

    private void EndGame() {
        Debug.Log("Квиз завершен");
        _gameEndScreen.gameObject.SetActive(true);
        _gameEndScreen.SetUpEndText(_score);

        if (_currentGameMode == GameMode.Multiplayer) {
            PlayerPrefs.SetInt("Player 1 Score", _player1Score);
            PlayerPrefs.SetInt("Player 2 Score", _player2Score);

            _gameEndScreen.SetUpMultiplayerScreenText();
        }
    }

    private void LoadQuestionsFromJSON() {
        int currentType = PlayerPrefs.GetInt("Current Type", 0);
        string fileName = "questions_aztec.json";

        if (currentType == 0) {
            fileName = "questions_aztec.json";
        } else if (currentType == 1) {
            fileName = "questions_mayan.json";
        }

        string filePath = Path.Combine(Application.streamingAssetsPath, fileName);
        if (File.Exists(filePath)) {
            string dataAsJson = File.ReadAllText(filePath);
            QuizQuestions loadedData = JsonUtility.FromJson<QuizQuestions>(dataAsJson);
            _quizQuestions = new List<QuizQuestion>(loadedData.questions);
        } else {
            Debug.LogError("Cannot find file: " + filePath);
        }
    }

    private List<QuizQuestion> ChooseRandomQuestions(int count) {
        return _quizQuestions.OrderBy(x => Random.value).Take(count).ToList();
    }

    private void SetQuestion(QuizQuestion quizQuestion) {
        _questionText.text = quizQuestion.question;
        _questionPanelText.text = "Question " + (_currentQuestionIndex + 1).ToString() + "/10";
        ShuffleAnswers(quizQuestion);

        for (int i = 0; i < _answerButtons.Count; i++) {
            if (i < quizQuestion.options.Length) {
                _answerButtons[i].gameObject.SetActive(true);
                _answerButtons[i].GetComponentInChildren<TextMeshProUGUI>().text = quizQuestion.options[i];
                _answerButtons[i].onClick.RemoveAllListeners();

                int buttonIndex = i; // Создаём временную переменную для правильного захвата индекса
                _answerButtons[i].onClick.AddListener(() => AnswerSelected(buttonIndex));

                if (quizQuestion.options[i] == quizQuestion.correct_answer) {
                    _correctAnswerIndex = i;
                }
            } else {
                _answerButtons[i].gameObject.SetActive(false);
            }
        }
    }

    private void ShuffleAnswers(QuizQuestion question) {
        int n = question.options.Length;
        while (n > 1) {
            n--;
            int k = Random.Range(0, n + 1);
            string value = question.options[k];
            question.options[k] = question.options[n];
            question.options[n] = value;
        }
    }

    public void AnswerSelected(int index) {
        if (_currentGameMode == GameMode.Multiplayer){
            if (index == _correctAnswerIndex) {
                _score++;

                if(_currentPlayer == 0)
                    _player1Score += 1;
                else
                    _player2Score += 1;

            } else {
                Debug.Log("Неправильный ответ.");
            }
            
            GoToNextQuestion();
        }
        else{
            if (index == _correctAnswerIndex) {
                _score++;
            } else {
                Debug.Log("Неправильный ответ.");
            }

            _timeRemaining = PlayerPrefs.GetInt("Timer", 30);
            StartCoroutine(WaitAndSetNextQuestion());
        }
    }

    private void OnHintButtonClicked() {
        int coins = PlayerPrefs.GetInt("Coins", 0);
        if (coins >= 50) {
            StartCoroutine(ShowCorrectAnswerHint());
            PlayerPrefs.SetInt("Coins", coins - 50); // Уменьшаем количество монет
        } else {
            _notEnoughMoneyPlane.SetActive(true);
        }
    }

    private IEnumerator WaitAndSetNextQuestion() {
        yield return new WaitForSeconds(1.5f);
        _currentQuestionIndex++;
        if (_currentQuestionIndex < _selectedQuestions.Count) {
            SetQuestion(_selectedQuestions[_currentQuestionIndex]);
        } else {
            Debug.Log("Квиз завершен");
            _gameEndScreen.gameObject.SetActive(true);
            _gameEndScreen.SetUpEndText(_score);
        }
    }

    private IEnumerator ShowCorrectAnswerHint() {
        Button correctButton = _answerButtons[_correctAnswerIndex];
        Color originalColor = correctButton.image.color;
        correctButton.image.color = Color.green;

        yield return new WaitForSeconds(2);

        correctButton.image.color = originalColor;
    }
}
