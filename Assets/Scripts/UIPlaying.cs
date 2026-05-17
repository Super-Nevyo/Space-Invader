using TMPro;
using UnityEngine;

public class UIPlaying : MonoBehaviour
{
    [SerializeField] private TMP_Text scoreTxt;
    [SerializeField] private TMP_Text hPTxt;
    [SerializeField] private TMP_Text timeTxt;


    public static UIPlaying instance;
    private void Awake()
    {
        if (instance == null) instance = this;
    }
    public void UpdateScore(int Score)
    {
        scoreTxt.SetText("Score: " + Score.ToString());
    }
    public void UpdateHP(int HP)
    {
        hPTxt.SetText("HP: " +  HP.ToString());
    }
    public void UpdateTime(float Time) 
    {
        timeTxt.SetText("Seconds Remaining: " + Time.ToString());
    }
}
