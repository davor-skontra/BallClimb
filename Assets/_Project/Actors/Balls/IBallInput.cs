using System;
using UniRx;

namespace Actors.Balls
{
    public interface IBallInput
    {
        IObservable<float> RawDirection { get; }
        IObservable<Unit> Jump { get; }
    }
}