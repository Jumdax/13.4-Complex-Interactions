using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Lever : MonoBehaviour
{
    public Transform startOrientation;
    public Transform endOrientation;

    MeshRenderer meshRenderer = null;

    GameObject leverItem;
    MeshRenderer leverMesh;

    bool hasLeverItem = false;

    // Start is called before the first frame update
    void Start()
    {
        leverItem = GameObject.FindGameObjectWithTag("Lever Item");
        if (leverItem != null)
        {
            hasLeverItem = true;
            leverMesh = leverItem.GetComponent<MeshRenderer>();
        }
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
        UpdateLeverItem(percent);
    }
    private void UpdateLeverItem(float percent)
    {
        if (hasLeverItem == true)
        {
            if (percent == 0)
            {
                leverMesh.material.SetColor("_Color", Color.cyan);
            }
            if (percent == 1)
            {
                leverMesh.material.SetColor("_Color", Color.yellow);
            }
        }
    }
}
