using UnityEngine;
using UnityEngine.UI;

public class controlSonido : MonoBehaviour
{
    public Image iconoImagen;
    public Color colorSonidoActivo = Color.white;
    public Color colorSonidoMuteado = Color.black;

    private bool sonidoActivo = true;

    void Start()
    {
        if (PlayerPrefs.HasKey("Sonido"))
        {
            sonidoActivo = PlayerPrefs.GetInt("Sonido") == 1;
        }

        AudioListener.volume = sonidoActivo ? 1f : 0f;
        ActualizarColorIcono();
    }

    public void AlternarSonido()
    {
        sonidoActivo = !sonidoActivo;
        AudioListener.volume = sonidoActivo ? 1f : 0f;

        ActualizarColorIcono();

        PlayerPrefs.SetInt("Sonido", sonidoActivo ? 1 : 0);
        PlayerPrefs.Save();
    }

    private void ActualizarColorIcono()
    {
        if (iconoImagen != null)
        {
            iconoImagen.color = sonidoActivo ? colorSonidoActivo : colorSonidoMuteado;
        }
    }
}
