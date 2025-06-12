using UnityEngine;

public class ToggleScript : MonoBehaviour
{
     [SerializeField] public MonoBehaviour scriptsToToggle; // Массив скриптов

    void toggleScript_()
    {

    scriptsToToggle.enabled = !scriptsToToggle.enabled; // Инвертируем состояние
    Debug.Log(scriptsToToggle.name + ": " + scriptsToToggle.enabled);

    }
}