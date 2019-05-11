using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class backgroundMove : MonoBehaviour
{
    private Renderer quadRenderer;
    public float scrollSpeed = 0.1f;

    // Start is called before the first frame update
    void Start()
    {
        quadRenderer = this.GetComponent<Renderer>();

    }

    // Update is called once per frame
    void Update()
    {
        Vector2 textureOffset = new Vector2(0, Time.time * scrollSpeed);
        quadRenderer.material.mainTextureOffset = textureOffset;
    }
}
