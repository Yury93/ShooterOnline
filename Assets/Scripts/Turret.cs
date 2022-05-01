using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;

public class Turret : MonoBehaviourPunCallbacks
{
    [SerializeField] private Button shootButton;
    [SerializeField] private GameObject prefabBullet;
    [SerializeField] private Transform placeSpawnBullet;
    private PhotonView view;
    private GameObject bullet;
    private void Start()
    {
        view = GetComponentInParent<PhotonView>();
        shootButton = FindObjectOfType<Button>();
        shootButton.onClick.AddListener(SpawnBullet);
    }
    public void SpawnBullet()
    {
        if (view.IsMine)
        {
            bullet = PhotonNetwork.Instantiate(prefabBullet.name, placeSpawnBullet.position, Quaternion.identity);
            bullet.GetComponent<Bullet>().InitDirectionTurret(gameObject);
           
        }
        bullet.GetComponent<Bullet>().SetShooter(gameObject.GetComponentInParent<PlayerController>());
    }
}
