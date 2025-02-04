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
    [SerializeField] private TextMeshProUGUI _textScoreCheminActuel;
    [SerializeField] private TextMeshProUGUI _textScoreCheminHighScore;
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
        EventManager.StartListening("StartChemin", StartChemin);
        EventManager.StartListening("UpdateScoreValue", UpdateScoreValue);
        EventManager.StartListening("EndChemin", EndChemin);
        EventManager.StartListening("FinGamePneu", FinGamePneu);
        EventManager.StartListening("LoadData", UpdateTextHighScores);


    }
    

    // Update is called once per frame
    void Update()
    {
        if (_pneuRestant == 0)
        {
            EventManager.TriggerEvent("FinGamePneu");
        }
    }
    void FinGamePneu(EventParam e)
    {
        //affichage clavier + recupération du score et envoie dans le json + update le panel avec les données du json

    }
    public void DestroyPneu()
    {
       
        int _numberchild=_parentPneu.transform.childCount;
        for (int i = 0; i < _numberchild; i++)
        {
            Destroy(_parentPneu.transform.GetChild(i).gameObject);
        }
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
        EventScoreGrueUpdate _eventScoreGrueUpdate = (EventScoreGrueUpdate)e;
        _textScoreCheminActuel.text = "Score Actuel : " + (int)_eventScoreGrueUpdate.Score;
    }
    void EndChemin(EventParam e)
    {
        EventScoreGrueUpdate _eventScoreGrueUpdate = (EventScoreGrueUpdate)e;
        _chemin.SetActive(false);
        _endChemin.SetActive(false);
        //save dans un json la variable score 
        EventManager.TriggerEvent("Savedata",new EventScoreGrueUpdate(_eventScoreGrueUpdate.Score));
    }
    void UpdateTextHighScores(EventParam e)
    {
        EventScoreGrueLoad _eventScoreGrueLoad = (EventScoreGrueLoad)e;
        _textScoreCheminHighScore.text = "User : " + (int)_eventScoreGrueLoad.Score;
        
    }
}
