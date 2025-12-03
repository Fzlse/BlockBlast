using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShapeStorage : MonoBehaviour
{
    public List<ShapeData> shapeData;
    public List<Shape> shapelist;

    void Start()
    {
     foreach (var shape in shapelist)
        {
            var shapeIndex = UnityEngine.Random.Range(0, shapeData.Count);
            shape.CreateShape(shapeData[shapeIndex]);
        }   
    }
    public Shape GetCurenntSelectedShape()
    {
        foreach (var shape in shapelist)
        {
            if (shape.IsOnStartPosition() == false && shape.IsAnyOfShapeSquareActive())
                return shape;
        }
        Debug.LogError("No Shape Selected");
        return null;
    }
}
