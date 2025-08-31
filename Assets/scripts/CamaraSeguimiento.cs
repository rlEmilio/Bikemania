using UnityEngine;

public class CamaraSeguimiento : MonoBehaviour
{
    public Transform objetivo;

    // Offset fijo para que la cámara quede adelantada y un poco arriba respecto al jugador
   private Vector3 offset = new Vector3(1.12f, 1.36f, -10f);

    void Update()
    {
        if (objetivo != null)
        {
            transform.position = objetivo.position + offset;
        }
    }
}
