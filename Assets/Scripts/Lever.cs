using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lever : MonoBehaviour
{
    public Transform startOrientation;
    public Transform endOrientation;

    MeshRenderer meshRenderer = null;

    // Start is called before the first frame update
    void Start()
    {
        meshRenderer = GetComponentInChildren<MeshRenderer>();
    }

    public void OnLeverPullStart()
    {
        Debug.Log("OnLeverPullStart");
        meshRenderer.material.SetColor("_Color", Color.red);
    }

    public void OnLeverPullStop()
    {
        Debug.Log("OnLeverPullStop");
        meshRenderer.material.SetColor("_Color", Color.white);
    }

    public void UpdateLever(float percent)
    {
        transform.rotation = Quaternion.Slerp(startOrientation.rotation, endOrientation.rotation, percent);
    }
}
