using System;
using UnityEngine;

[CreateAssetMenu(fileName = "Visual Config Service", menuName = "Visual Config Service")]
public class VisualConfigService : ScriptableObject
{
    [SerializeField]
    VisualConfig Default;
    [SerializeField]
    VisualConfig Christmas;
    [SerializeField]
    VisualConfig Easter;


    public VisualConfig GetVisualConfig
    {
        get
        {
            if (isChristmas())
            {
                return Christmas;
            }

            if (isEaster())
            {
                return Easter;
            }

            return Default;
        }
    }

    bool isChristmas()
    {
        return DateTime.Now.Month == 12 && DateTime.Now.Day <= 25;
    }

    bool isEaster()
    {
        DateTime easterMonday = EasterSunday(DateTime.Now.Year).AddDays(1);
        int dayOfYear = DateTime.Now.DayOfYear;

        return dayOfYear >= easterMonday.DayOfYear - 7 && dayOfYear <= easterMonday.DayOfYear;
    }

    DateTime EasterSunday(int year)
    {
        int day = 0;
        int month = 0;

        int g = year % 19;
        int c = year / 100;
        int h = (c - (c / 4) - ((8 * c + 13) / 25) + 19 * g + 15) % 30;
        int i = h - (h / 28) * (1 - (h / 28) * (29 / (h + 1)) * ((21 - g) / 11));

        day = i - ((year + (year / 4) + i + 2 - c + (c / 4)) % 7) + 28;
        month = 3;

        if (day > 31)
        {
            month++;
            day -= 31;
        }

        return new DateTime(year, month, day);
    }
}