using UnityEngine;

public class BoolVariableChanger : MonoBehaviour
{
    [Tooltip("Выберите, какую переменную изменять")]
    public enum BoolVariableType
    {
        IszapiskaRRead,
        IsDoorOpened
    }

    public BoolVariableType variableToChange;
    public bool newValue;

    public void ChangeBoolVariable()
    {
        switch (variableToChange)
        {
            case BoolVariableType.IszapiskaRRead:
                Suzhet.IszapiskaRRead = newValue;
                Debug.Log($"IszapiskaRRead изменено на: {newValue}");
                break;
            case BoolVariableType.IsDoorOpened:
                Suzhet.IsDoorOpened = newValue;
                Debug.Log($"IsDoorOpened изменено на: {newValue}");
                break;
        }
    }
}