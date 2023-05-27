using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slider : MonoBehaviour
{
    public Transform startPos;
    public Transform endPos;

    MeshRenderer meshRenderer = null;

    GameObject sliderItem;
    private Transform sliderTransform;

    bool hasSliderItem = false;

    float distance = 0.002f;

    // Start is called before the first frame update
    void Start()
    {
        sliderItem = GameObject.FindGameObjectWithTag("Slider Item");
        if(sliderItem != null )
        {
            hasSliderItem = true;
            sliderTransform = sliderItem.GetComponent<Transform>();
        }

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
        Vector3 holdPosition = Vector3.Lerp(startPos.position, endPos.position, percent);
        transform.position = new Vector3(holdPosition.x, transform.position.y, transform.position.z);
        UpdateSliderItem(percent);
    }

    private void UpdateSliderItem(float percent)
    {
        if (percent == 0)
        {
            distance = .002f;
        }
        if (percent == 1)
        {
            distance = -0.002f;
        }
        if (hasSliderItem == true && percent > 0 && percent < 1)
        {
            sliderItem.transform.position = new Vector3(sliderItem.transform.position.x, sliderItem.transform.position.y + distance, sliderItem.transform.position.z);
        }
       
    }
}
