using System;
using System.Collections;
using System.Collections.Generic;
using AlkarInjector;
using AlkarInjector.Attributes;
using UnityEngine;
using Utilities;

public class VictoryContact : MonoBehaviour
{
    [Inject] private IEndGameService _endGameService;

    private void Awake()
    {
        Alkar.InjectMonoBehaviour(this);
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.IsPlayer())
        {
            _endGameService.DeclareVictory();
        }
    }
}
