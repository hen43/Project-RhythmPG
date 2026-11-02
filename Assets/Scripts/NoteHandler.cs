using UnityEngine;

public class NoteHandler : MonoBehaviour
{
    [SerializeField] private NoteReceiver[] keys;

    public void OnButton1() { keys[0].Hit(); }
    public void OnButton2() { keys[1].Hit(); }
    public void OnButton3() { keys[2].Hit(); }
    public void OnButton4() { keys[3].Hit(); }

}
