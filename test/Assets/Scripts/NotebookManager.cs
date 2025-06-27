using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotebookManager : MonoBehaviour
{
    public GameObject notebookCanvas;

    public void OpenNotebook()
    {
        PlayerController.IsUsing = true;
        notebookCanvas.SetActive(true);
    }

    public void CloseNotebook()
    {
        notebookCanvas.SetActive(false);
        PlayerController.IsUsing = false;

    }
    
}
