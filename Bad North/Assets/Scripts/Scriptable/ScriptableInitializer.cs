using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptableInitializer : MonoBehaviour
{
    [SerializeField] private List<DataHolder> _dataHolders;

    private void Awake()
    {
        foreach (DataHolder data in _dataHolders)
            data.Init();
    }
}