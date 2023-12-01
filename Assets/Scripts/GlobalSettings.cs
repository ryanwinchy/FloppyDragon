using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalSettings : MonoBehaviour
{

    private static GlobalSettings instance;

    public static float selectedVolume = 0.25f;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // Keeps the GameObject alive between scenes
        }
        else
        {
            Destroy(gameObject); // Ensures only one AudioManager exists
        }
    }

    public void SaveSliderValue(float value)
    {
        selectedVolume = value;
    }

    public float GetVolume()
    {
        return selectedVolume;
    }
}
