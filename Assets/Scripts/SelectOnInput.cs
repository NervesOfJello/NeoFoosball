﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SelectOnInput : MonoBehaviour
{

    [SerializeField]
    private EventSystem eventSystem;
    [SerializeField]
    private GameObject selectedObject;
    private Text selectedText;

    private bool buttonSelected;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetAxisRaw("Vertical11") != 0 && buttonSelected == false)
        {
            eventSystem.SetSelectedGameObject(selectedObject);
            buttonSelected = true;
        }

    }

    private void OnDisable()
    {
        buttonSelected = false;
    }
}
