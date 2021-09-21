using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckObjects : MonoBehaviour
{
    public void OnTriggerEnter2D(Collider2D other)
    {
        other.transform.parent = transform;
        transform.root.GetComponent<Player>().GetState=State.Shorten;
        for(int i=0;i<transform.childCount;i++)
        {
            transform.GetChild(i).GetComponent<Collider2D>().enabled = false;
        }
        transform.GetComponent<Collider2D>().enabled = false;
    }
}
