using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePhase : ScriptableObject
{
    // Start is called before the first frame update
    public void Initialize()
    {
            initializeInternal();
    }


    protected virtual void initializeInternal(){

    }
}
