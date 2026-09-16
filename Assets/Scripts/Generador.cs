using UnityEngine;

public class Generador : MonoBehaviour
{
   
    public GameObject prefabEnemigo;
    private float randomX;
    private float randomY;
    public int enemyCount=0;
    public int enemy = 180;

    private Transform playerTransform;

    void Start()
    {
        Application.targetFrameRate = 90;

        GameObject player = GameObject.FindWithTag("Player");
        playerTransform = player.transform;
        CrearEnemigo();
       
    }
    void Update()
    {
        NewEnemy();
    }
    public void CrearEnemigo()
    {

       
        for (int i = 0; i < enemy; i++)
        {
            float offsetX = Random.Range(-199f, 199f);
            float offsetY = Random.Range(-99f, 99f);

            Vector3 spawnPosition = new Vector3(offsetX, offsetY, 0f);
            spawnPosition += playerTransform.position;
            GameObject x = Instantiate(prefabEnemigo);
            x.transform.position = spawnPosition;
            enemyCount++;

        }
        


    }
    public void NewEnemy()
    {
        if (enemyCount<enemy)
        {
            Debug.Log("Enemigo creado");
            float offsetX = Random.Range(-40f, 40f);
            float offsetY = Random.Range(-40f, 40f);

            Vector3 spawnPosition = new Vector3(offsetX, offsetY, 0f);
            spawnPosition += playerTransform.position;
            GameObject x = Instantiate(prefabEnemigo);
            x.transform.position = spawnPosition;
            enemyCount++;
            
           

        }

    }

}
