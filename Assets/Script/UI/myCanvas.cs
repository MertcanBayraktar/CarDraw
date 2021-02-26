using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class myCanvas : MonoBehaviour
{
    public Canvas _myCanvas;
    public static Vector2 canvasSize;
    private void Awake()
    {
        _myCanvas = GetComponent<Canvas>();
        RectTransform r = _myCanvas.GetComponent<RectTransform>();
        canvasSize = new Vector2(r.rect.width, r.rect.height);
    }
}
