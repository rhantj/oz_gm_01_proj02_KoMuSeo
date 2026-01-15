using UnityEngine;

public abstract class UIPanel : MonoBehaviour
{
    protected bool isOpen;
    public bool IsOpen
    {
        get {  return isOpen; }
        set
        {
            isOpen = value;
            gameObject.SetActive(isOpen);
        }
    }

    CanvasGroup cnv;

    protected virtual void Awake()
    {
        cnv = GetComponent<CanvasGroup>();
    }

    public virtual void Open()
    {
        IsOpen = true;
    }

    public virtual void Close()
    {
        IsOpen = false;
    }
}