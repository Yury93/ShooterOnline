using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Cinemachine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private PlayerUI playerUI;
    [SerializeField] private CinemachineVirtualCamera cam;
    [SerializeField] private int hp,score;
    public int HP => hp;
    public int Score => score;
    [SerializeField] private float speed;
    [SerializeField] private float radiusBoundary;
    private Vector3 inputMove;
    [SerializeField] private Joystick joystick;
    private PhotonView view;
    private Animator animator;
    private bool collision;
    #region UnityAPI
    private void Start()
    {
        playerUI = GetComponentInChildren<PlayerUI>();
        joystick = FindObjectOfType<Joystick>();
        view = GetComponent<PhotonView>();
        animator = GetComponentInChildren<Animator>();
        if(view.IsMine)
        {
            cam = FindObjectOfType<CinemachineVirtualCamera>();
            cam.Follow = gameObject.GetComponent<Transform>();
            cam.LookAt = gameObject.GetComponent<Transform>();
        }
    }
    
    private void Update()
    {
        if (view.IsMine)
        {
            if (hp > 0)
            {
                if (collision == false)
                {
                    inputMove = new Vector3(joystick.Horizontal, 0, joystick.Vertical);
                }
                else
                {
                    inputMove = new Vector3(-joystick.Horizontal, 0, -joystick.Vertical);
                }

                StayCollision();

                if (inputMove != Vector3.zero)
                {
                    transform.Translate(inputMove * speed * Time.deltaTime, Space.World);
                    Vector3 offset = transform.position - Vector3.zero;
                    transform.position = Vector3.zero + Vector3.ClampMagnitude(offset, radiusBoundary);
                }
                transform.LookAt(transform.position + inputMove);

                if (joystick.Direction.x != 0 || joystick.Direction.y != 0)
                {
                    animator.SetBool("Run", true);
                }
                else
                {
                    animator.SetBool("Run", false);
                }
            }
        }
    }
    #endregion

    private void StayCollision()
    {
        var coliders = Physics.OverlapSphere(transform.position, 1f);
        foreach (var col in coliders)
        {
            if(col.gameObject.CompareTag("Enivroment"))
            {
                collision = true;
            }
            else if(collision == true)
            {
                StartCoroutine(CorNonCollision());
                IEnumerator CorNonCollision()
                {
                    yield return new WaitForSeconds(1f);
                    collision = false;
                }
            }
        }
    }
    public void ApplyDamage(int damage)
    {
        playerUI.HpTranslate();
        hp -= damage;
        if(hp < 0)
        {
            animator.SetBool("Death", true);
            Destroy(gameObject, 2f);
        }
    }
    public void ScoreAdd(int scor)
    {
        score = scor;
        playerUI.ScoreTranslate();
        ///TODO: сделать ставки!!!
    }
}
