using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleTrigger : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D collision)
    {
        TriggerPuzzle();
    }

    public Puzzle puzzle;
    public Answers answers1;
    

    public void TriggerPuzzle ()
    {
        FindObjectOfType<PuzzleManager>().StartPuzzle(puzzle, answers1, this.gameObject);
    }

}
