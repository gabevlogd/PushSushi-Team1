using UnityEngine;

public struct ObjectState
{
    public Vector3 Position;
    public Quaternion Rotation;

    public ObjectState(Vector3 pos, Quaternion rot)
    {
        Position = pos;
        Rotation = rot;
    }
}