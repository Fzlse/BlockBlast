using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shape : MonoBehaviour
{
    public GameObject squareShapeImagaePrefab;

    //[HideInInspector]
    public ShapeData CurrentShapeData;
    private List<GameObject> _currentShapeSquares = new List<GameObject>();

    void Start()
     {
        RequestNewShape(CurrentShapeData);
     }

    public void RequestNewShape(ShapeData shapeData)
    {
        CreateShape(shapeData);
    }

    public void CreateShape(ShapeData shapeData)
    {
        CurrentShapeData = shapeData;
        var totalSquareNumber = GetNumberOfSquares(shapeData);

        while (_currentShapeSquares.Count < totalSquareNumber)
        {
            _currentShapeSquares.Add(Instantiate(squareShapeImagaePrefab, transform) as GameObject);
        }
        foreach (var square in _currentShapeSquares)
        {
            square.gameObject.transform.position = Vector3.zero;
            square.gameObject.SetActive(false);
        }

        var squareReact = squareShapeImagaePrefab.GetComponent<RectTransform>();
        var moveDistance = new Vector2(
            squareReact.rect.width * squareReact.transform.localScale.x,
            squareReact.rect.height * squareReact.transform.localScale.y
        );

        int currentIndexInList = 0;
        for (var row = 0; row < shapeData.rows; row++)
        {
            for (var column = 0; column < shapeData.columns; column++)
            {
                if (shapeData.board[row].column[column])
                {
                    float posX = GetXPositionForShapeSquare(shapeData, column, moveDistance);
                    float posY = GetYPositionForShapeSquare(shapeData, row, moveDistance);

                    _currentShapeSquares[currentIndexInList].gameObject.SetActive(true);
                    _currentShapeSquares[currentIndexInList].GetComponent<RectTransform>().localPosition =
                    new Vector2(GetXPositionForShapeSquare(shapeData, column, moveDistance), 
                    GetYPositionForShapeSquare(shapeData, row, moveDistance));
                    currentIndexInList++;
                }
            }
        }
    }

    private float GetXPositionForShapeSquare(ShapeData shapeData, int column, Vector2 moveDistance)
    {
        float shiftOnX = 0f;
        if (shapeData.columns > 1)
        {
            float startXPos;
            if (shapeData.columns % 2 != 0)
                startXPos = (shapeData.columns / 2) * moveDistance.x * -1;
            else
                startXPos = ((shapeData.columns / 2) - 1) * moveDistance.x * -1 - moveDistance.x / 2;
            shiftOnX = startXPos + column * moveDistance.x;

        }
        return shiftOnX;
    }

    private float GetYPositionForShapeSquare(ShapeData shapeData, int row, Vector2 moveDistance)
    {
        float shiftOnY = 0f;
        if (shapeData.rows > 1)
        {
            float startYPos;
            if (shapeData.rows % 2 != 0)
                startYPos = (shapeData.rows / 2) * moveDistance.y;
            else
                startYPos = ((shapeData.rows / 2) - 1) * moveDistance.y + moveDistance.y / 2;
            shiftOnY = startYPos - row * moveDistance.y;
        }
        return shiftOnY;
    }

    private int GetNumberOfSquares(ShapeData shapeData)
    {
        int number = 0;

        foreach (var rowData in shapeData.board)
        {
            foreach (var active in rowData.column)
            {
                if (active)
                {
                    number++;
                }
            }
        }
        return number;
    }
}
