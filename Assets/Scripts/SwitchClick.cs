using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SwitchClick : MonoBehaviour
{
    public Player player;
    public Image image;
    public int i;

    private void Start()
    {
        i = player.playerNum;
        Button btn = this.GetComponent<Button>();
        btn.onClick.AddListener(OnClick);
    }
    public void OnClick()
    {
        if(i==0)
        {
            image.color = new Color(255, 255, 255, 255);
            i = 1;
            player.playerNum = 1;
        }
        
        else
        {
            image.color = new Color(255, 255, 255, 0);
            i = 0;
            player.playerNum = 0;
        }
    }
}
