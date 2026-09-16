using TMPro;
using UnityEngine;
using System.Collections;

public class Puntuacion : MonoBehaviour
{
    public int puntajePorGolpe = 100;
    private int puntaje = 0;

    public Player playerScript;
    public TextMeshProUGUI textPuntaje;
    public TextMeshProUGUI textCombo; 

    private Vector3 escalaOriginal;



    void Start()
    {
        escalaOriginal = textCombo.rectTransform.localScale;
        ActualizarPuntaje();
        textCombo.gameObject.SetActive(false);
    }

    public void AgregarPuntos(int comboActual)
    {
        int puntosGanados = puntajePorGolpe * comboActual;
        puntaje += puntosGanados;

        ActualizarPuntaje();

        if (comboActual > 1)
        {
            MostrarCombo(comboActual);
        }
    }

    void ActualizarPuntaje()
    {
        textPuntaje.text = puntaje.ToString();
    }

    void MostrarCombo(int combo)
    {
        textCombo.text = "x" + combo;
        textCombo.gameObject.SetActive(true);

        StopAllCoroutines();
        StartCoroutine(AnimarCombo());
    }

    IEnumerator AnimarCombo()
    {
        float duracion = 0.4f;
        float escalaMax = 1.5f;

        textCombo.rectTransform.localScale = escalaOriginal * escalaMax;

        float t = 0f;
        while (t < duracion)
        {
            textCombo.rectTransform.localScale = Vector3.Lerp(textCombo.rectTransform.localScale, escalaOriginal, t / duracion);
            t += Time.deltaTime;
            yield return null;
        }

        textCombo.rectTransform.localScale = escalaOriginal;
    }
    public void OcultarCombo()
    {
        textCombo.gameObject.SetActive(false);
    }

}
