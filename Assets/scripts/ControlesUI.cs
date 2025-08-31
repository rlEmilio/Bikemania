using UnityEngine;

public class ControlesUI : MonoBehaviour
{
    public static ControlesUI Instance;

    [HideInInspector] public bool acelerarPresionado;
    [HideInInspector] public bool frenarPresionado;
    [HideInInspector] public bool inclinarIzqPresionado;
    [HideInInspector] public bool inclinarDerPresionado;

    private void Awake()
    {
        Instance = this;
    }

    // Estos métodos los vas a asignar en los botones
    public void AcelerarDown() => acelerarPresionado = true;
    public void AcelerarUp()   => acelerarPresionado = false;

    public void FrenarDown() => frenarPresionado = true;
    public void FrenarUp()   => frenarPresionado = false;

    public void InclinarIzqDown() => inclinarIzqPresionado = true;
    public void InclinarIzqUp()   => inclinarIzqPresionado = false;

    public void InclinarDerDown() => inclinarDerPresionado = true;
    public void InclinarDerUp()   => inclinarDerPresionado = false;
}
