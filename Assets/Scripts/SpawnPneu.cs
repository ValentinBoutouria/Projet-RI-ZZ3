using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;

public class SpawnPneu : MonoBehaviour
{
    [SerializeField] Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = GameObject.Find("RobotD").GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void SpawnPneuFunc()
    {
        Debug.Log("test");
        animator.SetTrigger("ButtonPressed");
    }
}
