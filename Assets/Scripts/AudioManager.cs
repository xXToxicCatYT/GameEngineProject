using UnityEngine;
using FMODUnity;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void PlaySound(EventReference soundEvent, Vector3 atWorldPos)
    {
        if (soundEvent.IsNull) return;
        RuntimeManager.PlayOneShot(soundEvent, atWorldPos);
    }
}
