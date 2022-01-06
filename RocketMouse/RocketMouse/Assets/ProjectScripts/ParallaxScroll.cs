using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxScroll : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private Renderer bg;
    [SerializeField] private Renderer fg;

    [SerializeField] private float bgSpeed;
    [SerializeField] private float fgSpeed;

    [SerializeField] private Transform mousePos;
    private float offset;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        offset = mousePos.position.x;
        float bgOffset = offset * bgSpeed;
        float fgOffset = offset * fgSpeed;
        bg.material.mainTextureOffset = new Vector2(bgOffset, 0);
        fg.material.mainTextureOffset = new Vector2(fgOffset, 0);
    }
}
