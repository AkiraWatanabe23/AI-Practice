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
            Debug.LogError("�͈͊O�̃C���f�b�N�X���w�肵�Ă��܂�");
        }
        _index = index;
    }
}
