using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GridSquare : MonoBehaviour
{
    public Image normalImage;
    public List<Sprite> normalImages;

    public void SetImage(bool setFirstImage)
    {
        if (normalImage == null || normalImages == null || normalImages.Count < 2)
        {
            return;
        }
        normalImage.sprite = setFirstImage ? normalImages[0] : normalImages[1];
    }
}
