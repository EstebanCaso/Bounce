using UnityEngine;
using UnityEngine.SceneManagement;
public class CambioNivel : MonoBehaviour
{

    public int indiceNivel;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            CambiarNivel(indiceNivel);
        }

        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            CambiarNivel(indiceNivel);
        }
    }



    public void CambiarNivel(int indice)
    {
        SceneManager.LoadScene(indice);
    }
}
