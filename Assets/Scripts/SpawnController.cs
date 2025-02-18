using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
//using UnityEngine.UIElements;
using Unity.VisualScripting;
using System;

public class SpawnController : MonoBehaviour
{
    [SerializeField] private GameObject _prefabPneu;
    [SerializeField] private GameObject _parentPneu;
    [SerializeField] private GameObject _chemin;
    [SerializeField] private GameObject _endChemin;
    [SerializeField] private GameObject _endGame;
    [SerializeField] private GameObject _midGame;
    [SerializeField] private GameObject _panelScoreGrue;
    
    [SerializeField] private TextMeshProUGUI _textPneuDetection;
    [SerializeField] private TextMeshProUGUI _textPneuBenne;
    [SerializeField] private TextMeshProUGUI _textScoreCheminActuel;
    [SerializeField] private TextMeshProUGUI _textScoreCheminHighScore;
    [SerializeField] private TextMeshProUGUI _textScoreCheminHighScorePneu;
    [SerializeField] private Button _restartButton;
    [SerializeField] private int _pneuRestant; 
    // Start is called before the first frame update
    void Start()
    {
        _pneuRestant = 10;
        UpdateTextBenne();
        _textPneuDetection.text = "Waiting";
        _restartButton.onClick.AddListener(NewGamePneu);
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
        EventManager.StartListening("UserNameValide", UpdateUserPneu);
        EventManager.StartListening("UpdateChronos", UpdateChronosPneu);
        EventManager.StartListening("LoadDataPneu", UpdateTextHighScoresPneu);


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
        _midGame.SetActive(false);
        _endGame.SetActive(true);

        //affichage clavier + recupération du score et envoie dans le json + update le panel avec les données du json

    }
    void NewGamePneu()
    {
        _midGame.SetActive(true);
        _endGame.SetActive(false);
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
        _panelScoreGrue.SetActive(true);

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
        _panelScoreGrue.SetActive(false);

        //save dans un json la variable score 
        EventManager.TriggerEvent("Savedata",new EventScoreGrueUpdate(_eventScoreGrueUpdate.Score));
    }
    void UpdateTextHighScores(EventParam e)
    {
        EventScoreGrueLoad _eventScoreGrueLoad = (EventScoreGrueLoad)e;
        string affichage  = "";
        for (int i = 0; i < 10; i++)
        {

            if (i < _eventScoreGrueLoad.topN.Count)
            {
                affichage +=  _eventScoreGrueLoad.topN[i].user + " " + _eventScoreGrueLoad.topN[i].int1 +"\n";

            }
        }

        _textScoreCheminHighScore.text = affichage;
        
    }
    void UpdateUserPneu(EventParam e)
    {
        //on change le json 
    }
    void UpdateChronosPneu(EventParam e)
    {
        //on change le json
    }
    private void UpdateTextHighScoresPneu(EventParam param)
    {
        EventScorePneuLoad _eventScorePneuLoad = (EventScorePneuLoad)param;
        string affichage = "";
        for (int i = 0; i < 10; i++)
        {
        
            if (i < _eventScorePneuLoad.topN.Count)
            {
                affichage += _eventScorePneuLoad.topN[i].user + " " + _eventScorePneuLoad.topN[i].score + "\n";
                
            }
        }
       
        _textScoreCheminHighScorePneu.text = affichage;

    }
}
