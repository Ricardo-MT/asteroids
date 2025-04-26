using System;
using UnityEngine;
using System.Collections.Generic;

public interface IGameEvent { }

public class EnemyDestroyedEvent : IGameEvent
{
    public Enemy Enemy { get; private set; }

    public EnemyDestroyedEvent(Enemy enemy)
    {
        Enemy = enemy;
    }
}

public class ScoreUpdateEvent : IGameEvent
{
    public float Score { get; private set; }

    public ScoreUpdateEvent(float score)
    {
        Score = score;
    }
}

public class PlayerHealthChanged : IGameEvent
{
    public float Health { get; private set; }
    public float MaxHealth { get; private set; }

    public PlayerHealthChanged(float health, float maxHealth)
    {
        Health = health;
        MaxHealth = maxHealth;
    }
}

public class MyEventManager : MonoBehaviour
{
    private Dictionary<Type, Delegate> _eventTable = new Dictionary<Type, Delegate>();

    public void AddEventListener<T>(Action<T> listener) where T : IGameEvent
    {
        var type = typeof(T);
        if (_eventTable.TryGetValue(type, out var existingDelegate))
        {
            _eventTable[type] = Delegate.Combine(existingDelegate, listener);
        }
        else
        {
            _eventTable[type] = listener;
        }
    }

    public void RemoveEventListener<T>(Action<T> listener) where T : IGameEvent
    {
        var type = typeof(T);
        if (_eventTable.TryGetValue(type, out var existingDelegate))
        {
            var newDelegate = Delegate.Remove(existingDelegate, listener);
            if (newDelegate == null)
                _eventTable.Remove(type);
            else
                _eventTable[type] = newDelegate;
        }
    }

    public void Trigger<T>(T gameEvent) where T : IGameEvent
    {
        var type = typeof(T);
        if (_eventTable.TryGetValue(type, out var existingDelegate))
        {
            ((Action<T>)existingDelegate)?.Invoke(gameEvent);
        }
    }
}
