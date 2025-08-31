using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class controlMoto : MonoBehaviour
{
    public AudioSource motorAudio;
    public AudioSource acelerarAudio;
    public WheelJoint2D ruedaDelantera;
    public WheelJoint2D ruedaTrasera;

    public float maxMotorSpeed = -1500f;
    public float aceleracionMotor = 700f;

    private JointMotor2D motorDelantero;
    private JointMotor2D motorTrasero;

    private float velocidadActual = 0f;
    public float maxAngularVelocity = 150f;
    public float torqueFijo = 300f;
    public float torqueResistencia = 0.9f;

    private Rigidbody2D rb;

    private bool puedeReproducirSonido = true;
    private float tiempoEsperado = 0f;

    void Start()
    {
        motorAudio.Play();
        rb = GetComponent<Rigidbody2D>();

        if (ruedaDelantera != null)
            motorDelantero = ruedaDelantera.motor;

        if (ruedaTrasera != null)
            motorTrasero = ruedaTrasera.motor;
    }

    void Update()
    {
        if (ruedaDelantera == null || ruedaTrasera == null) return;

        // --- INPUT COMBINADO TECLADO + UI ---
        bool acelerar = Input.GetKey(KeyCode.UpArrow) || (ControlesUI.Instance != null && ControlesUI.Instance.acelerarPresionado);
        bool frenar   = Input.GetKey(KeyCode.DownArrow) || (ControlesUI.Instance != null && ControlesUI.Instance.frenarPresionado);
        // -------------------------------------

        // Control de reproducción única del sonido de aceleración
        if (Time.time >= tiempoEsperado)
            puedeReproducirSonido = true;

        if ((Input.GetKeyDown(KeyCode.UpArrow) || (ControlesUI.Instance != null && ControlesUI.Instance.acelerarPresionado)) && puedeReproducirSonido)
        {
            acelerarAudio.Play();
            puedeReproducirSonido = false;
            tiempoEsperado = Time.time + acelerarAudio.clip.length;
        }

        // Movimiento de motor (acelerar/frenar)
        if (acelerar)
        {
            velocidadActual = Mathf.MoveTowards(velocidadActual, maxMotorSpeed, aceleracionMotor * Time.deltaTime);
        }
        else if (frenar)
        {
            velocidadActual = Mathf.MoveTowards(velocidadActual, -maxMotorSpeed, aceleracionMotor * Time.deltaTime);
        }
        else
        {
            velocidadActual = Mathf.MoveTowards(velocidadActual, 0f, aceleracionMotor * 2f * Time.deltaTime);
        }

        motorDelantero.motorSpeed = velocidadActual;
        motorTrasero.motorSpeed = velocidadActual;

        ruedaDelantera.motor = motorDelantero;
        ruedaTrasera.motor = motorTrasero;
    }

    void FixedUpdate()
    {
        if (ruedaDelantera == null || ruedaTrasera == null) return;

        // --- INPUT COMBINADO TECLADO + UI ---
        float inclinacionInput = 0f;
        if (Input.GetKey(KeyCode.LeftArrow) || (ControlesUI.Instance != null && ControlesUI.Instance.inclinarIzqPresionado)) inclinacionInput = -1f;
        else if (Input.GetKey(KeyCode.RightArrow) || (ControlesUI.Instance != null && ControlesUI.Instance.inclinarDerPresionado)) inclinacionInput = 1f;
        // -------------------------------------

        // Torque para inclinar, independiente del frame rate
        float torque = -inclinacionInput * torqueFijo;
        rb.AddTorque(torque * Time.fixedDeltaTime, ForceMode2D.Impulse);

        // Limitar velocidad angular
        rb.angularVelocity = Mathf.Clamp(rb.angularVelocity, -maxAngularVelocity, maxAngularVelocity);

        // Aplicar resistencia suave
        rb.angularVelocity = Mathf.Lerp(rb.angularVelocity, 0f, 1f - torqueResistencia);
    }
}
