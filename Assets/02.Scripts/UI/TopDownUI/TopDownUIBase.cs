using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TopDownUIBase : MonoBehaviour
{
    protected ToDownGameUIManager ToDownGameUIManager;

    public virtual void Init(ToDownGameUIManager uiManager)
    {
        this.ToDownGameUIManager = uiManager;
    }

    protected abstract UIState GetUIState();
    public void SetActive(UIState state)
    {
        this.gameObject.SetActive(GetUIState() == state);
    }
}
