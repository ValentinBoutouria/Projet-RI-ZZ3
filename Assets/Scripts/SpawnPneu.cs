using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;

public class SpawnPneu : MonoBehaviour
{
    [SerializeField] private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = GameObject.Find("RobotD").GetComponent<Animator>();
        EventManager.StartListening("SpawnPneu", SpawnPneuFunc);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void SpawnPneuFunc(EventParam e)
    {
        animator.SetTrigger("ButtonPressed");
        EventManager.TriggerEvent("BoutonVertTriggered");
        EventSpawnPneu _eventSpawnPneu=(EventSpawnPneu)e;
        float y = 0;
        for (int i = 0; i < 10; i++)
        {
            y = y + (float)0.1;
            Vector3 pos=new Vector3(0, y, 0);
            GameObject temp=Instantiate(_eventSpawnPneu.Pneu,Vector3.zero,Quaternion.identity,_eventSpawnPneu.ParentPneu.transform);
            temp.transform.localPosition = pos; 
        }
    }
}
