using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotebookManager : MonoBehaviour
{
    public GameObject notebookCanvas;

    public void OpenNotebook()
    {
        notebookCanvas.SetActive(true);
    }

    public void CloseNotebook()
    {
        notebookCanvas.SetActive(false);
    }
}
