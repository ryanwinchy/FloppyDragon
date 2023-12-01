using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneSwapper : MonoBehaviour
{

    [SerializeField] private Button start;

    // Start is called before the first frame update
    void Start()
    {
        start.onClick.AddListener(TaskOnClick);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

   /* public void StartNow()
    {
        SceneManager.LoadScene(1);
    }*/

    void TaskOnClick()
    {
        SceneManager.LoadScene(1);
    }

}
