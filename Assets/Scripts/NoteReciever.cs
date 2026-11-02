using System;
using System.Collections.Generic;
using UnityEngine;

public class NoteReceiver : MonoBehaviour
{
    
    private Transform _transform;
    private Vector3 _originalScale;
    [SerializeField] private float shrinkSpeed;
    [SerializeField] private float hitWindow;

    public event Action<float> NoteHit;
    public event Action NoteMiss;
    
    private readonly Queue<Note> _notes = new Queue<Note>();
    
    private void Start()
    {
        _transform = GetComponent<Transform>();
        _originalScale = _transform.localScale;
    }

    private void Update()
    {
        _transform.localScale = Vector3.Lerp(_transform.localScale, 
                                             _originalScale, 
                                           Time.deltaTime * shrinkSpeed);
        LostNotes();
    }

    private void LostNotes()
    {
        float time = TrackHandler.Instance.GetTrackTime();
        while (_notes.Count > 0)
        {
            Note frontNote = _notes.Peek();
            // ReSharper disable once Unity.PerformanceCriticalCodeNullComparison
            if (frontNote == null)
            {
                _notes.Dequeue();
                continue;
            }

            if (time <= frontNote.GetHitTime() + hitWindow)
            {
                break;
            }
            
            _notes.Dequeue();
            NoteMiss?.Invoke();
        }
    }

    public void AddNote(Note note) { _notes.Enqueue(note); }

    public void Hit()
    {
        HitEffect();
        LostNotes();

        if (_notes.Count == 0) return;

        Note note = _notes.Peek();
        float noteOffset = TrackHandler.Instance.GetTrackTime() - note.GetHitTime();
        if (Mathf.Abs(noteOffset) > hitWindow) { return; }
        
        _notes.Dequeue();
        note.Hit();
        NoteHit?.Invoke(noteOffset);
        // Debug.Log(noteOffset);
    }
    private void HitEffect()
    {
        _transform.localScale = new Vector3(_originalScale.x * 0.8f, 
                                            _originalScale.y * 0.8f, 
                                            _originalScale.z * 0.8f);
    }
}
