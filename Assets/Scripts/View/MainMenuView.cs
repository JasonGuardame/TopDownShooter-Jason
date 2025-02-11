using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class MainMenuView : MonoBehaviour
{
    public UnityEvent OnStartClickedEvent;
    public UnityEvent OnQuitClickedEvent;

    public Button startBtn, quitBtn;

    public void Start()
    {
        startBtn.onClick.AddListener(OnStartBtnClicked);
        quitBtn.onClick.AddListener(OnQuitBtnClicked);
    }

    void OnQuitBtnClicked()
    {
        OnQuitClickedEvent?.Invoke();
    }

    void OnStartBtnClicked()
    {
        OnStartClickedEvent?.Invoke();
    }
}
