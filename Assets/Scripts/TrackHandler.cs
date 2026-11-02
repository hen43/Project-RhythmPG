using UnityEngine;

public class TrackHandler : MonoBehaviour
{
    public static TrackHandler Instance { get; private set; }
    private float _trackTime; // replace this with audio time at some point
    
    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }
    
    private void Start()
    {
        _trackTime = 0f;
    }
    
    private void Update()
    {
        _trackTime += Time.deltaTime;
    }

    public float GetTrackTime() { return _trackTime; }
}
