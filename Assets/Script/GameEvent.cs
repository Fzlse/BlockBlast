using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEvent : MonoBehaviour
{
    public static Action<int> AddScore;
    public static Action CheckIfShapeCanBePlaced;
    public static Action MoveShapeBackToStartPosition;
    public static Action RequestNewShape;
    public static Action SetShapeInactive;
}
