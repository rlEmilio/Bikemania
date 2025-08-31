using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro; // o UnityEngine.UI si usas Text clásico

public class MostrarNivelActual : MonoBehaviour
{
    public TextMeshProUGUI textoNivel; // asigna en el Inspector

    void Start()
    {
        int indice = SceneManager.GetActiveScene().buildIndex;
        textoNivel.text = "Nivel " + (indice+1);
    }
}
