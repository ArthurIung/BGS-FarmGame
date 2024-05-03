using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC_Schedule : BaseInitializer
{

    BaseNPC _base;

    [SerializeField, Range(0,1)] float timeToCome;
    [SerializeField, Range(0,1)] float timeToLeave;

    [Header("Destination Points")]
    [SerializeField, Tooltip("This destination is automatically filled")] Transform _destination;

    [SerializeField] Transform _shopDestination;
    [SerializeField] Transform _houseDestination;

    bool reachedDestination;
    public Action OnReachDestination;
    public Action OnLeaveDestination;
    public Action OnMove;
    public enum ScheduleStatus
    {
        Stopped = 0,
        Coming = 1,
        Leaving = 2
    }

    public ScheduleStatus _currentScheduleStatus;
    public override IEnumerator Cor_Initialize()
    {
        _base = GetComponent<BaseNPC>();
        LogicController.Instance._timeController.OnSecondChange += VerifySchedule;
        yield return StartCoroutine(base.Cor_Initialize());

        OnReachDestination += ()=>_currentScheduleStatus = ScheduleStatus.Stopped;

    }

    void VerifySchedule(float currentTime)
    {
        if(currentTime > timeToCome && currentTime < timeToLeave)
        {
            _destination = _shopDestination;
            _currentScheduleStatus = ScheduleStatus.Coming;
            reachedDestination = false;


        }
        else if(currentTime > timeToLeave)
        {
            _destination = _houseDestination;
            _currentScheduleStatus = ScheduleStatus.Leaving;
            OnLeaveDestination?.Invoke();
            reachedDestination = false;
        }

    }

    private void Update()
    {
        if (!IsInitialized) return;

        if (_currentScheduleStatus == ScheduleStatus.Stopped) return;

        if (Vector3.Distance(transform.position, _destination.position) < 0.02f)
        {
            if (!reachedDestination)
            {
                _base.Animation.SetWalk(false);
                _base.Animation.AnimateMovement(0, -1);
                reachedDestination = true;
                OnReachDestination?.Invoke();
                _base.OnStartMoving.Invoke(new Vector2(0,-1), false);
            }
        }
        else
        {
            _base.Animation.SetWalk(true);
            MoveTowardsDestination();
        }

    }

    void MoveTowardsDestination()
    {
        transform.position = Vector3.MoveTowards(transform.position, _destination.position, _base.Speed * Time.deltaTime);

        Vector3 directionVector = (_destination.position - transform.position).normalized;

        _base.Animation.AnimateMovement(directionVector.x, directionVector.y);
        _base.OnStartMoving.Invoke(directionVector, true);
        OnMove?.Invoke();
    }


}
