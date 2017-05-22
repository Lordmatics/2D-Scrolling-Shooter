using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Utility
{

    public static void SetPosition(this Transform trans, Vector3 pos)
    {
        trans.position = pos;
    }

    public static void SetScale(this Transform trans, float val)
    {
        trans.localScale = new Vector3(val, val, val);
    }
}
