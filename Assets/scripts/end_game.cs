using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class end_game : MonoBehaviour
{
    public List<string> ScenesToLoad;
    public List<string> ScenesToUnload;
    // Start is called before the first frame update
    public void Check()
    {
        if (GameManager.Instance.ListaStringhe.Count == 0 && FindObjectOfType<Player>().GetComponent<Player>().Money==0)
        {
            GameManager.Instance.LoadUnloadScenes(ScenesToLoad, ScenesToUnload);
        }

    }
}
