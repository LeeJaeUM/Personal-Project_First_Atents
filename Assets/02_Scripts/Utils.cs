using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class PRS
{
    public Vector3 pos;
    public Quaternion rot;
    public Vector3 scale;

    public PRS(Vector3 _pos, Quaternion _rot, Vector3 _scale)
    {
        this.pos = _pos;
        this.rot = _rot;
        this.scale = _scale;
    }
}

public class Utils
{
    public static Quaternion QI => Quaternion.identity;
}
