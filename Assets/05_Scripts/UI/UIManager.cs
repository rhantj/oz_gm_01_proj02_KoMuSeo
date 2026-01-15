using System;
using System.Collections.Generic;
using UnityEngine;

public enum UIKey
{
    HUD,
    Pause,
    Setting,
    GameOver,
    Popup,
    FadeOut
}

public class UIManager : MonoBehaviour, IRegistryAdder
{
    [Header("UI Panel")]
    [SerializeField] UIPanel hudUI;
    [SerializeField] UIPanel pauseUI;
    [SerializeField] UIPanel settingUI;
    [SerializeField] UIPanel gameoverUI;
    [SerializeField] UIPanel popupUI;
    [SerializeField] UIPanel fadeoutUI;

    Stack<UIPanel> popupStack = new();
    Stack<UIPanel> rewindStack = new();
    Dictionary<UIKey, UIPanel> panels = new();
    public event Action OnUIChanged;

    void Awake()
    {
        AddRegistry();
        AddtoDictionary();
    }

    public void AddRegistry()
    {
        StaticRegistry.Add(this);
    }

    void AddtoDictionary()
    {
        panels.Clear();
        Add(UIKey.HUD, hudUI);
        Add(UIKey.Pause, pauseUI);
        Add(UIKey.Setting, settingUI);
        Add(UIKey.GameOver, gameoverUI);
        Add(UIKey.Popup, popupUI);
        Add(UIKey.FadeOut, fadeoutUI);
    }

    void Add(UIKey key, UIPanel value)
    {
        if (value == null) return;
        panels[key] = value;
    }

    public T Get<T>(UIKey key) where T : UIPanel
    {
        return panels.TryGetValue(key, out var p) ? p as T : null;
    }

    public void Show(UIKey key, bool open)
    {
        if(!panels.TryGetValue(key, out var p) || !p) return;

        if (open)
        {
            p.Open();
            rewindStack.Push(p);
        }
        else
            p.Close();

        OnUIChanged?.Invoke();
    }
}