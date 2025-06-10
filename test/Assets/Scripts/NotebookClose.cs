using UnityEngine;
using UnityEngine.UI;

public class NotebookClose : MonoBehaviour
{
    public GameObject notebookCanvas;

    void Start()
    {
        GetComponent<Button>().onClick.AddListener(() =>
        {
            notebookCanvas.SetActive(false);
        });
    }
}
