using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventQueueManager : MonoBehaviour
{
    static public EventQueueManager instance;
    public Queue<ICommand> Events => _events;
    private Queue<ICommand> _events = new Queue<ICommand>();
    private void Awake()
    {
        if (instance != null) Destroy(this);
        instance = this;
    }

    private void Update()
    {
        while (_events.Count > 0)
        {
            _events.Dequeue().Execute();
        } 
        _events.Clear();
    }

    public void AddCommand(ICommand command) => _events.Enqueue(command);
}
