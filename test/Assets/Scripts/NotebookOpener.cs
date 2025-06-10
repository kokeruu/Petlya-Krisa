using UnityEngine;

public class NotebookOpener : MonoBehaviour
{
    public GameObject notebookCanvas; // Сюда перетащишь NotebookCanvas

    public void OpenNotebook()
    {
        notebookCanvas.SetActive(true);
    }
}



