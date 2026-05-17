using TMPro;
using UnityEngine;

public class UIMenuing : MonoBehaviour
{
    [SerializeField] private GameObject startCanvas;
    [SerializeField] private GameObject menuCanvas;
    [SerializeField] private GameObject endCanvas;
    [SerializeField] private TMP_Text endScore;

    public static UIMenuing instance;
    private void Awake()
    {
        if (instance == null) instance = this;
    }
    public void StartCanvas()
    {
        startCanvas.SetActive(true);
        menuCanvas.SetActive(false);
        endCanvas.SetActive(false);
    }
    public void MenuCanvas()
    {
        startCanvas.SetActive(false);
        menuCanvas.SetActive(true);
        endCanvas.SetActive(false);
    }
    public void EndCanvas(int Score)
    {
        startCanvas.SetActive(false);
        menuCanvas.SetActive(false);
        endCanvas.SetActive(true);
        endScore.SetText("Your final score is: " + Score);
    }

}
