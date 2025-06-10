using UnityEngine;

public class Talk : MonoBehaviour
{
    void Start() { }

    void Update() { }
    public void ConvEnd()
    {
        PlayerController.IsTalking = false;
        Debug.Log("Talk end: ");
    }
    // public void TestMethod() {
    // Debug.Log("Test called");
// }
}