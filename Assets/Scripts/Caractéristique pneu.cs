using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Caractéristiquepneu : MonoBehaviour
{
    [SerializeField] public bool _pneuCorrect;
    [SerializeField] public bool _lisibilité;
    // Start is called before the first frame update
    void Start()
    {
        GenerationAleaPneuCorrect();
        GenerationAleaLisibilité();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void GenerationAleaPneuCorrect()
    {
        _pneuCorrect = true;
        if(Random.Range(0, 50) >= 25)
        {
            _pneuCorrect=false;
        }
    }
    void GenerationAleaLisibilité()
    {
        _lisibilité = true;
        if (Random.Range(0, 50) >= 25)
        {
            _lisibilité = false;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag=="Sol")
        {
            Debug.Log("Test");
            EventManager.TriggerEvent("Rebond");
        }
    }
}
