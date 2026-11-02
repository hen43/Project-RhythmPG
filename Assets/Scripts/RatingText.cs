using TMPro;
using UnityEngine;

public class RatingText : MonoBehaviour
{
    private static int _layerOrder = 100;
    
    private TMP_Text _text;
    private const float Lifetime = 1.0f;
    private float _timeElapsed;
    private float _riseSpeed = 10.0f;
    
    public void Initialize(string text)
    {
        _text.text = text;
    }

    private void Awake()
    {
        _text = GetComponent<TMP_Text>();
        GetComponent<Renderer>().sortingOrder = _layerOrder++;
        _timeElapsed = 0f;
    }

    private void Update()
    {
        _timeElapsed += Time.deltaTime;
        transform.position += new Vector3(0.0f, _riseSpeed * Time.deltaTime, 0.0f);
        _riseSpeed *= Mathf.Pow(0.94f, Time.deltaTime * 60f);        
        if (_timeElapsed >= Lifetime)
        {
            Destroy(gameObject);
        }
    }
}
