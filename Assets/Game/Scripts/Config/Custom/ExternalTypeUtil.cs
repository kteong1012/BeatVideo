using System;
using UnityEngine;

namespace Game.Cfg
{
    internal class ExternalTypeUtil
    {
        internal static Vector2 NewVector2(vector2 vector2)
        {
            return new Vector2(vector2.X, vector2.Y);
        }
    }
}
