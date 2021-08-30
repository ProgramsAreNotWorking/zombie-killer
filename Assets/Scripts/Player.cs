using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class Player : MonoBehaviour
{

    [Header("Movement")]//move
    [SerializeField] Rigidbody2D rigid;
    private bool canMove = true;
    [SerializeField] float speed = 4;

    [Header("Vida")]
    #region //vida
    [SerializeField] int vida = 5;

        //hacer inmune
    private bool inmunity;

    [SerializeField] float resetInmunityTime;
    [SerializeField] float knockbackForce = 14;

    [SerializeField] float damegeShakeFrequence;
    private float damegeShakeAmplitude = 5;

    private bool damageShake; //si el shake del daño se activo
    #endregion

    //cambiar color
    [SerializeField] SpriteRenderer spriteRender;
    [SerializeField] Color color;

    [Header("Shoot")] 
    #region //shoot 
    [SerializeField] float bulletSpeed = 5;
    [SerializeField] Transform bulletSpawner;

        //frequencia de disparo
    [SerializeField] float frecuencia = 0.1f;
    private float timeToWait;

        //shake
    [SerializeField] float shootShakeFrequence;
    private float shootShakeAmplitude = 0.5f;
    [SerializeField] float shootShakeDuration;
    #endregion

    //play audio
    [SerializeField] AudioManager audio;

    // Start is called before the first frame update
    void Awake()
    {
        spriteRender = GetComponent<SpriteRenderer>();
        audio = GetComponent<AudioManager>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Shoot();
        Death();
    }

    void Move()
    {
        if (canMove)
        {
            Vector2 direction = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
            rigid.velocity = direction * speed;
        }
    }

    void Death()
    {
        if(vida < 0) print("moriste");
    }

    void Damage()//recibir daño
    {
        if(!inmunity)
        {
            vida -= 1;
            inmunity = true;
            spriteRender.color = color;
            damageShake = true;
            canMove = false;

            audio.playAudio("hit", 0.4f);//ejecuta el sonido especificado y reduce el pitch

            CinemachineShake.cinemachineShake.ShakeCamera(damegeShakeFrequence, damegeShakeAmplitude, resetInmunityTime);

            StartCoroutine("ResetInmunity");
        }
    }

    void KnockBack(Transform enemy)//aplica una fuerza al chocar con un enemigo
    {
        Vector2 playerEnemy = transform.position - enemy.position;
        rigid.AddForce(playerEnemy.normalized * knockbackForce, ForceMode2D.Impulse);
    }

    void Shoot()//disparar
    {
        timeToWait -= 1 * Time.deltaTime;

        if (timeToWait <= 0)
        {
            if (!damageShake)
            {
                if (Input.GetMouseButton(0))
                {
                    //le pide al objectpooling que active la bala y se la devuelva
                    GameObject bullet = BulletPooling.bulletPooling.GetBullet(bulletSpawner.position, bulletSpawner.rotation);

                    //le aplica una fuerza a la bala
                    bullet.GetComponent<Rigidbody2D>().AddForce(bulletSpawner.right * bulletSpeed, ForceMode2D.Impulse);
                    bullet.GetComponent<Bullet>().Destroy(2);//le dice en cuanto tiempo se debe destruir

                    timeToWait = 1 / frecuencia;

                    CinemachineShake.cinemachineShake.ShakeCamera(shootShakeFrequence, shootShakeAmplitude, shootShakeDuration);

                    audio.playAudio("shoot");
                }
            }
        }
    }

    //corrutinas
    IEnumerator ResetInmunity()
    {
        yield return new WaitForSeconds(resetInmunityTime);
        inmunity = false;
        spriteRender.color = new Color(255, 255, 255, 255);
        damageShake = false;
        canMove = true;
    }
    //cosas de unity
    private void OnTriggerEnter2D(Collider2D col)
    {
        //todo falta codigo

        if(col.CompareTag("Enemy"))
        {
            print("perdi vida");
            Damage();
            KnockBack(col.transform);
        }
    }
}
