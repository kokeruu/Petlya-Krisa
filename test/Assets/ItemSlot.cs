using System.Collections;
using System.Collections.Generic;
using Microsoft.Unity.VisualStudio.Editor;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class ItemSlot : MonoBehaviour
{
    [SerializeField]
    private UnityEngine.UI.Image icon;
    [SerializeField]
    private TextMeshProUGUI label;
    // [SerializeField]
    // private GameObject StackObj;
    // [SerializeField]
    // private Image icon;
    public void Set(InventoryItem item)
    {
        icon.sprite = item.data.icon;
        label.text = item.data.displayName;

    }
}
