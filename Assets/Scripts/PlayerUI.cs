using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviourPunCallbacks
{
    [SerializeField] private Text hpTxt, scoreTxt;
    
    private PhotonView view;
    private PlayerController playerController;
    private void Start()
    {
        view = GetComponent<PhotonView>();
        playerController = gameObject.GetComponentInParent<PlayerController>();
        if (view.IsMine)
        {
            hpTxt.gameObject.SetActive(true);
            scoreTxt.gameObject.SetActive(true);
            hpTxt.text = "HP: " + playerController.HP.ToString();

        }
        else
        {
            hpTxt.gameObject.SetActive(false);
            scoreTxt.gameObject.SetActive(false);
        }
    }
    public void HpTranslate()
    {
        if (playerController.HP > 0)
            hpTxt.text = "HP: " + playerController.HP.ToString();
        else
            hpTxt.text = "HP: 0";
    }
    public void ScoreTranslate()
    {
        scoreTxt.text = "SCORE: " + playerController.Score.ToString();
    }
}
