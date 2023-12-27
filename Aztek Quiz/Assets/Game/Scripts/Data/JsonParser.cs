using System.Collections.Generic;
using UnityEngine;
using System.IO;

[System.Serializable]
public class Question
{
    public string question;
    public string[] options;
    public int correctAnswer;
}

public class JsonParser : MonoBehaviour {
    [SerializeField] private string jsonFileNameAztec, jsonFileNameMayan;
    private List<Question> questions;

    private void Start() {
        questions = LoadQuestions();
    }

    [OPS.Obfuscator.Attribute.DoNotRename]
    public List<Question> LoadQuestions() {
        List<Question> questions = new List<Question>();

        string jsonFileName = PlayerPrefs.GetInt("Current Type", 0) == 0 ? jsonFileNameAztec : jsonFileNameMayan;
        string jsonFilePath = Path.Combine(Application.streamingAssetsPath, jsonFileName + ".json");

        if (File.Exists(jsonFilePath)) {
            string jsonText = File.ReadAllText(jsonFilePath);
            questions = JsonUtility.FromJson<QuestionsData>(jsonText).questions;
        }

        else
            Debug.LogError("JSON file not found: " + jsonFilePath);

        return questions;
    }

    [OPS.Obfuscator.Attribute.DoNotRename]
    public Question GetQuestion(int index) {
        if (index >= 0 && index < questions.Count)
            return questions[index];
            
        else {
            Debug.LogError("Question index out of range: " + index);
            return null;
        }
    }

    [OPS.Obfuscator.Attribute.DoNotRename]
    public string[] GetOptions(int questionIndex) {
        Question question = GetQuestion(questionIndex);
        return question != null ? question.options : null;
    }

    [OPS.Obfuscator.Attribute.DoNotRename]
    public int GetCorrectAnswerIndex(int questionIndex) {
        Question question = GetQuestion(questionIndex);
        return question != null ? question.correctAnswer : -1;
    }

    [System.Serializable]
    private class QuestionsData {
        public List<Question> questions;
    }
}
