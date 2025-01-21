using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotRobotSocle : MonoBehaviour
{
    private Transform _transform;
    // Start is called before the first frame update
    void Start()
    {
        EventManager.StartListening("UpdateAxisValueSocle", updateRotation);
        _transform = transform;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void updateRotation(EventParam e)
    {
        EventParamVector2 _eventParamVector2=(EventParamVector2)e;
        Vector3 rot=new Vector3(0, _eventParamVector2.Value.x, 0);
        _transform.Rotate(rot*Time.deltaTime* _eventParamVector2.Speed);
    }
}
