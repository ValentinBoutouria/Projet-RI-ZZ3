using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Emetteur : MonoBehaviour
{
    [SerializeField] private float score;
    [SerializeField] private float timer;
    [SerializeField] private float counter;
    // Start is called before the first frame update
    void Start()
    {

        

    }

    // Update is called once per frame
    void Update()
    {
        ControlValueScore();


    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag=="Start")
        {
            counter = 0;
            score = 1000;
            EventManager.TriggerEvent("StartChemin");

            EventManager.TriggerEvent("UpdateScoreValue");
        }
        if(other.tag=="End")
        {
            EventManager.TriggerEvent("EndChemin");
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if(other.tag=="Chemin")
        {
            score -= Time.deltaTime;
            counter += Time.deltaTime;
        }
    }
    void ControlValueScore()
    {
        if(counter > timer) 
        {
            EventManager.TriggerEvent("UpdateScoreValue");
            counter = 0;
        }
    }


}
