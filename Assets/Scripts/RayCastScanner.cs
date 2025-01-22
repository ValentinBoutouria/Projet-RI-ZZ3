using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayCastScanner : MonoBehaviour
{
    [SerializeField] private int rayLength;
    [SerializeField] private int layerMask;
    [SerializeField] private Caract�ristiquepneu hitCarac;
    // Start is called before the first frame update
    void Start()
    {
        rayLength = 10;
        layerMask = 0;
        
    }

    // Update is called once per frame
    void Update()
    {
        RaycastDetetctionPneu();
        
    }
    void RaycastDetetctionPneu()
    {
        // R�cup�rer la position et la direction du rayon
        Vector3 origin = transform.position;
        Vector3 direction = transform.forward;

        // Tracer un rayon pour le d�bogage
        Debug.DrawRay(origin, direction * rayLength, Color.green);

         
        // Effectuer le Raycast
        if (Physics.Raycast(origin, direction, out RaycastHit hit, rayLength))
        {
            // R�cup�rer un composant du GameObject touch� (par exemple, un Rigidbody)
            hitCarac = hit.collider.gameObject.GetComponent<Caract�ristiquepneu>();
            if(hitCarac._pneuCorrect)
            {
                EventManager.TriggerEvent("PneuCorrect");


            }
            else
            {
                EventManager.TriggerEvent("PneuIncorrect");

            }
        }

    }
}
