using System.Collections.Generic;
using UnityEngine;

public class GameState : MonoBehaviour
{
    private Marks _currentTurn;
    private List<Marks> _marks;

    public GameState(Marks turn, List<Marks> marks = null)
    {
        if (marks == null)
        {
            for (int i = 0; i < 9; i++)
            {
                marks.Add(Marks.E);
            }
        }
        _currentTurn = turn;
        _marks = marks;
    }
}
