using UnityEngine;

public class NoteSpawner : MonoBehaviour
{
    [SerializeField] private NoteReceiver[] keys;
    [SerializeField] private Note notePrefab;
    [SerializeField] private float targetTravelTime = 2f;
    [SerializeField] private float spawnInterval = 1f;

    private float _timer;

    void Update()
    {
        _timer += Time.deltaTime;
        if (_timer < spawnInterval) return;
        _timer -= spawnInterval;

        NoteReceiver lane = keys[Random.Range(0, keys.Length)];
        Note note = Instantiate(notePrefab);
        note.Initialize(TrackHandler.Instance.GetTrackTime() + targetTravelTime, lane.transform);
        lane.AddNote(note);
    }
}