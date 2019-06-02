using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class buttonBehaviour : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public bool isOver = false;
    Vector3 normalScale; 

    // Start is called before the first frame update
    void Start()
    {
        normalScale = this.transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //private void OnMouseEnter()
    //{
    //    Debug.Log("Mouse enter");
    //    isOver = true;
    //}

    //private void OnMouseExit()
    //{
    //    Debug.Log("Mouse exited");
    //    isOver = false;
    //}

    public void OnPointerEnter(PointerEventData pointerEventData)
    {
        //Output to console the GameObject's name and the following message
        Debug.Log("Cursor Entering " + name + " GameObject");
        isOver = true;
        //Debug.Log("SCALE: " + pointerEventData.selectedObject.transform.localScale);
        //pointerEventData.selectedObject.transform.localScale = new Vector3(2f, 2f, 2f);
        this.transform.localScale = new Vector3(this.transform.localScale.x + 0.02f,
                                                this.transform.localScale.y + 0.02f,
                                                this.transform.localScale.z + 0.02f);
        //this.transform.localScale = new Vector3(0.12f, 0.12f, 0.12f);
    }

    //Detect when Cursor leaves the GameObject
    public void OnPointerExit(PointerEventData pointerEventData)
    {
        //Output the following message with the GameObject's name
        Debug.Log("Cursor Exiting " + name + " GameObject");
        isOver = false;
        this.transform.localScale = normalScale;
    }
}
