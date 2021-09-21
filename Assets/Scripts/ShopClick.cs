using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopClick : MonoBehaviour
{
    public int num;
    public ShopManager shopManager;
    private void Start()
    {
        Button btn = this.GetComponent<Button>();
        btn.onClick.AddListener(OnClick);
    }
    public void OnClick()
    {

        if(num==1)
        shopManager.TextOneSee();
        else if(num==2)
            shopManager.TextTwoSee();
        else
            shopManager.TextThreeSee();
    }
}
