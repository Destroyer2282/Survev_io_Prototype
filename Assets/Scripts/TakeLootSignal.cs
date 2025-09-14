using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakeLootSignal : MonoBehaviour
{
    public static TakeLootSignal Instance;

    private void Awake()
    {
        Instance = this;
        gameObject.SetActive(false);
    }
}
