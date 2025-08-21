using System;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;

public class DayTimeController : MonoBehaviour
{
    public Color mNightLightColor;
    public AnimationCurve mNightTimeCurve;
    public Color mDayLightColor = Color.white;
    public Light2D mGlobalLight;
    public TextMeshProUGUI mTimeText;
    public bool UseDateTime = false;
    public float TIME_SCALE = 60.0f; // tốc độ thời gian

    private float mTime = 0f; // số giây trôi qua trong ngày (giả lập)
    private int mDays = 1;


    private void Update()
    {
        if (!UseDateTime) return;

        // Tăng thời gian theo TIME_SCALE
        mTime += Time.deltaTime * TIME_SCALE;

        // Chuyển đổi thành DateTime để dễ hiển thị
        TimeSpan timeSpan = TimeSpan.FromSeconds(mTime);
        mTimeText.text = $"Day {mDays} - {timeSpan.Hours:00}:{timeSpan.Minutes:00}";

        

        // Đổi màu ánh sáng theo giờ (0–24)
        float gameHours = (float)timeSpan.TotalHours;
        float v = mNightTimeCurve.Evaluate(gameHours);
        Color c = Color.Lerp(mNightLightColor, mDayLightColor, v);
        mGlobalLight.color = c;

        // Sang ngày mới
        if (mTime > 86400.0f)
        {
            NextDay();
        }
    }

    private void NextDay()
    {
        mTime = 0.0f;
        mDays++;
    }
}
