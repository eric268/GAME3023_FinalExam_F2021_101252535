using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum IconCornerPos
{
    Top_Right,
    Top_Left,
    Top_Center,
    Bottom_Left,
    Bottom_Right,
    Bottom_Center,
    Left_Center,
    Right_Center
}

public class IconPositions
{
    public static Vector2 GetCornerOffset(IconCornerPos pos)
    {
        switch(pos)
        {
            case IconCornerPos.Top_Right:
                return new Vector2(50.0f, 50.0f);

            case IconCornerPos.Top_Left:
                return new Vector2(-50.0f, 50.0f);            
            
            case IconCornerPos.Top_Center:
                return new Vector2(0, 50.0f);

            case IconCornerPos.Bottom_Left:
                return new Vector2(-50.0f, -50.0f);

            case IconCornerPos.Bottom_Right:
                return new Vector2(50.0f, -50.0f);

            case IconCornerPos.Bottom_Center:
                return new Vector2(0, -50.0f);

            case IconCornerPos.Left_Center:
                return new Vector2(-50.0f, 0);

            case IconCornerPos.Right_Center:
                return new Vector2(50.0f, 0);

        }
        return new Vector2(0, 0);
    }
}
