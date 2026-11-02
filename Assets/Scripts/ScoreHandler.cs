using TMPro;
using UnityEngine;

public class ScoreHandler : MonoBehaviour
{
    [SerializeField] private NoteReceiver[] keys;
    [SerializeField] private RatingText ratingText;
    [SerializeField] private TMP_Text ratingCounterText;
    private readonly int[] _ratingCounter = {0, 0, 0, 0, 0};
    private int _notesAttempted = 0;
    
    private float _score;
    
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
        float absScore = Mathf.Abs(offset);
        RatingText rText = Instantiate(ratingText);
        _notesAttempted++;

        if (absScore < 0.045f)
        {
            // Debug.Log("PERFECT");
            _ratingCounter[0]++;
            rText.Initialize("PERFECT");
            AddScore(1.25f);
            return;
        }

        if (absScore < 0.080f)
        {
            // Debug.Log("GREAT");
            _ratingCounter[1]++;
            rText.Initialize("Great");
            AddScore(1.0f);
            return;
        }
        
        if (absScore < 0.125f)
        {
            // Debug.Log("OKAY");
            _ratingCounter[2]++;
            rText.Initialize("Okay");
            AddScore(0.75f);
            return;
        }
        
        // Debug.Log("TRASH");
        _ratingCounter[3]++;
        rText.Initialize("TRASH");
        AddScore(0.5f);
        return;
        
    }

    private void OnMiss()
    {
        // Debug.Log("MISS")
        _ratingCounter[4]++;
        RatingText rText = Instantiate(ratingText);
        rText.Initialize("Miss");
        AddScore(-0.5f);
    }

    private void AddScore(float mult)
    {
        _score += 1000 * mult;
        int highestScore = _notesAttempted * 1250;
        ratingCounterText.text = $"PERFECT: {_ratingCounter[0]}\n" +
                                 $"Great: {_ratingCounter[1]}\n" +
                                 $"Okay: {_ratingCounter[2]}\n" +
                                 $"TRASH: {_ratingCounter[3]}\n" +
                                 $"Miss: {_ratingCounter[4]}\n\n" +
                                 $"Score: {_score:F0}\n" + 
                                 $"Accuracy: {(_score / highestScore * 100):F2}%";
        // Debug.Log(_score); 
    }
}
