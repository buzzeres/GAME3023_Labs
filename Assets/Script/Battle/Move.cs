using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Move class definition
[Serializable]
public class Move
{
    public MoveBase Base { get; set; } // Reference to the move base data
    public int PP { get; set; }  // Current Power Points for the move
    public object Pokemon { get; internal set; }

    public Move(MoveBase pBase)
    {
        Base = pBase;
        PP = pBase.PP; 
    }
}
