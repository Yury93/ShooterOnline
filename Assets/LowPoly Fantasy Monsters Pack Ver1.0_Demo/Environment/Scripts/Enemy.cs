using UnityEngine;
using System.Collections;
using Photon.Pun;
public class Enemy : MonoBehaviourPunCallbacks
{
	[SerializeField] private int hp;
	[SerializeField] private GameObject target;
	[SerializeField] private float speed;
	private PhotonView view;
	[SerializeField] private Animator animator;
	private GameManager gameManager;
	private float timer;
    private void Start()
    {
		view = GetComponent<PhotonView>();
		gameManager = FindObjectOfType<GameManager>();
    }
    void Update () 
	{
		if (hp > 0)
		{
				if (target)
			{
				var dir = target.transform.position - transform.position;
				if (dir.sqrMagnitude > 1.5f)
				{
					float step = speed * Time.deltaTime;
                    transform.position = Vector3.MoveTowards(transform.position, target.transform.position, step);
                    animator.SetBool("attack01", false);
					animator.SetBool("run", true);
					transform.LookAt(target.transform);
				}
                else
                {
					animator.SetBool("run", false);
					animator.SetBool("attack01", true);
					if (timer <= 0)
					{
						view.RPC("OnAttackEnemy", RpcTarget.All);
						timer = 2f;
					}
                    else
                    {
						timer -= Time.deltaTime;
                    }
				}
			}
            else
            {
				animator.SetBool("run", false);
				animator.SetBool("attack01", false);
			}
			var coliders = Physics.OverlapSphere(transform.position, 1f);
			foreach (var col in coliders)
			{
				if (col.gameObject.CompareTag("Enivroment") || col.gameObject.CompareTag("Enemy")&& col.gameObject != gameObject)
				{
					print("Коллизия с другой сущностью!");
				}
			}
		}
        else
        {
			animator.SetBool("run", false);
			animator.SetBool("attack01", false);
			
		}
	}
	public void KillEnemy()
    {
		view.RPC("RPC_KillEnemy", RpcTarget.All);
	}
	[PunRPC]
	private void RPC_KillEnemy()
    {
			hp -= 5;
			if (hp < 0)
			{
			animator.SetBool("dead", true);
			Destroy(gameObject, 2f);
			}
	}
	[PunRPC]
	private void OnAttackEnemy()
	{
		if(target)
		target.GetComponent<PlayerController>().ApplyDamage(10);
	}
	public void SetTarget(GameObject target)
    {
		this.target = target;
    }
}
