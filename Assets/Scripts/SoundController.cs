using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.InputSystem.LowLevel.InputStateHistory;

public class SoundController : MonoBehaviour
{
    //*****************************Audio Clips :

    [SerializeField] private AudioClip _rebondPneu; //sound à jouer

    [SerializeField] private AudioClip _pneuBonneBenne; //sound à jouer
    [SerializeField] private AudioClip _pneuMauvaiseBenne; //sound à jouer/*


    [SerializeField] private AudioClip _pneuScannedConforme; //sound à jouer
    [SerializeField] private AudioClip _pneuScannedNonConforme; //sound à jouer
    [SerializeField] private AudioClip _pneuInScannable; //sound à jouer
    [SerializeField] private AudioClip _jeuPneuFini; //sound à jouer


    [SerializeField] private AudioClip _pertePointGrue; //sound à jouer/*
    [SerializeField] private AudioClip _jeuStarted; //sound à jouer
    [SerializeField] private AudioClip _jeuGrueFini; //sound à jouer

    //*****************************Audio Sources:

    [SerializeField] private AudioSource _boutonSpawnPneuSource; //d'où vient le sound à jouer
    [SerializeField] private AudioSource _machineScan; //d'où vient le sound à jouer
    [SerializeField] private AudioSource _benneSource; //d'où vient le sound à jouer
    [SerializeField] private AudioSource _playerSource; //d'où vient le sound à jouer
    // Start is called before the first frame update
    void Start()
    {
        EventManager.StartListening("Rebond", Rebond);
        EventManager.StartListening("PneuCorrect", PneuCorrect);
        EventManager.StartListening("PneuIncorrect", PneuIncorrect);
        EventManager.StartListening("PneuIllisible", PneuIllisible);
        EventManager.StartListening("PneuBonnePlace", PneuBonnePlace);
        EventManager.StartListening("FinGamePneu", FinGamePneu);

        //*****************************Niveau 2
        
        EventManager.StartListening("StartChemin", StartChemin);
        EventManager.StartListening("EndChemin", EndChemin);

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    //dans event :
    //// Jouer le son à partir du clip défini dans l'inspecteur
    //audioSource.PlayOneShot(soundClip);
    void Rebond(EventParam e)
    {
        _playerSource.PlayOneShot(_rebondPneu);
    }
    void PneuCorrect(EventParam e)
    {
        _machineScan.PlayOneShot(_pneuScannedConforme);
    }
    void PneuIncorrect(EventParam e)
    {
        _machineScan.PlayOneShot(_pneuScannedNonConforme);
    }
    void PneuIllisible(EventParam e)
    {
        _machineScan.PlayOneShot(_pneuInScannable);
    }

    void PneuBonnePlace(EventParam e)
    {
        _benneSource.PlayOneShot(_pneuBonneBenne);
    }
    void FinGamePneu(EventParam e)
    {
        _playerSource.PlayOneShot(_jeuPneuFini);
    }

    //*****************************Niveau 2
    void StartChemin(EventParam e)
    {
        _playerSource.PlayOneShot(_jeuStarted);
        
    }
    void EndChemin(EventParam e)
    {
        _machineScan.PlayOneShot(_jeuGrueFini);
        

    }

}
