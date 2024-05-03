using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Canvas object that follows world objects
/// </summary>
public class FollowObjectWorld : MonoBehaviour
{
    [SerializeField] Vector3 _offsetPosition;
    [SerializeField] Transform _objectToFollow;
    [SerializeField, Range(0,1)] float _interpolationValue;

    private void Update()
    {

        Vector3 positionToFollow = Camera.main.WorldToScreenPoint(_objectToFollow.position);
        positionToFollow += _offsetPosition * GetComponentInParent<Canvas>().transform.localScale.x;
        //The scale of the canvas is only to not mistake values when working with scaled canvas caused by reduction of 'game screen' in unity

        transform.position = Vector3.Lerp(transform.position, positionToFollow, _interpolationValue);
    }
}
