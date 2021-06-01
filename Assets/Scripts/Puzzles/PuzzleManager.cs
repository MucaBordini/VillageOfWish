using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PuzzleManager : MonoBehaviour
{
    private AudioSource sounds;
    public AudioClip rightAnswerSound;
    public AudioClip wrongAnswerSound;

    public Text questionText;
    public Text answer1Text;
    public Text answer2Text;
    public Text answer3Text;
    public Text answer4Text;

    public Button btn1;
    public Button btn2;
    public Button btn3;
    public Button btn4;
    public Button ajuda;

    public Animator animator;

    private Queue<string> questions;
    private Queue<string> answers1queue;
    private Queue<string> answers2queue;
    private Queue<string> answers3queue;
    private Queue<string> answers4queue;
    private Queue<int> rightAnswerqueue;

    private GameObject enemy;

    List<int> cantBeThisValues = new List<int>();
    private int helpCalls = 0;

    void WrongAnwserResponse(Button btnWrong)
    {
        PopUpText.fillPopUp("Resposta Errada!!!");
        paintButtons(btnWrong);
        sounds.PlayOneShot(wrongAnswerSound);
        PlayerStats.getIstance().healthLoss();
    }
    void RightAnwserResponse()
    {
        sounds.PlayOneShot(rightAnswerSound);
        PlayerStats.getIstance().addPoints();
        DisplayNextQuestion();
    }

    void ResetButtonColor()
    {
        ColorBlock colors1 = btn1.colors;
        ColorBlock colors2 = btn2.colors;
        ColorBlock colors3 = btn3.colors;
        ColorBlock colors4 = btn4.colors;
        colors1.normalColor = Color.white;
        colors2.normalColor = Color.white;
        colors3.normalColor = Color.white;
        colors4.normalColor = Color.white;

        btn1.colors = colors1;
        btn2.colors = colors2;
        btn3.colors = colors3;
        btn4.colors = colors4;
    }

    void paintButtons(Button btn)
    {
        ColorBlock colors = btn.colors;
        colors.normalColor = Color.red;
        btn.colors = colors;
    }

    bool verifyHelp(int value, List<int> listOfValues)
    {
        if (listOfValues.Contains(value))
            return true;
        else
            return false;
    }

    void HelpButton(int rightAns)
    {
        helpCalls++;
        if (helpCalls <= 2)
        {
            if (!cantBeThisValues.Contains(rightAns))
            {
                cantBeThisValues.Add(rightAns);
            }

            if (PlayerStats.getIstance().getPoints() < 100)
            {
                PopUpText.fillPopUp("Pontos insuficientes! São necessários no mínimo 100 pontos para pedir ajuda!");
                StartCoroutine(WaitPopUp());
            }
            else
            {
                PlayerStats.getIstance().usePoints();
                int randomNumber = Random.Range(1, 4);
                while (verifyHelp(randomNumber, cantBeThisValues))
                {
                    randomNumber = Random.Range(1, 4);
                }
                switch (randomNumber)
                {
                    case 1:
                        paintButtons(btn1);
                        break;
                    case 2:
                        paintButtons(btn2);
                        break;
                    case 3:
                        paintButtons(btn3);
                        break;
                    case 4:
                        paintButtons(btn4);
                        break;
                }
                cantBeThisValues.Add(randomNumber);
            }
        }
        else if (helpCalls == 20)
        {
            PopUpText.fillPopUp("Você não cansa de apertar esse botão?");
        }
        else
        {
            PopUpText.fillPopUp("Já utilizou todas as ajudas!");
        }
        
    }

    void RemoveAllButtonListeners()
    {
        btn1.onClick.RemoveAllListeners();
        btn2.onClick.RemoveAllListeners();
        btn3.onClick.RemoveAllListeners();
        btn4.onClick.RemoveAllListeners();
        ajuda.onClick.RemoveAllListeners();
    }

    void Start()
    {
        questions = new Queue<string>();
        answers1queue = new Queue<string>();
        answers2queue = new Queue<string>();
        answers3queue = new Queue<string>();
        answers4queue = new Queue<string>();
        rightAnswerqueue = new Queue<int>();
        sounds = GetComponent<AudioSource>();
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
        ResetButtonColor();
        RemoveAllButtonListeners();
        cantBeThisValues.Clear();
        helpCalls = 0;

        PopUpText.fillPopUp("");

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
            btn2.onClick.AddListener(delegate { WrongAnwserResponse(btn2); });
            btn3.onClick.AddListener(delegate { WrongAnwserResponse(btn3); });
            btn4.onClick.AddListener(delegate { WrongAnwserResponse(btn4); });
        }
        else if (rightAns == 2)
        {
            btn1.onClick.AddListener(delegate { WrongAnwserResponse(btn1); });
            btn2.onClick.AddListener(RightAnwserResponse);
            btn3.onClick.AddListener(delegate { WrongAnwserResponse(btn3); });
            btn4.onClick.AddListener(delegate { WrongAnwserResponse(btn4); });
        }
        else if (rightAns == 3)
        {
            btn1.onClick.AddListener(delegate { WrongAnwserResponse(btn1); });
            btn2.onClick.AddListener(delegate { WrongAnwserResponse(btn2); });
            btn3.onClick.AddListener(RightAnwserResponse);
            btn4.onClick.AddListener(delegate { WrongAnwserResponse(btn4); });
        }
        else if (rightAns == 4)
        {
            btn1.onClick.AddListener(delegate { WrongAnwserResponse(btn1); });
            btn2.onClick.AddListener(delegate { WrongAnwserResponse(btn2); });
            btn3.onClick.AddListener(delegate { WrongAnwserResponse(btn3); });
            btn4.onClick.AddListener(RightAnwserResponse);
        }

        ajuda.onClick.AddListener(delegate { HelpButton(rightAns); });

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

    IEnumerator WaitPopUp()
    {
        yield return new WaitForSeconds(3f);
        PopUpText.fillPopUp("");

    }


}
