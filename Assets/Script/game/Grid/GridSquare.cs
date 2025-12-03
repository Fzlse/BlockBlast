using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class GridSquare : MonoBehaviour
{
    public Image hoverImage;
    public Image activeImage;
    public Image normalImage;
    public List<Sprite> normalImages;
    public bool selected { get; set; }
    public int SquareIndex { get; set; }
    public bool SquareOccupied { get; set; }

    void Start()
    {
        selected = false;
        SquareOccupied = false;
    }
    //temporary function to test grid square usability
    public bool CanWeUseThisGridSquare()
    {
        return hoverImage.gameObject.activeSelf;
    }
    public void ActivateSquare()
    {
        hoverImage.gameObject.SetActive(false);
        activeImage.gameObject.SetActive(true);
        selected = true;
        SquareOccupied = true;

    }

    public void SetImage(bool setFirstImage)
    {
        if (normalImage == null || normalImages == null || normalImages.Count < 2)
        {
            return;
        }
        normalImage.sprite = setFirstImage ? normalImages[0] : normalImages[1];
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        hoverImage.gameObject.SetActive(true);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        hoverImage.gameObject.SetActive(true);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        hoverImage.gameObject.SetActive(false);
    }
}
