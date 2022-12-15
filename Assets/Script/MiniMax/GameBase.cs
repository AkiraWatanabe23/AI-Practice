using UnityEngine;

public class GameBase : MonoBehaviour
{
    private Marks[] _currentBoard = new Marks[9];
    private Marks _mark = Marks.E;

    private void Start()
    {
        for (int i = 0; i < _currentBoard.Length; i++)
        {
            _currentBoard[i] = Marks.E;
        }
    }

    //ƒ^[ƒ“‚ðØ‚è‘Ö‚¦‚é
    private Marks SwitchTurn()
    {
        if (_mark == Marks.O)
            return Marks.X;
        else if (_mark == Marks.X)
            return Marks.O;

        return Marks.E;
    }
}
