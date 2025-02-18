using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectionKo : MonoBehaviour
{
    [SerializeField] private Caractéristiquepneu caract;
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
            caract=other.GetComponent<Caractéristiquepneu>();
            if(caract._pneuCorrect==false)
            {
                EventManager.TriggerEvent("PneuBonnePlace");
            }
        }
        Destroy(other.gameObject);
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Pneu")
        {
            caract = other.GetComponent<Caractéristiquepneu>();
            if (caract._pneuCorrect == false)
            {
                EventManager.TriggerEvent("PneuExitBenne");
            }
        }
    }
}
