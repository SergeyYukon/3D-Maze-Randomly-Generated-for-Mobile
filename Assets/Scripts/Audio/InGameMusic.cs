using UnityEngine;

public class InGameMusic : MonoBehaviour
{
    public static InGameMusic instance { get; private set; }

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            return;
        }
        Destroy(gameObject);
    }
}
