using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slider : MonoBehaviour
{
    public Transform startPos;
    public Transform endPos;

    MeshRenderer meshRenderer = null;

    // Start is called before the first frame update
    void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
    }

    public void OnSlideStart()
    {
        Debug.Log("OnSlideStart");
        meshRenderer.material.SetColor("_Color", Color.red);
    }

    public void OnSlideStop()
    {
        Debug.Log("OnSlideStop");
        meshRenderer.material.SetColor("_Color", Color.white);
    }

    public void UpdateSlider(float percent)
    {
        Debug.Log("UpdateSlider " + percent);
        Vector3 holdPosition = Vector3.Lerp(startPos.position, endPos.position, percent);
        transform.position = new Vector3(holdPosition.x, transform.position.y, transform.position.z);
    }
}
