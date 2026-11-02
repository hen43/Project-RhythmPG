using UnityEngine;

public class AudioHandler : MonoBehaviour
{
    [SerializeField] private NoteReceiver[] keys;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip hitSound;

    void OnEnable()
    {
        foreach (var key in keys)
        {
            key.NoteHit += OnHit;
            key.NoteMiss += OnMiss;
        }
    }

    void OnDisable()
    {
        foreach (var key in keys)
        {
            key.NoteHit -= OnHit;
            key.NoteMiss -= OnMiss;
        }
    }

    private void OnHit(float offset)
    {
        audioSource.PlayOneShot(hitSound);
    }

    private void OnMiss()
    {
        // nothing
    }
}
