using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using System.Globalization;

public class pasarNivel : MonoBehaviour
{
    public contador contadorScript;
    public Transform jugador;
    public GameObject panelContinuar;
    public GameObject panelPerder;
    public Transform metaTransform;
    public TextMeshProUGUI contadorTexto;
    public TextMeshProUGUI textoFinal;
    public TextMeshProUGUI textoChoque;
    public AudioSource motorAudio;
    public AudioSource acelerarAudio;

    // Referencias para detección de colisión
    public GameObject cuerpoMoto;
    public GameObject ruedaDelantera;
    public GameObject ruedaTrasera;

    private bool nivelCompletado = false;
    private bool haPerdido = false;

    private int estrellasActuales = 0; // Guardamos estrellas calculadas en el nivel actual

    public Image[] estrellasUI;
    public Sprite estrellaLlenaSprite;
    public Sprite estrellaVaciaSprite;

    void Update()
    {
        if (!nivelCompletado && jugador.position.x > metaTransform.position.x + 5f)
        {
            CompletarNivel();
        }

        if (haPerdido && Input.GetKey(KeyCode.Return))
        {
            Reiniciar();
        }

        if (nivelCompletado && Input.GetKey(KeyCode.Return))
        {
            Continuar();
        }
        
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (haPerdido || nivelCompletado) return;

        GameObject otroObjeto = collision.gameObject;

        if (otroObjeto == ruedaDelantera || otroObjeto == ruedaTrasera)
            return;

        Perder();
    }

   private void CompletarNivel()
{
    // Obtener tiempo local en segundos desde el contador (no del texto)
    float tiempoNivelSegundos = contadorScript != null ? contadorScript.GetTiempoNivel() : 0f;

    // Formatear para mostrar
    string tiempoNivelTexto = FormatearTiempo(tiempoNivelSegundos);
    string tiempoTotal = FormatearTiempo(gameTimeManager.Instance.tiempoTotal);

    int nivelIndex = SceneManager.GetActiveScene().buildIndex;
    estrellasActuales = CalcularEstrellas(tiempoNivelSegundos, nivelIndex);

    textoFinal.text = $"Nivel completado en: {tiempoNivelTexto}\n\nTiempo total: {tiempoTotal}\n\n";

    // Mostrar estrellas en UI
    MostrarEstrellasUI(estrellasActuales);

    nivelCompletado = true;
    panelContinuar.SetActive(true);
    Time.timeScale = 0f;
    acelerarAudio.Stop();
    motorAudio.Stop();
}

    public void Continuar()
    {
        Time.timeScale = 1f;
        StarsManager.Instance.AgregarEstrellas(estrellasActuales);
        if (SceneManager.GetActiveScene().buildIndex == 19)
        {
            SceneManager.LoadScene(0);
        }
        else
        {
         SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }

    }

    public void Reiniciar()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }


    private void MostrarEstrellasUI(int cantidad)
    {
        for (int i = 0; i < estrellasUI.Length; i++)
        {
            if (i < cantidad)
                estrellasUI[i].sprite = estrellaLlenaSprite;
            else
                estrellasUI[i].sprite = estrellaVaciaSprite;
        }
    }

    string FormatearTiempo(float tiempo)
    {
        int minutos = Mathf.FloorToInt(tiempo / 60f);
        int segundos = Mathf.FloorToInt(tiempo % 60f);
        return string.Format("{0:00}:{1:00}", minutos, segundos);
    }

    float ConvertirTiempoATiempoSegundos(string tiempoTexto)
    {
        // Asume formato mm:ss
        if (System.TimeSpan.TryParseExact(tiempoTexto, @"mm\:ss", CultureInfo.InvariantCulture, out System.TimeSpan tiempo))
        {
            return (float)tiempo.TotalSeconds;
        }
        else
        {
            Debug.LogWarning("Formato de tiempo inválido: " + tiempoTexto);
            return 0f;
        }
    }

    [System.Serializable]
    public class EstrellasPorNivel
    {
        public float tiempo3Estrellas;
        public float tiempo2Estrellas;
        public float tiempo1Estrella;
    }

    public EstrellasPorNivel[] nivelesEstrellas;

    private int CalcularEstrellas(float tiempoSegundos, int nivelIndex)
    {
        if (nivelIndex < 0 || nivelIndex >= nivelesEstrellas.Length)
            return 0;

        EstrellasPorNivel nivel = nivelesEstrellas[nivelIndex];

        if (tiempoSegundos <= nivel.tiempo3Estrellas) return 3;
        else if (tiempoSegundos <= nivel.tiempo2Estrellas) return 2;
        else if (tiempoSegundos <= nivel.tiempo1Estrella) return 1;
        else return 0;
    }

    private void Perder()
    {
        float tiempoNivelSegundos = contadorScript != null ? contadorScript.GetTiempoNivel() : 0f;
        string tiempoNivel = FormatearTiempo(tiempoNivelSegundos);
        string tiempoTotal = FormatearTiempo(gameTimeManager.Instance.tiempoTotal);

        textoChoque.text = "Has chocado.\n\nTiempo nivel: " + tiempoNivel + "\n\nTiempo total: " + tiempoTotal;
        haPerdido = true;
        panelPerder.SetActive(true);
        Time.timeScale = 0f;
        acelerarAudio.Stop();
        motorAudio.Stop();

}

}
