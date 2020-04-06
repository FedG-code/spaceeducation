using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    Question[] _questions = null;
    public Question[] Questions { get { return _questions; } }
    public HexMap map;


    [SerializeField] GameEvents events = null;

    [SerializeField] Animator timerAnimtor = null;
    [SerializeField] TextMeshProUGUI timerText = null;
    [SerializeField] Color timerHalfWayOutColor = Color.yellow;
    [SerializeField] Color timerAlmostOutColor = Color.red;


    private List<AnswerData> PickedAnswers = new List<AnswerData>();
    private List<int> FinishedQuestions = new List<int>();
    private int currentQuestion = 0;

    private int timerStateParaHash = 0;



    private IEnumerator IE_WaitTillNextRound = null;
    private IEnumerator IE_StartTimer = null;
    private Color timerDefaultColor = Color.white;

    private bool IsFinished
    {
        get
        {  //checked if the number of questions all been completed
            return (FinishedQuestions.Count < Questions.Length) ? false : true;
        }
    }

    void OnEnable()
    {
        events.UpdateQuestionAnswer += UpdateAnswers;
    }
    void OnDisable()
    {
        events.UpdateQuestionAnswer -= UpdateAnswers;
    }


    void Awake()
    {
        events.CurrentFinalScore = 0;
    }

    void Start()
    {
        events.ResearchPoints = PlayerPrefs.GetInt(GameUtility.SaveResearchKey);
        //cache the startup highscore to compare with score after the game is finished
        events.StartupHighscore = PlayerPrefs.GetInt(GameUtility.SavePrefKey);

        timerDefaultColor = timerText.color;


        // instead of loading the question on start we could load the question on update?
        LoadQuestions();


        timerStateParaHash = Animator.StringToHash("TimerState");

        //generate random value
        var seed = UnityEngine.Random.Range(int.MinValue, int.MaxValue);
        UnityEngine.Random.InitState(seed);


        Display();
    }


    public void UpdateAnswers(AnswerData newAnswer)
    {   // check the type of answer, if it's single then we just clear the existing one
        //and update with a new answer
        if (Questions[currentQuestion].GetAnswerType == Question.AnswerType.Single)
        {
            foreach (var answer in PickedAnswers)
            {
                if (answer != newAnswer)
                {
                    answer.Reset();
                }
                PickedAnswers.Clear();
                PickedAnswers.Add(newAnswer);
            }
        }
        else
        {   // check if the answer is already picked, if it exist in the list then it will return false
            bool alreadyPicked = PickedAnswers.Exists(x => x == newAnswer);
            if (alreadyPicked)
            {
                PickedAnswers.Remove(newAnswer);
            }
            else
            {// but if it's not picked then we add the new answer
                PickedAnswers.Add(newAnswer);
            }
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

        if (events.UpdateQuestionUI != null)
        {
            events.UpdateQuestionUI(question);
        }
        else { Debug.LogWarning("something went wrong while " +
            "trying to display new Question UI data "); }

        if (question.useTimer)
        {
            UpdateTimer(question.useTimer);
        }

    }

    //check the answer when the next button is clicked
    public void Accept()
    {   //after the question is answered the timer disappear
        UpdateTimer(false);
        bool isCorrect = CheckAnswers();
        FinishedQuestions.Add(currentQuestion);

        //Update the score, if the answert of time to wait till next question  is incorrect subtract the addscore value
        UpdateScore((isCorrect) ? Questions[currentQuestion].AddScore : -Questions[currentQuestion].AddScore);





        //check if the game is finished
        if (IsFinished)
        {
            SetHighScore();
            SetResearchScore();
        }






        var type = (IsFinished) ? UIManager.ResolutionScreenType.Finish : (isCorrect) ?
            UIManager.ResolutionScreenType.Correct :
            UIManager.ResolutionScreenType.Incorrect;

        if (events.DisplayResolutionScreen != null)
        {
            events.DisplayResolutionScreen(type, Questions[currentQuestion].AddScore);
        }

        if (IE_WaitTillNextRound != null)
        {
            StopCoroutine(IE_WaitTillNextRound);
        }
        IE_WaitTillNextRound = WaitTillNextRound();
        StartCoroutine(IE_WaitTillNextRound);
    }

    //timer
    void UpdateTimer(bool state)
    {
        switch (state)
        {
            case true:
                IE_StartTimer = StartTimer();
                StartCoroutine(IE_StartTimer);

                timerAnimtor.SetInteger(timerStateParaHash, 2);
                break;

            case false:
                if (IE_StartTimer != null)
                {
                    StopCoroutine(IE_StartTimer);
                }
                timerAnimtor.SetInteger(timerStateParaHash, 1);
                break;
        }
    }

    IEnumerator StartTimer()
    {
        var totalTime = Questions[currentQuestion].Timer;
        var timeLeft = totalTime;

        timerText.color = timerDefaultColor;
        while (timeLeft > 0)
        {
            timeLeft--;

            //change timer color depending on how much time is left
            if (timeLeft < totalTime / 2 && timeLeft < totalTime / 4)
            {
                timerText.color = timerHalfWayOutColor;
            }
            if (timeLeft < totalTime / 4)
            {
                timerText.color = timerAlmostOutColor;
            }

            timerText.text = timeLeft.ToString();
            yield return new WaitForSeconds(1.0f);
        }
        Accept();
    }


    IEnumerator WaitTillNextRound()
    {   // the amount of time to wait till next question
        yield return new WaitForSeconds(GameUtility.ResolutionDelayTime);
        //display a new question
        Display();
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
        if (FinishedQuestions.Count < Questions.Length)
        {
            do
            {
                random = UnityEngine.Random.Range(0, Questions.Length);
            } while (FinishedQuestions.Contains(random) || random == currentQuestion);
        }
        return random;

    }

    bool CheckAnswers()
    {
        if (!CompareAnswers())
        {
            return false;
        }
        return true;
    }
    bool CompareAnswers()
    {//check if at least one correct answers are picked
        if (PickedAnswers.Count > 0)
        { // list of correct answers
            List<int> c = Questions[currentQuestion].GetCorrectAnswers();
            // list of picked answers, select the picked answer and put into a list
            List<int> p = PickedAnswers.Select(x => x.AnswerIndex).ToList();

            // .Except() removes all the elements except the ones in p
            var f = c.Except(p).ToList();
            // removes all the elements that can be found in the correct list c
            var s = p.Except(c).ToList();

            // if f and s contains elements then it return false
            // if both lists don't contain any element it returns true
            return !f.Any() && !s.Any();
        }
        // automatically false if no correct answer is picked
        return false;
    }


    //we could make another void loadquestions for different topics, and call the function
    //when we need to?
    void LoadQuestions()
    {
        // Load all the questions inside the 'Resources'/'Questions' folder
        Object[] objs = Resources.LoadAll("Questions", typeof(Question));
        _questions = new Question[objs.Length];
        for (int i = 0; i < objs.Length; i++)
        {
            _questions[i] = (Question)objs[i];
        }
    }




    // HighScore
    private void SetHighScore()
    {

        var highscore = PlayerPrefs.GetInt(GameUtility.SavePrefKey);

        // check if the highscore is less than the current game score
        if (highscore < events.CurrentFinalScore)
        {   // save the CurrentFinalScore as highscore
            PlayerPrefs.SetInt(GameUtility.SavePrefKey, events.CurrentFinalScore);

        }
    }

    // Research Points (add final score to research score)
    private void SetResearchScore()
    {
        PlayerPrefs.SetInt(GameUtility.SaveResearchKey, events.CurrentFinalScore + events.ResearchPoints);
    }


    private void UpdateScore(int add)
    {
        events.CurrentFinalScore += add;

        if (events.ScoreUpdated != null)
        {
            events.ScoreUpdated();
        }
    }

}
