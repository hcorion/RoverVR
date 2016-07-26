using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameStart : MonoBehaviour
{
    public GameManager gameManager;


    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Blocks"))
        {
            new WaitForSeconds(1);
            SceneManager.LoadSceneAsync(gameManager.getMainSceneName(), LoadSceneMode.Single);
        }


    }
}

