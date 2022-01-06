using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;

public class MouseController : MonoBehaviour
{
    [SerializeField] GameObject heart1;
    [SerializeField] GameObject heart2;
    [SerializeField] GameObject heart3;
    // Start is called before the first frame update
    [SerializeField] private float jetpackForce = 75.0f,movementSpeed = 3f;
    [SerializeField] private Transform groundCheckerPos;
    [SerializeField] private LayerMask groundCheckerLayerMask;
    [SerializeField] private ParticleSystem jetpack;
    [SerializeField] private Texture2D coinTexture;

    int health = 3;
    private bool grounded;
    private bool jetpackActive;
    private bool dead;
    private uint coins = 0;
    private Animator _animator;
    private Rigidbody2D _rigidbody;

    [SerializeField] private Text coinsLabel;
    [SerializeField] private Animator diePanelAnimator;

    [SerializeField] private AudioClip coinCollectSound;
    [SerializeField] private AudioSource jetpackSound;
    [SerializeField] private AudioSource footstepsSound;
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void FixedUpdate()
    {
        jetpackActive = Input.GetButton("Fire1");
        jetpackActive = jetpackActive && !dead;
        if (!dead)
        {
            Vector2 newVel = _rigidbody.velocity;
            newVel.x = movementSpeed;
            _rigidbody.velocity = newVel;
        }
        if (jetpackActive)
        {
            _rigidbody.AddForce(new Vector2(0, jetpackForce));
        }
        adjustJetpack(jetpackActive);
        updateGrounded();
        AdjustJetpackAndFootstepsSounds();
    }
    void updateGrounded()
    {
        grounded = Physics2D.OverlapCircle(groundCheckerPos.position, 0.1f, groundCheckerLayerMask);
        _animator.SetBool("switchToRun", grounded);
    }
    void adjustJetpack(bool jetpackActive)
    {
        ParticleSystem.EmissionModule emissionModule = jetpack.emission;
        emissionModule.enabled = !grounded;
        emissionModule.rateOverTime = jetpackActive ? 300.0f : 75.0f;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("coins"))
            CollectCoin(collision);
        else
            HitByLaser(collision);
    }
    void HitByLaser(Collider2D laserCollider)
    {
        health--;
        if(health==0)
        {
            if (!dead)
                laserCollider.gameObject.GetComponent<AudioSource>().Play();
            dead = true;
            _animator.SetBool("dead", true);
            //diePanelAnimator.enabled = true;
            StartCoroutine(WaitUntilDie());
            heart3.SetActive(false);
        }
        else if(health==1)
        {
            heart2.SetActive(false);
        }
        else if (health==2)
        {
            heart1.SetActive(false);
        }

    }
    void CollectCoin(Collider2D coinCollider)
    {
        AudioSource.PlayClipAtPoint(coinCollectSound, transform.position);
        coins++;
        Destroy(coinCollider.gameObject);
        coinsLabel.text = coins.ToString();
    }
    private void AdjustJetpackAndFootstepsSounds()
    {
        footstepsSound.enabled = !dead && grounded;
        jetpackSound.enabled = !dead && !grounded;
        jetpackSound.volume = jetpackActive ? 1.0f : 0.5f;
    }
    private IEnumerator WaitUntilDie()
    {
        while(!grounded)
        {
            yield return null;
        }
        diePanelAnimator.enabled = true;
    }
    public void ExitToMenu()
    {
        SceneManager.LoadScene("MenuScene");
    }
    public void RestartGame()
    {
        SceneManager.LoadScene("RocketMouse");
    }
}
