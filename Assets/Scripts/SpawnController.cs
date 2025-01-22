using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SpawnController : MonoBehaviour
{
    [SerializeField] private GameObject _prefabPneu;
    [SerializeField] private GameObject _parentPneu;
    [SerializeField] private TextMeshProUGUI _textPneuDetection;
    [SerializeField] private TextMeshProUGUI _textPneuBenne;
    [SerializeField] private int _pneuRestant; 
    // Start is called before the first frame update
    void Start()
    {
        _pneuRestant = 10;
        UpdateTextBenne();
        _textPneuDetection.text = "Waiting";
        EventManager.StartListening("PneuCorrect", PneuCorrect);
        EventManager.StartListening("PneuIncorrect", PneuIncorrect);
        EventManager.StartListening("PneuIllisible", PneuIllisible);
        EventManager.StartListening("PneuRetire", PneuRetire);
        EventManager.StartListening("PneuExitBenne", PneuExitBenne);
        EventManager.StartListening("PneuBonnePlace", PneuBonnePlace);


    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void SpawnPneu()
    {
        EventManager.TriggerEvent("SpawnPneu",new EventSpawnPneu(_prefabPneu,_parentPneu));    
    }
    void PneuCorrect(EventParam e)
    {
        _textPneuDetection.text = "Pneu Conforme";
        Debug.Log("AHHHHHHHHHHHHH");


    }
    void PneuIncorrect(EventParam e)
    {
        _textPneuDetection.text = "Pneu Non-Conforme";
    }
    void PneuIllisible(EventParam e)
    {
        _textPneuDetection.text = "Pneu Illisible";
    }
    void PneuRetire(EventParam e)
    {
        _textPneuDetection.text = "Waiting";
    }
    void PneuBonnePlace(EventParam e)
    {
        _pneuRestant--;
        UpdateTextBenne();
    }
    void PneuExitBenne(EventParam e)
    {
        _pneuRestant++;
        UpdateTextBenne();

    }
    void UpdateTextBenne()
    {
        _textPneuBenne.text = "Pneu Restant : " + _pneuRestant + "/10";
    }

}
