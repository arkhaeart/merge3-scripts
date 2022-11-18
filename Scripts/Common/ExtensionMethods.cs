using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ExtensionMethods 
{
    public static void DestroyAllChildren(this Transform transform,bool immediate)
    {
        while(transform.childCount>0)
        {
            if(immediate)
            {
                GameObject.DestroyImmediate(transform.GetChild(0).gameObject);
            }
            else
            {
                GameObject.Destroy(transform.GetChild(0).gameObject, 0);
            }
        }
    }
    public static Vector2 StripLowerAxis(this Vector2 vector2)
    {
        if(Mathf.Abs( vector2.y)>=Mathf.Abs(vector2.x))
        {
            vector2.x = 0;
        }
        else
        {
            vector2.y = 0;
        }
        return vector2;
    }
}
