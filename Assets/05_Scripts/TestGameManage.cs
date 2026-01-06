using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TestGameManage : MonoBehaviour
{
    [SerializeField] Button pauseBtn;
    [SerializeField] Button resumeBtn;
    [SerializeField] Button exitBtn;
    [SerializeField] TextMeshProUGUI deltaTimeText;

    GameManager gm;

    private void Start()
    {
        gm = StaticRegistry.Find<GameManager>();

        pauseBtn.onClick.AddListener(gm.Pause);
        resumeBtn.onClick.AddListener(gm.Resume);
        exitBtn.onClick.AddListener(OnExitPressed);
    }

    private void Update()
    {
        deltaTimeText.text = Time.deltaTime.ToString();
    }

    void OnExitPressed()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
        Application.Quit();
    }
}
