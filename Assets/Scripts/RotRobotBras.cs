using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotRobotBras : MonoBehaviour
{
    private Transform _transform;
    // Start is called before the first frame update
    void Start()
    {
        EventManager.StartListening("UpdateAxisValueBras", updateRotation);
        _transform = transform;
    }

    // Update is called once per frame
    void Update()
    {

    }
    void updateRotation(EventParam e)
    {
        EventParamVector2 _eventParamVector2 = (EventParamVector2)e;
        Vector3 rot = new Vector3( _eventParamVector2.Value.y,0, 0);
        if (!(_transform.rotation.x > 0.30 | _transform.rotation.x < 0.30)) {  
            
            _transform.Rotate(rot * Time.deltaTime * _eventParamVector2.Speed);
            Debug.Log(_transform.rotation.x);
        }
    }
}
