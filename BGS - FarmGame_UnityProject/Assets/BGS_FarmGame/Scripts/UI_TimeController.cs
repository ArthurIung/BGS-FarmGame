using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UI_TimeController : MonoBehaviour
{

    [SerializeField] TextMeshProUGUI _time_text;

    private void Start()
    {
        GetComponent<TimeController>().OnTimeChange += (CurrentTime) => _time_text.text = CurrentTime;
    }

}
