using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct Block
{

    public Vector3 Position;

    public string BlockID;

    public Block(Vector3 _Position, string _BlockID = "Air")
    {
        this.BlockID = _BlockID;
        this.Position = _Position;
    }

}
