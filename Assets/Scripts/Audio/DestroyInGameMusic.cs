using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyInGameMusic : MonoBehaviour
{
    private void Awake()
    {
        if (InGameMusic.instance != null) Destroy(InGameMusic.instance.gameObject);
    }
}
