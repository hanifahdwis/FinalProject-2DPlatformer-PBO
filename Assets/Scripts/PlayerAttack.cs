using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAttack : CharacterBase
{
    private PlayerMovement playerMovement;
    [SerializeField] private float attackTime;
    [SerializeField] private Transform candypoint;
    [SerializeField] private GameObject[] candyball;
    private float attackTimeCounter = Mathf.Infinity;

    private new void Awake()
    {
        base.Awake();
        playerMovement = GetComponent<PlayerMovement>();
    }

    private void Update()
    {
        HandleMovement();
    }

    protected override void HandleMovement()
    {
        // Check input keyboard player buat attack
        if (Keyboard.current.jKey.wasPressedThisFrame && attackTimeCounter > attackTime && playerMovement.canAttack())
        {
            Attack();
            
        }
        attackTimeCounter += Time.deltaTime;
    }

    private void Attack()
    {
        //Cek apakah player bisa attack
        anim.SetTrigger("Attack");
        attackTimeCounter = 0;
        candyball[Findcandyball()].transform.position = candypoint.position;
        candyball[Findcandyball()].GetComponent<CandyShoot>().SetDirection(Mathf.Sign(transform.localScale.x));

    }
    private int Findcandyball()
    {
        //Mencari candyball yang tidak aktif buat dikembaliin indeksnya(respawn) ke posisi candypoint
        for (int i = 0; i < candyball.Length; i++)
        {
            if (!candyball[i].activeInHierarchy)
                return i;
        }
        return 0;
    }
}

