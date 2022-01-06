using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class laserScript : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private Sprite laseOnSprite, laserOffSprite;
    [SerializeField] private float interval = 0.5f, rotationSpeed = 0.0f;
    private bool isLaserOn = true;
    private float timeUntilNexttoggle;
    private Collider2D _collider;
    private SpriteRenderer spriteRenderer;
    void Start()
    {
        timeUntilNexttoggle = interval;
        _collider = GetComponent<Collider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void FixedUpdate()
    {
        timeUntilNexttoggle -= Time.fixedDeltaTime;
        if(timeUntilNexttoggle<=0)
        {
            isLaserOn = !isLaserOn;
           _collider.enabled=isLaserOn;
            if (isLaserOn)
                spriteRenderer.sprite = laseOnSprite;
            else spriteRenderer.sprite = laserOffSprite;
            timeUntilNexttoggle = interval;
        }
        transform.RotateAround(transform.position, Vector3.forward, rotationSpeed * Time.fixedDeltaTime);
    }
}
