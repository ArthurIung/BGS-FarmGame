using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TimeController : BaseInitializer
{
    public enum DayWeek { Monday, Tuesday, Wednesday, Thursday, Friday, Saturday, Sunday}

    public enum Seasons
    {
        Spring,
        Summer,
        Fall,
        Winter
    }

    public enum Weather
    {
        Sun,
        Rain,
        Wind,
        Storm,
        Snow
    }

    [System.Serializable]
    public class WeatherChangeChance
    {
        [System.Serializable]
        public struct WeatherChance
        {
            public Weather _weather;
            [Range(0, 101)]
            public int chance;
        }

        public Seasons _season;
        public List<WeatherChance> _chance = new List<WeatherChance>();

        public Weather RandomizeWeather()
        {
            int rng = UnityEngine.Random.Range(0, 101);
            int minorChance = 101;
            int indexWeather = -1;

            for (int i = 0; i < _chance.Count; i++)
            {
                if (_chance[i].chance <= minorChance && rng <= _chance[i].chance)
                {
                    indexWeather = i;
                    minorChance = _chance[i].chance;
                }
            }

            if (indexWeather != -1)
                return _chance[indexWeather]._weather;

            return Weather.Sun;
        }
    }

    
    [Space]

    [Header("Time")]

    [Range(0,1)]public float currentTime;
    public Action<string> OnTimeChange;
    public Action<float> OnSecondChange;
    [Space]
    [SerializeField] float totalTimeInDay;
    [SerializeField, Range(0, 1)] float offsetTime;
    [SerializeField] string currentTimeDay;

    [Header("Seasons")]
    [SerializeField] int currentDay = 1;
    [SerializeField] DayWeek _today;
    public int CurrentWeekDay
    {
        get
        {
            return currentDay % 7;
        }
    }

    [SerializeField] Seasons currentSeason;
    [SerializeField] List<WeatherChangeChance> _weatherChance = new List<WeatherChangeChance>();

    [SerializeField] Weather weather_today;
    [SerializeField] Weather weather_tomorrow;

    public Action OnChangeDay;
    public Action OnChangeSeason;

    [SerializeField] int year = 1;

    [HideInInspector] public bool sleepOnBed;
    [SerializeField] bool _AM_View;

    public bool timeIsRunning = true;

    public DayWeek Today
    {
        get
        {
            _today = (DayWeek) (currentDay % 7);
            return _today;
        }
    }

    public override IEnumerator Cor_Initialize()
    {
        yield return StartCoroutine(base.Cor_Initialize());
        StartCoroutine(Cor_TimePass());
    }


    IEnumerator Cor_TimePass()
    {

        while(true)
        {
                if(currentTime >= 1)
                {
                    currentTime = 0;

                    yield return StartCoroutine(Cor_PassDay());

                    PassDay();
                    break;
                }

                currentTime += (1f / totalTimeInDay) * Time.deltaTime;
            
                PassTime();
            

            yield return null;
        }
    }

    void PassTime()
    {
        float _current = (currentTime  + offsetTime) * totalTimeInDay;
        float secondsInHour = totalTimeInDay / 24f;
        float secondsInMinute = secondsInHour / 60f;

        int hour = (int)(_current / secondsInHour) % 24;
        int minutes = (int)((_current % secondsInHour) / secondsInMinute);
        minutes /= 10;

        string newTime = "";
        if (_AM_View)
        {
            string am_pm = "";

            if(hour > 12)
            {
                hour -= 12;
                am_pm = "pm";
            }
            else if(hour >= 12)
            {
                am_pm = "pm";
            }
            else
            {
                am_pm = "am";
            }

            newTime = $"{hour}:{minutes}0 {am_pm}";
        }
        else
        {
            newTime = $"{hour}:{minutes}0";
        }
        OnSecondChange?.Invoke(currentTime);

        if (newTime != currentTimeDay)
        {
            currentTimeDay = newTime;

            if (OnTimeChange != null)
                OnTimeChange.Invoke(currentTimeDay);
        }

    }

    IEnumerator Cor_PassDay()
    {
        yield return new WaitForSeconds(2f);
    }


    public void PassDay()
    {
        if(!sleepOnBed)
        {
        }

        sleepOnBed = false;

        if (currentDay == 28)
        {
            currentDay = 1;
            ChangeSeason();
        }
        else
        {
            currentDay++;
        }

        StartCoroutine(Cor_TimePass());
        GetTomorrowClimate();


        if (OnChangeDay != null)
            OnChangeDay.Invoke();

    }

    void GetTomorrowClimate()
    {
        weather_today = weather_tomorrow;
        weather_tomorrow = _weatherChance.Find(x => x._season == currentSeason).RandomizeWeather();

        SetTodayClimate();
    }

    void SetTodayClimate()
    {
        switch (weather_today)
        {
            case Weather.Sun:
                break;
            case Weather.Rain:
                break;
            case Weather.Wind:
                break;
            case Weather.Storm:
                break;
            case Weather.Snow:
                break;
            default:
                break;
        }
    }

    public void ChangeSeason()
    {

        switch (currentSeason)
        {
            case Seasons.Spring:
                currentSeason = Seasons.Summer;
                break;
            case Seasons.Summer:
                currentSeason = Seasons.Fall;
                break;
            case Seasons.Fall:
                currentSeason = Seasons.Winter;
                break;
            case Seasons.Winter:
                currentSeason = Seasons.Spring;
                year++;
                break;
        }


        if (OnChangeSeason != null)
            OnChangeSeason.Invoke();
    }


    public static string CalculateTime(float time)
    {
        float offsetTime = GameObject.FindObjectOfType<TimeController>().offsetTime;
        float totalTimeInDay = GameObject.FindObjectOfType<TimeController>().totalTimeInDay;

        float _current = (time + offsetTime) * totalTimeInDay;
        float secondsInHour = totalTimeInDay / 24f;
        float secondsInMinute = secondsInHour / 60f;

        int hour = (int)(_current / secondsInHour) % 24;
        int minutes = (int)((_current % secondsInHour) / secondsInMinute);
        minutes /= 10;

        string newTime = $"{hour}:{minutes}0";

        return newTime;
    }

}
