using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private GameObject target;
    private float distanceToTarget;
    private AudioSource musVolume;
    private Rigidbody _rigidbode;
    void Start()
    {
        musVolume = GetComponent<AudioSource>();
        musVolume.volume = PlayerPrefs.GetFloat("musicVolume", 0);
        if (PlayerPrefs.GetInt("volumeToggle", 0) == 1)
            musVolume.mute = true;
        else musVolume.mute = false;
        distanceToTarget = transform.position.x - target.transform.position.x;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 cameraPos = transform.position;
        cameraPos.x = target.transform.position.x+distanceToTarget;
        transform.position = cameraPos;
    }
}
