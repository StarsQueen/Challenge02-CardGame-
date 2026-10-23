using System;
using UnityEngine;

// A message "channel" stored as an asset. Systems share the asset instead of
// referencing each other: one raises, others listen. (Observer pattern as ScriptableObject.)
public abstract class EventChannelSO<T> : ScriptableObject
{
    public event Action<T> Raised;
    public void Raise(T value) => Raised?.Invoke(value);
}
