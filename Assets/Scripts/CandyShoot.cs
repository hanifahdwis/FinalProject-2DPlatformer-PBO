using NUnit.Framework;
using UnityEngine;
using UnityEngine.InputSystem;

public class CandyShoot : MonoBehaviour
{
    [SerializeField] private float speed;
    private float direction;
    private bool hit;
    private float lifetime;

    private BoxCollider2D boxCollider;
    private Animator anim;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
    }
    private void Update()
    {
        // Cek apakah candyball sudah nabrak musuh
        if (hit)
        {
            return;
        }

        // Cek apakah candyball sudah aktif
        float movementSpeed = speed * Time.deltaTime * direction;
        transform.Translate(movementSpeed, 0, 0);

        // Cek apakah candyball sudah terlalu lama spawn
        lifetime += Time.deltaTime;
        if (lifetime > 5) gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Cek apakah candyball nabrak musuh
        hit = true;
        boxCollider.enabled = false;
        anim.SetTrigger("Explode");
    }
    public void SetDirection(float _direction)
    {
        // Set direction dan aktifkan candyball
        lifetime = 0;
        direction = _direction;
        gameObject.SetActive(true);
        hit = false;
        boxCollider.enabled = true;

        // Ubah skala candyball sesuai arah
        float localScaleX = transform.localScale.x;
        if (Mathf.Sign(localScaleX) != _direction)
            localScaleX = -localScaleX;
        
        transform.localScale = new Vector3(localScaleX, transform.localScale.y, transform.localScale.z); ;
    }

    //Animasi selesai, matikan candyball
    private void Deactivate()
    {
        gameObject.SetActive(false);
    }
}
