using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class StarsManager : MonoBehaviour
{
    public static StarsManager Instance;

    public int estrellasTotales = 0;
    public TextMeshProUGUI contadorEstrellas;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Persiste entre escenas
            SceneManager.sceneLoaded += OnSceneLoaded; // Suscribir al evento
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnDestroy()
    {
        if (Instance == this)
        {
            SceneManager.sceneLoaded -= OnSceneLoaded; // Desuscribir al destruir
        }
    }

   private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
{
    GameObject go = GameObject.Find("textoContadorEstrellas"); // nombre exacto en la jerarquía
    if (go != null)
    {
        contadorEstrellas = go.GetComponent<TextMeshProUGUI>();
        contadorEstrellas.text = estrellasTotales.ToString();
    }
    else
    {
        Debug.LogWarning("No se encontró el texto contador de estrellas en la escena " + scene.name);
    }
}

    public void AgregarEstrellas(int cantidad)
    {
        estrellasTotales += cantidad;
        if (contadorEstrellas != null)
        {
            contadorEstrellas.text = "" + estrellasTotales;
        }
    }
}
