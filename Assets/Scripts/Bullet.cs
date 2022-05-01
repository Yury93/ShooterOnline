using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Bullet : MonoBehaviour
{
    [SerializeField] private PlayerController shooter;
    [SerializeField] private float speed;
    private Vector3 dir;

    public void SetShooter(PlayerController shot)
    {
        shooter = shot;
    }
    private void Start()
    {
        Destroy(gameObject, 2f);
    }
    public void  InitDirectionTurret(GameObject tur)
    {
        dir = tur.transform.forward;
    }
    private void Update()
    {
        transform.Translate(dir * speed * Time.deltaTime);
    }
    private void OnCollisionEnter(Collision collision)
    {
        var e = collision.gameObject.GetComponent<Enemy>();
        if (e)
        {
            e.KillEnemy();
            shooter.ScoreAdd(1);
        }
        Destroy(gameObject);
    }
}
