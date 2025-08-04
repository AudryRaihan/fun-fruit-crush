using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CameraScalarResponsive : MonoBehaviour
{
    private Board board;
    public float padding = 2;
    public float yOffset = 1;

    void Start()
    {
        board = FindObjectOfType<Board>();
        if (board != null)
        {
            RepositionCamera(board.width - 1, board.height - 1);
        }
    }

    void RepositionCamera(float x, float y)
    {
        Vector3 tempPosition = new Vector3(x / 2, y / 2 + yOffset, -10);
        transform.position = tempPosition;

        float screenAspect = (float)Screen.width / Screen.height;
        float boardAspect = (float)board.width / board.height;

        float orthoSizeWidth = (board.width / 2 + padding) / screenAspect;
        float orthoSizeHeight = board.height / 2 + padding + yOffset;

        Camera.main.orthographicSize = Mathf.Max(orthoSizeWidth, orthoSizeHeight);
    }
}
