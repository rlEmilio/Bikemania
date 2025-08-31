using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;
public class gameTimeManager : MonoBehaviour
{
    public static gameTimeManager Instance;

    public float tiempoTotal = 0f;

    void Awake()
    {
        // Si ya existe otra instancia, destruir esta
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;

        // Que no se destruya al cambiar de escena
        DontDestroyOnLoad(gameObject);
    }

    void Update()
    {
        // Sumar el tiempo transcurrido cada frame
        tiempoTotal += Time.deltaTime;
    }
}
