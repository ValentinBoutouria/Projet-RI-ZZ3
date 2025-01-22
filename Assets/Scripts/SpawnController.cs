using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SpawnController : MonoBehaviour
{
    [SerializeField] private GameObject _prefabPneu;
    [SerializeField] private GameObject _parentPneu;
    [SerializeField] private GameObject _chemin;
    [SerializeField] private GameObject _endChemin;
    [SerializeField] private TextMeshProUGUI _textPneuDetection;
    [SerializeField] private TextMeshProUGUI _textPneuBenne;
    [SerializeField] private TextMeshProUGUI _textScoreChemin;
    [SerializeField] private int _pneuRestant; 
    [SerializeField] private float _scoreChemin;
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

        EventManager.StartListening("StartChemin", StartChemin);
        EventManager.StartListening("UpdateScoreValue", UpdateScoreValue);
        EventManager.StartListening("EndChemin", EndChemin);


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
    void StartChemin(EventParam e)
    {
        _chemin.SetActive(true);
        _endChemin.SetActive(true);

    }
    void UpdateScoreValue(EventParam e)
    {
        _textScoreChemin.text = "Score Actuel : " + (int)_scoreChemin;
    }
    void EndChemin(EventParam e)
    {
        _chemin.SetActive(false);
        _endChemin.SetActive(false);
        //save dans un json la variable score 
    }
}
