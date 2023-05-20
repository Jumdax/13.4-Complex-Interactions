using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR.Interaction.Toolkit;

[Serializable]
public class DragEvent : UnityEvent<float> { }


public class DragInteractable : XRBaseInteractable
{
    public Transform startDragPos = null;
    public Transform endDragPos = null;
    
    [HideInInspector]
    public float dragPercent = 0.0f; // [0,1]
    
    protected XRBaseInteractor m_interactor = null;

    public UnityEvent onDragStart = new UnityEvent();
    public UnityEvent onDragEnd = new UnityEvent();
    public DragEvent onDragUpdate = new DragEvent();

    Coroutine m_drag = null;

    void StartDrag()
    {
        if (m_drag != null)
        {
            StopCoroutine(m_drag);
        }
        m_drag = StartCoroutine(CalculateDrag());
        onDragStart?.Invoke();
    }

    void EndDrag()
    {
        if (m_drag != null)
        {
            StopCoroutine(m_drag);
            m_drag = null;
            onDragEnd?.Invoke();
        }
    }

    public static float InverseLerp(Vector3 a, Vector3 b, Vector3 value)
    {
        Vector3 AB = b - a;
        Vector3 AV = value - a;
        // the dot of a to value divided by the dot of the total range
        // gives the normalized 0-1 distance of a valuu between a and b
        return Mathf.Clamp01(Vector3.Dot(AB, AV) / Vector3.Dot(AB, AB));
    }

    IEnumerator CalculateDrag()
    {
        while (m_interactor !=  null)
        {
            // get a line in local space
            Vector3 line = startDragPos.localPosition - endDragPos.localPosition;

            // convert our interactor position to a local space
            Vector3 ineractorLocalPos = startDragPos.parent.InverseTransformPoint(m_interactor.transform.position);

            // project the interactor position onto the line
            Vector3 projectedPoint = Vector3.Project(ineractorLocalPos, line.normalized);

            // reverse interpolate that position on the line to get a percentage of how far drag has moved
            dragPercent = InverseLerp(startDragPos.localPosition, endDragPos.localPosition, projectedPoint);

            onDragUpdate?.Invoke(dragPercent);
            
            yield return null;
        }
    }

    protected override void OnSelectEntered(XRBaseInteractor interactor)
    {
        Debug.Log("OnSelectEntered");
        m_interactor = interactor;
        StartDrag();
        base.OnSelectEntered(interactor);
    }

    protected override void OnSelectExited(XRBaseInteractor interactor)
    {
        Debug.Log("OnSelectExited");
        EndDrag();
        m_interactor = null;
        base.OnSelectExited(interactor);
    }
}
