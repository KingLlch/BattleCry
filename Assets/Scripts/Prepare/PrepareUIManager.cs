using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrepareUIManager : MonoBehaviour
{
    private PrepareUIManager instance;

    public PrepareUIManager Instance
    {
        get { return instance; }
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
}
