using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecurityCamera : MonoBehaviour
{
    public float startPoint = -1.0f;
    public float endPoint = 1.0f;

    public float interpolationMultiplier = 0.25f;

    private float _interpolationValue = 0.0f;

    public List<GameObject> guardList;
    public GameObject[] guardArr;
    private Sentry _guardScript;
    private Light _spotlight;
    private bool _alert;
    private bool _playerNotFound;

    private void Start()
    {
        _spotlight = GetComponentInChildren<Light>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (_alert)
            _spotlight.color = Color.red;
        else
            _spotlight.color = new Color(1, 0.64f, 0, 1);

        // Animate the rotation between start to end
        transform.Rotate(0, Mathf.Lerp(startPoint, endPoint, _interpolationValue), 0);

        // Increase the interpolation value
        _interpolationValue += interpolationMultiplier * Time.deltaTime;

        // If the interpolator value reaches it's current target, 
        // the points are swapped so that it should move to the opposite direction
        if (_interpolationValue > 1.0f)
        {
            float temp = endPoint;
            endPoint = startPoint;
            startPoint = temp;
            _interpolationValue = 0.0f;
        }
    }

    void SeePlayer(List<RaycastHit> hitList)
    {
        _playerNotFound = true;

        if (hitList != null)
        {
            for (int i = 0; i < hitList.Count; i++)
            {
                //if (hitList[i].collider != null)
                //{
                if (hitList[i].collider.tag == "Player")
                {
                    _playerNotFound = false;
                    _alert = true;
                    for (int j = 0; j < guardList.Count; j++)
                    {
                        _guardScript = guardList[j].GetComponent<Sentry>();
                        _guardScript.SendMessage("SeePlayer", hitList);
                        //_guardScript.DetectionAmount = _guardScript.MaxDetectionAmount;
                    }
                }
                else if (_playerNotFound)
                    _alert = false;
                //}
            }
        }
        else
            _alert = false;

        

        //if (hit.collider != null)
        //{
        //    if (hit.collider.tag == "Player")
        //    {
        //        _alert = true;
        //        for (int i = 0; i < guardList.Capacity; i++)
        //        {
        //            _guardScript = guardList[i].GetComponent<Sentry>();
        //            _guardScript.SendMessage("SeePlayer", hit);
        //            //_guardScript.DetectionAmount = _guardScript.MaxDetectionAmount;
        //        }
        //    }
        //    else
        //        _alert = false;
        //}
    }

}
