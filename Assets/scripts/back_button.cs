using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class back_button : MonoBehaviour
{
    public List<string> ScenesToLoad;
    public List<string> ScenesToUnload;

    public void StartGame()
    {
        foreach (var scene in ScenesToLoad)
        {
            SceneManager.LoadScene(scene, LoadSceneMode.Additive);
        }
        foreach (var scene in ScenesToUnload)
        {
            SceneManager.UnloadSceneAsync(scene);
        }
    }
    void Update()
    {
        // Verifica se il tasto sinistro del mouse è stato premuto
        if (Input.GetMouseButtonDown(0))
        {
            // Ottieni un raggio dalla posizione del mouse
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            // Verifica se il raggio colpisce un oggetto
            if (Physics.Raycast(ray, out hit))
            {
                // Verifica se l'oggetto colpito è quello che vuoi gestire
                if (hit.collider.gameObject == this.gameObject)
                {
                    // Esegui l'azione desiderata quando l'oggetto è cliccato
                    StartGame();
                }
            }
        }
    }

    void HandleClick()
    {
        // Personalizza l'azione che vuoi eseguire quando l'oggetto è cliccato
        Debug.Log("Oggetto cliccato!");
    }
}
