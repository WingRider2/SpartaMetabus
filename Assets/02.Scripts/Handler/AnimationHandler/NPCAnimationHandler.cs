using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCAnimationHandler : MonoBehaviour , IAnimationHandler
{
    protected Animator animator;

    public void Move(Vector2 obj)
    {
        
    }

    protected virtual void Awake()
    {
        animator = GetComponentInChildren<Animator>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
