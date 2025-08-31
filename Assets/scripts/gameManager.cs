using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);  // Esto hace que no se destruya al cambiar escena
        }
        else
        {
            Destroy(gameObject);  // Evita duplicados si cargas la escena base otra vez
        }
    }
}
