using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZeroScore : MonoBehaviour
{
    [SerializeField] ScoreData scoreData;
    private void Awake()
    {
        scoreData.score = 0;
    }
}
