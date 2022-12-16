using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DVDCheck : MonoBehaviour
{
    public Sprite checkedSprite;
    private GameObject player;
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        player.GetComponent<PlayerCollect>().OnDVDCollect += GetDVD;
    }
    public void GetDVD()
    {
        GetComponent<Image>().sprite = checkedSprite;
    }
}
