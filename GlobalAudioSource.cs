using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalAudioSource : MonoBehaviour
{
    #region GlobalCrap
    //For making this a global Script
    public static GlobalAudioSource Instance;
    void Awake()
    {
        if (Instance == null)
        {
            DontDestroyOnLoad(gameObject);
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }
    #endregion
}
