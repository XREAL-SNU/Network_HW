using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
 
public class test3 : MonoBehaviour
{
    public Ease ease;
 
    public Transform wayPoint1;
    public Transform wayPoint2;
    public Transform wayPoint3;
 
    private Vector3[] wayPoints;
 
    // Start is called before the first frame update
    void Start()
    {
        // static DOTween.Init(bool recycleAllByDefault = false, bool useSafeMode = true, LogBehaviour logBehaviour = LogBehaviour.ErrorsOnly)
        // Initializes DOTween.
        DOTween.Init(false, true, LogBehaviour.ErrorsOnly);
 
        wayPoints = new Vector3[3];
        wayPoints.SetValue(wayPoint1.position, 0);
        wayPoints.SetValue(wayPoint2.position, 1);
        wayPoints.SetValue(wayPoint3.position, 2);
        // wayPoints = new[] { wayPoint1.position, wayPoint2.position, wayPoint3.position };
 
        // DOPath(Vector3[] waypoints, float duration, PathType pathType = Linear, PathMode pathMode = Full3D, int resolution = 10, Color gizmoColor = null)
        // Tweens a Transform's position through the given path waypoints, using the chosen path algorithm.
        transform.DOPath(wayPoints, 6.0f, PathType.CatmullRom).SetLookAt(new Vector3(0.0f, 0.0f, 0.0f)).SetEase(ease).SetLoops(-1, LoopType.Yoyo);
    }
 
    // Update is called once per frame
    void Update()
    {
 
    }
}