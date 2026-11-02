using UnityEngine;

public class Note : MonoBehaviour
{
    [SerializeField] private float scrollSpeed = 5f;
    private const float DestroyHeight = -15f;

    private float _hitTime;
    private float _hitY;

    public void Initialize(float hitTime, Transform key)
    {
        _hitTime = hitTime;
        _hitY = key.position.y;

        var pos = key.position;
        pos.y = GetY();
        transform.position = pos;
    }

    void Update()
    {
        var pos = transform.position;
        pos.y = GetY();
        transform.position = pos;

        if (pos.y < DestroyHeight)
            Destroy(gameObject);
    }

    private float GetY()
    {
        return _hitY + (_hitTime - TrackHandler.Instance.GetTrackTime()) * scrollSpeed;
    }

    public float GetHitTime()
    {
        return _hitTime;
    }

    public void Hit()
    {
        Destroy(gameObject);
    }
}