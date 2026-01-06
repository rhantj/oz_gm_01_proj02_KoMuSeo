using UnityEngine;
using System.Collections;
using System;

public enum PlayState
{
    None,
    Playing,
    Pause
}

public class GameManager : MonoBehaviour, IRegistryAdder
{
    [Header("Variable")]
    [SerializeField]private PlayState currentPlayState;

    public event Action<PlayState> OnPlayStateChanged;

    private void Awake()
    {
        AddRegistry();
        StartCoroutine(Co_PlayLoop());
    }

    private IEnumerator Co_PlayLoop()
    {
        currentPlayState = PlayState.Playing;

        while (true)
        {
            if (currentPlayState == PlayState.None) break;

            while(currentPlayState == PlayState.Pause)
            {
                yield return null;
            }

            yield return null;
        }
    }
    public void AddRegistry()
    {
        StaticRegistry.Add(this);
    }

    public PlayState GetCurrentState()
    {
        return currentPlayState;
    }

    private void SetCurrentState(PlayState change)
    {
        currentPlayState = change;
        OnPlayStateChanged?.Invoke(currentPlayState);
    }

    public void Pause()
    {
        Time.timeScale = 0f;
        SetCurrentState(PlayState.Pause);
    }
    
    public void Resume()
    {
        Time.timeScale = 1f;
        SetCurrentState(PlayState.Playing);
    }
}
