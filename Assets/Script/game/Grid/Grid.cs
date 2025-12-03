using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour
{
    public ShapeStorage shapeStorage;
    public int columns = 0;
    public int rows = 0;
    public float squareGap = 0.1f;
    public GameObject gridSquare;  
    public Vector2 startPos = new Vector2(0.0f, 0.0f);
    public float squareScale = 0.5f;
    public float everySquareOffset = 0f;
    
    private Vector2 offset = new Vector2(0.0f, 0.0f);
    private List<GameObject> _gridSquares = new List<GameObject>();
    private LIneIndicator _lineIndicator;

    private void OnDisable()
    {
        GameEvent.CheckIfShapeCanBePlaced -= CheckIfShapeCanBePlaced;
    }

    private void OnEnable()
    {
        GameEvent.CheckIfShapeCanBePlaced += CheckIfShapeCanBePlaced;
    }
    void Start()
    {
        _lineIndicator = GetComponent<LIneIndicator>();
        CreateGrid();
    }
    

    private void CreateGrid()
    {
        SpawnGridSquare();
        SetGridSquarePosition();
    }

    private void SpawnGridSquare()
    {
        int square_index = 0;
        for (var row = 0; row < rows; ++row)
        {
            for (var column = 0; column < columns; ++column)
            {
                var squareObj = Instantiate(gridSquare) as GameObject;
                squareObj.transform.SetParent(this.transform);
                squareObj.transform.localScale = new Vector3(squareScale, squareScale, squareScale);
                var gridSquareComp = squareObj.GetComponent<GridSquare>();
                gridSquareComp.SquareIndex = square_index;
                gridSquareComp.SetImage(_lineIndicator.GetGridSquareIndex(square_index) % 2 == 0);
                _gridSquares.Add(squareObj);
                square_index++;
            }
        }
    }

    private void SetGridSquarePosition()
    {
        int column_number = 0;
        int row_number = 0;
        Vector2 square_gap_number = new Vector2(0.0f, 0.0f);
        bool row_moved = false;

        var square_rect = _gridSquares[0].GetComponent<RectTransform>();

        offset.x = square_rect.rect.width * square_rect.transform.localScale.x + everySquareOffset;
        offset.y = square_rect.rect.height * square_rect.transform.localScale.y + everySquareOffset;

        foreach (GameObject square in _gridSquares)
        {
            if (column_number >= columns)
            {
                square_gap_number.x = 0;
                // Move to next row
                column_number = 0;
                row_number++;
                row_moved = false;   
            }
            
            var pos_x_offset = offset.x * column_number + (square_gap_number.x * squareGap);
            var pos_y_offset = offset.y * row_number + (square_gap_number.y * squareGap);

            if (column_number > 0 && column_number % 3 == 0)
            {
                square_gap_number.x++;
                pos_x_offset += squareGap;
            }

            if (row_number > 0 && row_moved == false)
            {
                row_moved = true;
                square_gap_number.y++;
                pos_y_offset += squareGap;
            }

            square.GetComponent<RectTransform>().anchoredPosition = new Vector2(startPos.x + pos_x_offset, startPos.y - pos_y_offset);
            square.GetComponent<RectTransform>().localPosition = new Vector3(square.GetComponent<RectTransform>().localPosition.x, square.GetComponent<RectTransform>().localPosition.y, 0.0f);
            column_number++;
        }
    }

    private void CheckIfShapeCanBePlaced()
    {
        var selectedIndexes = new List<int>();

        foreach (var square in _gridSquares)
        {
            var gridSquare = square.GetComponent<GridSquare>();

            if (gridSquare.selected && !gridSquare.SquareOccupied)
            {
                selectedIndexes.Add(gridSquare.SquareIndex);
                gridSquare.selected = false;
            }
        }
        var currentSelectedShape = shapeStorage.GetCurenntSelectedShape();
        if (currentSelectedShape == null)
            return;

        if (currentSelectedShape.totalSquareNumber == selectedIndexes.Count)
        {
            foreach (var squareIndex in selectedIndexes)
            {
                _gridSquares[squareIndex].GetComponent<GridSquare>().PlaceShapeOnBoard();
            }
            var shapeLeft = 0;
            foreach (var shape in shapeStorage.shapelist)
            {
                if (shape.IsOnStartPosition() && shape.IsAnyOfShapeSquareActive())
                {
                    shapeLeft++;
                }              
            }

            if (shapeLeft == 0)
            {
                GameEvent.RequestNewShape();
            }
            else
            {
                GameEvent.SetShapeInactive();
            }
            
        }
        else
        {
            GameEvent.MoveShapeBackToStartPosition();
        }
    }    
}

