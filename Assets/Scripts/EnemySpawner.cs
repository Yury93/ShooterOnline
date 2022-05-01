using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class EnemySpawner : MonoBehaviourPunCallbacks
{
    [SerializeField] private GameManager gameManager;
    [SerializeField] private GameObject enemy;
    [SerializeField] private float timer;
    private float startTimer;
    [SerializeField] private int countEnemies;
    private PhotonView view;

    private void Start()
    {
        view = GetComponent<PhotonView>();
        startTimer = timer;
        gameManager = FindObjectOfType<GameManager>();
    }
    private void Update()
    {
        
        if (timer < 0)
        {
            for (int i = 0; i < countEnemies; i++)
            {
               var e =  PhotonNetwork.Instantiate(enemy.name,
                    new Vector3(transform.position.x + Random.Range(-10, 10),
                    transform.position.y,
                    transform.position.z + Random.Range(-10, 10)), Quaternion.identity);

                if (gameManager.PlayersList.Count > 0)
                {
                    e.GetComponent<Enemy>().SetTarget(gameManager.PlayersList[Random.Range(0, gameManager.PlayersList.Count)]);
                }

            }
            countEnemies++;
            timer = startTimer;
        }
        else
        {
            timer -= Time.deltaTime;
        }
    }
}
