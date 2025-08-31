using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    private string nivelActual = "";

    void Start()
    {
        // Al iniciar, carga el primer nivel
        StartCoroutine(CargarNivel("Nivel_1 1"));
    }

    // Método público para cambiar de nivel desde otros scripts
    public void CambiarANivel(string nuevoNivel)
    {
        StartCoroutine(CambiarNivelCoroutine(nivelActual, nuevoNivel));
    }

    // Coroutine para cargar un nivel aditivo
    IEnumerator CargarNivel(string nombreNivel)
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(nombreNivel, LoadSceneMode.Additive);
        while (!asyncLoad.isDone)
        {
            yield return null;
        }

        Scene nivel = SceneManager.GetSceneByName(nombreNivel);
        SceneManager.SetActiveScene(nivel);

        nivelActual = nombreNivel;
    }

    // Coroutine para cambiar de nivel: cargar nuevo y descargar viejo
    IEnumerator CambiarNivelCoroutine(string nivelViejo, string nivelNuevo)
    {
        // Carga el nuevo nivel
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(nivelNuevo, LoadSceneMode.Additive);
        while (!asyncLoad.isDone)
        {
            yield return null;
        }

        // Descarga el nivel viejo si hay uno cargado
        if (!string.IsNullOrEmpty(nivelViejo))
        {
            AsyncOperation asyncUnload = SceneManager.UnloadSceneAsync(nivelViejo);
            while (!asyncUnload.isDone)
            {
                yield return null;
            }
        }

        // Cambia la escena activa al nuevo nivel
        Scene nuevoNivel = SceneManager.GetSceneByName(nivelNuevo);
        SceneManager.SetActiveScene(nuevoNivel);

        nivelActual = nivelNuevo;
    }
}
