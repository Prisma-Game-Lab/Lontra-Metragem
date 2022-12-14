using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class CameraController : MonoBehaviour
{
    public Tilemap tilemap;
    public SpriteRenderer sprite;
    void Start()
    {
        ResizeCamera();
    }

    public void ResizeCamera()
    {
        Vector3 spriteSize = sprite.bounds.size;
        float diffX = tilemap.size.x / spriteSize.x;
        float diffY = tilemap.size.y + 1.0f / spriteSize.y;
        sprite.gameObject.transform.localScale = new Vector3(diffX, diffY, 1.0f);

        float screenRatio = Screen.safeArea.width / Screen.safeArea.height;
        float targetRatio = sprite.bounds.size.x / sprite.bounds.size.y;

        if (screenRatio >= targetRatio)
        {
            Camera.main.orthographicSize = sprite.bounds.size.y * 0.5f;
        }
        else
        {
            float diff = targetRatio / screenRatio;
            Camera.main.orthographicSize = sprite.bounds.size.y * 0.5f * diff;
        }

        Camera.main.transform.position = new Vector3(-0.2f, 0.5f, -10.0f);
    }
}
