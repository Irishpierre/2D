using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoadTrigger : MonoBehaviour
{

    [SerializeField] private string loadSceneString;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject == Player.Instance.gameObject)
        {
            SceneManager.LoadScene(loadSceneString);
            Player.Instance.SetSpawnPosition();
        }
    }

}
