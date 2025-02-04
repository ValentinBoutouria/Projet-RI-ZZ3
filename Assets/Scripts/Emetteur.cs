using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Emetteur : MonoBehaviour
{
    [SerializeField] private float score;
    [SerializeField] private float timer ;
    [SerializeField] private float counter;
    [SerializeField] private bool _dehorsLigne=false;
    // Start is called before the first frame update
    void Start()
    {
        score = 1000;
        timer = 1;
        counter = 0;

    }

    // Update is called once per frame
    void Update()
    {
        if (_dehorsLigne)
        {
            ControlValueScore();
        }


    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag=="Start")
        {
            counter = 0;
            score = 1000;
            EventManager.TriggerEvent("StartChemin");

            EventManager.TriggerEvent("UpdateScoreValue", new EventScoreGrueUpdate(score));
        }
        if(other.tag=="End")
        {
            EventManager.TriggerEvent("EndChemin", new EventScoreGrueUpdate(score));
            _dehorsLigne = false;
            
        }
        if(other.tag=="Chemin")
        {
            _dehorsLigne=false;

        }
    }
    private void OnTriggerExit(Collider other)
    {
        if(other.tag=="Chemin")
        {
            _dehorsLigne = true;
        }
    }
    void ControlValueScore()
    {
        counter += Time.deltaTime;
        
        if (counter > timer) 
        {
            score = score - 1;
            counter = 0;
            EventManager.TriggerEvent("UpdateScoreValue", new EventScoreGrueUpdate(score));
        }
    }


}
