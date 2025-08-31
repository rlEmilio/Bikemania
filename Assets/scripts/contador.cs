using UnityEngine;
using TMPro;

public class contador : MonoBehaviour
{
    public TextMeshProUGUI textoTiempoTotal; // Texto en pantalla que muestra tiempo total
    private float tiempoNivel = 0f;          // Tiempo acumulado del nivel

    void Update()
    {
        // Acumula el tiempo local del nivel
        tiempoNivel += Time.deltaTime;

        // Muestra el tiempo total acumulado global en pantalla (HUD)
        if (gameTimeManager.Instance != null && textoTiempoTotal != null)
        {
            float tiempoTotal = gameTimeManager.Instance.tiempoTotal;
            textoTiempoTotal.text = FormatearTiempo(tiempoTotal);
        }
    }

    // Método público para que otros scripts puedan obtener el tiempo local actual
    public float GetTiempoNivel()
    {
        return tiempoNivel;
    }

    // Método para formatear tiempo a mm:ss
    private string FormatearTiempo(float tiempo)
    {
        int minutos = Mathf.FloorToInt(tiempo / 60f);
        int segundos = Mathf.FloorToInt(tiempo % 60f);
        return string.Format("{0:00}:{1:00}", minutos, segundos);
    }
}
