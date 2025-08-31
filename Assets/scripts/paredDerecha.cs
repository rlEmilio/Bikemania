using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class ParedDerecha : MonoBehaviour
{
    void Start()
    {
        Camera cam = Camera.main;
        float z = transform.position.z;
        Vector3 worldRight = cam.ViewportToWorldPoint(new Vector3(1, 0.5f, cam.nearClipPlane));
        transform.position = new Vector3(worldRight.x + 140.1f, 0, z); // Ajusta el +0.5f según el tamaño del collider

        BoxCollider2D col = GetComponent<BoxCollider2D>();
        if (col != null)
        {
            col.size = new Vector2(1f, 100f); // Ancho, Alto
        }
    }
}