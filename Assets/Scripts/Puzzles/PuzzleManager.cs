using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PuzzleManager : MonoBehaviour
{
    public Text questionText;
    public Text answer1Text;
    public Text answer2Text;
    public Text answer3Text;
    public Text answer4Text;

    public Button btn1;
    public Button btn2;
    public Button btn3;
    public Button btn4;

    public Animator animator;

    private Queue<string> questions;
    private Queue<string> answers1queue;
    private Queue<string> answers2queue;
    private Queue<string> answers3queue;
    private Queue<string> answers4queue;
    private Queue<int> rightAnswerqueue;

    private GameObject enemy;

    void WrongAnwserResponse()
    {
        Debug.Log("ERROU!");
    }
    void RightAnwserResponse()
    {
        Debug.Log("ACERTOU!");
        DisplayNextQuestion();
    }

    void RemoveAllButtonListeners()
    {
        btn1.onClick.RemoveAllListeners();
        btn2.onClick.RemoveAllListeners();
        btn3.onClick.RemoveAllListeners();
        btn4.onClick.RemoveAllListeners();
    }

    void Start()
    {
        questions = new Queue<string>();
        answers1queue = new Queue<string>();
        answers2queue = new Queue<string>();
        answers3queue = new Queue<string>();
        answers4queue = new Queue<string>();
        rightAnswerqueue = new Queue<int>();

    }

    public void StartPuzzle (Puzzle puzzle, Answers answers, GameObject enemy)
    {
        this.enemy = enemy;

        StartCoroutine(WaitAnimation());

        animator.SetBool("isOpen", true);

        questions.Clear();
        answers1queue.Clear();
        answers2queue.Clear();
        answers3queue.Clear();
        answers4queue.Clear();
        rightAnswerqueue.Clear();

        foreach (string question in puzzle.questions)
        {
            questions.Enqueue(question);
        }

        foreach(string answer in answers.answer1)
        {
            answers1queue.Enqueue(answer);
        }
        foreach (string answer in answers.answer2)
        {
            answers2queue.Enqueue(answer);
        }
        foreach (string answer in answers.answer3)
        {
            answers3queue.Enqueue(answer);
        }
        foreach (string answer in answers.answer4)
        {
            answers4queue.Enqueue(answer);
        }
        foreach (int rightAnswer in answers.rightAnswer)
        {
            rightAnswerqueue.Enqueue(rightAnswer);
        }

        DisplayNextQuestion();
    
    }

    public void DisplayNextQuestion()
    {
        RemoveAllButtonListeners();

        if (questions.Count == 0)
        {
            EndQuestions();
            return;
        }

        string question = questions.Dequeue();
        string ans1 = answers1queue.Dequeue();
        string ans2 = answers2queue.Dequeue();
        string ans3 = answers3queue.Dequeue();
        string ans4 = answers4queue.Dequeue();
        int rightAns = rightAnswerqueue.Dequeue();
        questionText.text = question;
        answer1Text.text = ans1;
        answer2Text.text = ans2;
        answer3Text.text = ans3;
        answer4Text.text = ans4;

        if (rightAns == 1)
        {
            btn1.onClick.AddListener(RightAnwserResponse);
            btn2.onClick.AddListener(WrongAnwserResponse);
            btn3.onClick.AddListener(WrongAnwserResponse);
            btn4.onClick.AddListener(WrongAnwserResponse);
        }
        else if (rightAns == 2)
        {
            btn1.onClick.AddListener(WrongAnwserResponse);
            btn2.onClick.AddListener(RightAnwserResponse);
            btn3.onClick.AddListener(WrongAnwserResponse);
            btn4.onClick.AddListener(WrongAnwserResponse);
        }
        else if (rightAns == 3)
        {
            btn1.onClick.AddListener(WrongAnwserResponse);
            btn2.onClick.AddListener(WrongAnwserResponse);
            btn3.onClick.AddListener(RightAnwserResponse);
            btn4.onClick.AddListener(WrongAnwserResponse);
        }
        else if (rightAns == 4)
        {
            btn1.onClick.AddListener(WrongAnwserResponse);
            btn2.onClick.AddListener(WrongAnwserResponse);
            btn3.onClick.AddListener(WrongAnwserResponse);
            btn4.onClick.AddListener(RightAnwserResponse);
        }

    }

    public void EndQuestions()
    {
        
        Time.timeScale = 1;
        animator.SetBool("isOpen", false);
        Destroy(enemy);

    }
    IEnumerator WaitAnimation()
    {
        yield return new WaitForSeconds(0.25f);
        Time.timeScale = 0;

    }


}
