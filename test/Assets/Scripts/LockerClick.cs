using UnityEngine;

public class LockerClick : MonoBehaviour
{
    public PuzzleLock puzzleLock;

    void OnMouseDown()
    {
        puzzleLock.OpenPanel();
    }
}
