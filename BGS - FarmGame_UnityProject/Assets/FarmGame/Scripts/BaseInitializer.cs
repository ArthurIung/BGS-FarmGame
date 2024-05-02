using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseInitializer : MonoBehaviour
{
    /// <summary>
    /// Boolean that show this class has been initialized
    /// </summary>
    bool isInitialized;

    public bool IsInitialized
    {
        get 
        {
            if (!isInitialized)
                Debug.LogWarning($"The gameObject {gameObject.name} has not been initialized and is trying to execute");

            return isInitialized;
        }
    }


    public virtual IEnumerator Cor_Initialize() 
    {
        yield return null;
        isInitialized = true;
    }
}
