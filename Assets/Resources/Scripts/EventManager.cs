﻿using System;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    private Dictionary<string, Action<EventParam>> eventDictionary;
    private static EventManager eventManager;

    public static EventManager instance
    {
        get
        {
            if (!eventManager)
            {
                eventManager = FindObjectOfType(typeof(EventManager)) as EventManager;
                if (!eventManager)
                {
                    Debug.LogError("There needs to be one active EventManager script on a GameObject in your scene.");
                }
                else
                {
                    eventManager.Init();
                }
            }
            return eventManager;
        }
    }

    void Init()
    {
        if (eventDictionary == null)
        {
            eventDictionary = new Dictionary<string, Action<EventParam>>();
        }
    }

    public static void StartListening(string eventName, Action<EventParam> listener)
    {
        if (instance.eventDictionary.TryGetValue(eventName, out Action<EventParam> thisEvent))
        {
            //Add more event to the existing one
            thisEvent += listener;

            //Update the Dictionary
            instance.eventDictionary[eventName] = thisEvent;
        }
        else
        {
            //Add event to the Dictionary for the first time
            thisEvent += listener;
            instance.eventDictionary.Add(eventName, thisEvent);
        }
    }

    public static void StopListening(string eventName, Action<EventParam> listener)
    {
        if (instance == null) return;
        if (instance.eventDictionary.TryGetValue(eventName, out Action<EventParam> thisEvent))
        {
            //Remove event from the existing one
            thisEvent -= listener;
            if (thisEvent == null)
            {
                instance.eventDictionary.Remove(eventName);
            }
            else
            {
                //Update the Dictionary
                instance.eventDictionary[eventName] = thisEvent;
            }

        }
    }

    public static void TriggerEvent(string eventName, EventParam eventParam)
    {
        if (instance == null) return;
        if (instance.eventDictionary.TryGetValue(eventName, out Action<EventParam> thisEvent))
        {
            thisEvent.Invoke(eventParam);
            // OR USE  instance.eventDictionary[eventName](eventParam);
        }
    }

    internal static void TriggerEvent(string v)
    {
        TriggerEvent(v, null);
    }
}

//Re-usable structure/ Can be a class to. Add all parameters you need inside it
public interface EventParam
{

}

public class EventParamVector2 : EventParam
{
	private Vector2 value;
	private float speed ;

    public EventParamVector2(Vector2 Value, float Speed)
    {
        this.value = Value;
        this.speed = Speed;
    }

    public Vector2 Value { get => value; set => this.value = value; }
    public float Speed { get => speed; set => speed = value; }
}
public class EventSpawnPneu : EventParam
{
    private GameObject _pneu;
    private GameObject _parentPneu;


    public EventSpawnPneu(GameObject Pneu, GameObject ParentPneu)
    {
        this._pneu = Pneu;
        this._parentPneu = ParentPneu;
    }

    public GameObject Pneu { get => _pneu; set => _pneu = value; }
    public GameObject ParentPneu { get => _parentPneu; set => _parentPneu = value; }
}
public class EventScoreGrueUpdate : EventParam
{
    private float _score;
    


    public EventScoreGrueUpdate(float Score)
    {
        this._score = Score;
       
    }

    public float Score { get => _score; set => _score = value; }
}
public class EventScorePneuSave : EventParam
{
    private float _score;



    public EventScorePneuSave(float Score)
    {
        this._score = Score;

    }

    public float Score { get => _score; set => _score = value; }
}
public class EventScoreGrueSave : EventParam
{
    private float _score;



    public EventScoreGrueSave(float Score)
    {
        this._score = Score;

    }

    public float Score { get => _score; set => _score = value; }
}
public class EventScoreGrueLoad : EventParam
{

    public List<Data> topN;


    public EventScoreGrueLoad(List<Data> topN)
    {
        this.topN = topN;

    }

}
public class EventScorePneuLoad : EventParam
{

    public List<DataPneu> topN;


    public EventScorePneuLoad(List<DataPneu> topN)
    {
        this.topN = topN;

    }

}
public class EventUserNameValide : EventParam
{
    private string _user;
   
    public EventUserNameValide(string User)
    {
        this._user = User;
    }
    public string User { get => _user; set => _user = value; }
}
public class EventEnvoieChronos : EventParam
{
    private float _chronos;

    public EventEnvoieChronos(float Chronos)
    {
        this._chronos = Chronos;
    }

    public float Chronos { get => _chronos; set => _chronos = value; }
}
