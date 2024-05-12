using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CheckFields : MonoBehaviour
{
    [SerializeField] private InputFieldChanger[] fields;
    [SerializeField] private Button button;
    private void FixedUpdate()
    {
        foreach (var field in fields)
        {
            if (field.text == "")
            {
                button.interactable = false;
                return;
            }
        }
        button.interactable = true;
    }
}
