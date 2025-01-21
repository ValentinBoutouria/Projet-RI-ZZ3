using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectionPneu : MonoBehaviour
{
    
    [SerializeField] private Caract�ristiquepneu _caract;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag=="Pneu") 
        {
            _caract=other.GetComponent<Caract�ristiquepneu>();
            GestionErreurPneu();
        }
    }
    private void OnTriggerExit(Collider other)
    {
        EventManager.TriggerEvent("PneuRetire");
    }
    void GestionErreurPneu()
    {
        if( _caract._lisibilit�)
        {
            if (_caract._pneuCorrect)
            {
                EventManager.TriggerEvent("PneuCorrect");
            }
            else
            {
                EventManager.TriggerEvent("PneuIncorrect");
            }
        }
        else
        {
            EventManager.TriggerEvent("PneuIllisible");
        }
        


    }
}
