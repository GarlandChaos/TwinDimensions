using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class audioLevel : MonoBehaviour, IPointerDownHandler
{
    //[SerializeField]
    GameObject imageOn;

    //[SerializeField]
    //bool active;
    int id;

    // Start is called before the first frame update
    void Start()
    {
        imageOn = transform.GetChild(0).gameObject;
        //active = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void setId(int a)
    {
        id = a;
    }

    //public void setActive(bool a)
    //{
    //    active = a;
    //}

    //public bool getActive()
    //{
    //    return active;
    //}

    public void setOn()
    {
        imageOn.SetActive(true);
    }

    public void setOff()
    {
        imageOn.SetActive(false);
    }

    //private void OnMouseEnter()
    //{
    //    setOn();
    //}

    //private void OnMouseExit()
    //{
    //    setOff();
    //}

    //public void OnPointerEnter(PointerEventData pointerEventData)
    //{
    //    setOn();
        
    //}

    public void OnPointerDown(PointerEventData eventData)
    {
        setOn();
        //active = true;
        this.GetComponentInParent<audioLevelInterface>().setCurrentLevel(id);
        this.GetComponentInParent<audioLevelInterface>().setVolume();
        Debug.Log("clicked");
    }

    //public void OnPointerExit(PointerEventData pointerEventData)
    //{
    //    //if (!active)
    //    //{
    //        setOff();
    //    //}
    //}
}
