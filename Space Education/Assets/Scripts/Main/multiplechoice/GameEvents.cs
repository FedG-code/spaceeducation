﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "GameEvents", menuName = "Quiz/new GameEvents")]
public class GameEvents : ScriptableObject
{
    public delegate void UpdateQuestionUICallback(Question question);
    public UpdateQuestionUICallback updateQuestionUI;

    public delegate void UpdateQuestionAnswerCallback(AnswerData pickedAnswer);
    public UpdateQuestionAnswerCallback UpdateQuestionAnswer;

    public delegate void DisplayResolutionScreenCallback(UIManager.ResolutionScreenType
        type,int score);
    public DisplayResolutionScreenCallback DisplayResolutionScreen;

    public delegate void ScoreupdatedCallback();
    public ScoreupdatedCallback ScoreUpdated;


    public int CurrentFinalScore;

}