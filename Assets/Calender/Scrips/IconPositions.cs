using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum IconCornerPos
{
    Top_Right,
    Top_Left,
    Bottom_Left,
    Bottom_Right
}

public class IconPositions
{
    public static Vector2 GetCornerOffset(IconCornerPos pos)
    {
        switch(pos)
        {
            case IconCornerPos.Top_Right:
                return new Vector2(50.0f, 60.0f);

            case IconCornerPos.Top_Left:
                return new Vector2(-50.0f, 60.0f);

            case IconCornerPos.Bottom_Left:
                return new Vector2(-50.0f, -60.0f);

            case IconCornerPos.Bottom_Right:
                return new Vector2(50.0f, -60.0f);
        }
        return new Vector2(0, 0);
    }
}
