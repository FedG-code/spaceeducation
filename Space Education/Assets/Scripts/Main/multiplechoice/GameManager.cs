using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Question[] Questions { get; private set; } = null;

    [SerializeField] GameEvents events = null;


    private List<AnswerData> PickedAnswers = new List<AnswerData>();
    private List<int> FinishedQuestions = new List<int>();
    private int currentQuestion = 0;


    void Start()
    {
        Display();

        LoadQuestions();
        foreach (var question in Questions)
        {
            Debug.Log(question.Info);
        }

    }

    public void EraseAnswers()
    {
        PickedAnswers = new List<AnswerData>();
    }
     
    void Display()
    {
        EraseAnswers();
        var question = GetRandomQuestion();

        if(events.updateQuestionUI != null)
        {
            events.updateQuestionUI(question);
        }
        else { Debug.LogWarning("something went wrong while " +
            "trying to display new Question UI data "); }
    }

    Question GetRandomQuestion()
    {
        var randomIndex = GetRandomQuestionIndex();
        currentQuestion = randomIndex;

        return Questions[currentQuestion];
    }
    int GetRandomQuestionIndex()
    {
        var random = 0;
        if (FinishedQuestions.Count<Questions.Length)
        {
            do
            {
                random = UnityEngine.Random.Range(0, Questions.Length);
            } while (FinishedQuestions.Contains(random) || random == currentQuestion); 
        }
        return random;

    }

    void LoadQuestions()
    {
        // Load all the questions inside the 'Resources'/'Questions' folder
        Object[] objs = Resources.LoadAll("Questions", typeof(Question));
        Questions = new Question[objs.Length];
        for (int i = 0; i< objs.Length; i++)
        {
            Questions[i] = (Question)objs[i];
        }
    }

}
