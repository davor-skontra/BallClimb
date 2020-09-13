using System;
using System.Collections;
using System.Collections.Generic;
using EndGameTracking;
using UniRx;
using UnityEngine;

public interface IEndGameService
{
    void DeclareVictory();
    void DeclareDefeat();
    IObservable<string> EndGameWithMessage { get; }
    
    bool GameEnded { get; }
}
public class EndGameService: IEndGameService
{
    private readonly IEndGameMessages _messages;
    private Subject<string> _endGameWithMessageSubject = new Subject<string>();
    
    public bool GameEnded { get; private set; }

    public EndGameService(IEndGameMessages messages)
    {
        _messages = messages;
    }
    
    public void DeclareVictory()
    {
        _endGameWithMessageSubject.OnNext(_messages.Victory);
        GameEnded = true;
    }

    public void DeclareDefeat()
    {
        _endGameWithMessageSubject.OnNext(_messages.Defeat);
        GameEnded = true;
    }

    public IObservable<string> EndGameWithMessage => _endGameWithMessageSubject;
}
