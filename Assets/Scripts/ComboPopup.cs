using UnityEngine;
using TMPro;

public class ComboPopup : MonoBehaviour
{
    public float duracion = 1f;
    public Vector3 offset = new Vector3(0, 1f, 0);
    public float velocidadFlotacion = 1f;

    private TextMeshProUGUI texto;
    private float tiempo;

    void Start()
    {
        texto = GetComponent<TextMeshProUGUI>();
        tiempo = 0f;
    }

    void Update()
    {
        transform.position += Vector3.up * velocidadFlotacion * Time.deltaTime;
        tiempo += Time.deltaTime;

        if (tiempo >= duracion)
        {
            Destroy(gameObject);
        }
    }

    public void SetTexto(string valor)
    {
        texto = GetComponent<TextMeshProUGUI>();
        texto.text = valor;
    }
}
