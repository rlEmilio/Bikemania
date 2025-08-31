using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParedIzquierda : MonoBehaviour
{
    void Start()
    {
        // Obtiene la posición del borde izquierdo de la cámara principal
        Camera cam = Camera.main;
        float z = transform.position.z; // Mantén la profundidad actual
        Vector3 worldLeft = cam.ViewportToWorldPoint(new Vector3(0, 0.5f, cam.nearClipPlane));
        transform.position = new Vector3(worldLeft.x-8 - 0.5f, 0, z); // Ajusta el -0.5f según el tamaño del collider

        // Ajusta tamaño del BoxCollider2D si es necesario
        BoxCollider2D col = GetComponent<BoxCollider2D>();
        if (col != null)
        {
            col.size = new Vector2(1f, 100f); // Ancho, Alto
        }
    }
}
