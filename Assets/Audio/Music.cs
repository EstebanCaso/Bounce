using UnityEngine;

public class Music : MonoBehaviour
{

    private AudioSource musicSource;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (musicSource != null)
        {
            musicSource.loop = true;
            musicSource.Play();
        }
        else
        {
            Debug.LogWarning("No hay AudioSource asignado. Por favor, asigna uno en el inspector.");
        }
    }

    // Update is called once per frame
    void Update()
    {
        

    }
}
