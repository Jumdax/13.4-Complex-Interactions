using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dial : MonoBehaviour
{
    Vector3 m_startRotation;
    
    MeshRenderer m_meshRenderer = null;

    GameObject dialItem;
    private Transform dialItemTransform;
    Vector3 dialItemRotation;

    bool hasDialItem = false;

    float itemAngle = 0.002f;

    private void Start()
    {
        dialItem = GameObject.FindGameObjectWithTag("Dial Item");
        if (dialItem != null)
        {
            hasDialItem = true;
            dialItemTransform = dialItem.GetComponent<Transform>();
            dialItemRotation = dialItemTransform.localEulerAngles;
         }
        m_meshRenderer = GetComponent<MeshRenderer>();
    }

    public void StartTurn()
    {
        m_startRotation = transform.localEulerAngles;
        m_meshRenderer.material.SetColor("_Color", Color.green);
    }

    public void StopTurn()
    {
        m_meshRenderer.material.SetColor("_Color", Color.white);
    }

    public void DialUpdate(float angle)
    {
        Debug.Log("DialUpdate -> " +  angle);
        Vector3 angles = m_startRotation;
        angles.y += angle;
        transform.localEulerAngles = angles;
        DialItemUpdate(angle);
    }

    private void DialItemUpdate(float angle)
    {
        if (hasDialItem == true) 
        {
            Debug.Log("DialItem Update -> " + angle);
            Vector3 angles = dialItemRotation;
            angles.z += angle;
            dialItemTransform.localEulerAngles = angles;
        }
    }
}
