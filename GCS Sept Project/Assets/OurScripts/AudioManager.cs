using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance {get; private set;}

    [SerializeField] private Transform camera;


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    public void PlaySound(AudioClip audioClip, float volume = 1f)
    {
        AudioSource.PlayClipAtPoint(audioClip, camera.position, volume);
    }

}
