using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelTransition : MonoBehaviour
{
    private float speed = 25f;
    private Vector3 otherPosition;
    private Vector3 defaultPosition;
    Vector3 goalPosition;
    Vector3 goalScale;

    bool transitioning = false;
    void Start()
    {
        defaultPosition = transform.position;
        otherPosition = new Vector3(defaultPosition.x, defaultPosition.y + Random.Range(20f, 50f), defaultPosition.z);
        transform.position = otherPosition;   
        transform.localScale = new Vector3(0.01f, 0.01f, 0.01f);
    }
    public void HideMe()
    {
        transitioning = true;
        goalPosition = otherPosition;
        goalScale = new Vector3(0.01f, 0.01f, 0.01f);
    }
    public void ShowMe()
    {
        transitioning = true;
        goalPosition = defaultPosition;
        goalScale = Vector3.one;
    }

    public void Update()
    {
        if (transitioning)
        {
            TransitionTo(goalPosition, goalScale);
        }
    }
    public void TransitionTo(Vector3 positionTo, Vector3 scaleTo)
    {
        float t = Time.deltaTime * 4f;
        t = t * t * (3f - 2f * t);

        float s = Time.deltaTime * 4f;
        s = s * s * (3f - 2f * s);

        transform.position = Vector3.Lerp(transform.position, positionTo, t);
        transform.localScale = Vector3.Lerp(transform.localScale, scaleTo, s);

        if(Vector3.Distance(transform.position, positionTo) < 0.001f && Vector3.Distance(transform.localScale, scaleTo) < 0.001f)
        {
            transform.position = positionTo;
            transform.localScale = scaleTo;
            transitioning = false;
        }   
    }
}
