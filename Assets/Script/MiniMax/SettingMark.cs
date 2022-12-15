using System.Collections.Generic;
using UnityEngine;

public class SettingMark : MonoBehaviour
{
    private List<int> _indexRange = new();
    private int _index;

    public SettingMark(int index)
    {
        if (_indexRange.IndexOf(index) == -1)
        {
            Debug.LogError("範囲外のインデックスを指定しています");
        }
        _index = index;
    }
}
