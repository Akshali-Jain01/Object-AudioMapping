using UnityEngine;

public class ObjectAudioController : MonoBehaviour
{
    public GameObject[] objects; // Array to hold all objects
    public AudioClip[] audioClips; // Array to hold all audio clips

    private AudioSource audioSource;
    private int currentIndex = 0;

    void Start()
    {
        // Ensure we have as many audio clips as objects
        if (objects.Length != audioClips.Length)
        {
            Debug.LogError("Objects and AudioClips arrays must be of the same length!");
            return;
        }

        // Initialize audio source
        audioSource = gameObject.AddComponent<AudioSource>();
        PlayNext();
    }

    void PlayNext()
    {
        if (currentIndex < objects.Length)
        {
            // Deactivate previous object
            if (currentIndex > 0)
            {
                objects[currentIndex - 1].SetActive(false);
            }

            // Activate current object and play its audio
            objects[currentIndex].SetActive(true);
            audioSource.clip = audioClips[currentIndex];
            audioSource.Play();

            // Listen for audio completion
            Invoke("OnAudioComplete", audioSource.clip.length);
        }
    }

    void OnAudioComplete()
    {
        currentIndex++;
        PlayNext();
    }
}
