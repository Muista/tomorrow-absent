using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class ShopManager : MonoBehaviour
{
    public GameObject boom;
    public GameObject water;
    public GameObject book;
    public Image boomImage;
    public Image waterImage;
    public Image bookImage;
    public Text boomCost;
    public Text waterCost;
    public Text bookCost;
    public Text boomExplanation;
    public Text waterExplanation;
    public Text bookExplanation;
    int i;
    public int choice;

    public void ShopInit()
    {
        choice = 0;
        boomExplanation.color = new Color(255, 255, 255, 0);
        waterExplanation.color = new Color(255, 255, 255, 0);
        bookExplanation.color = new Color(255, 255, 255, 0);
        i = Random.Range(0, 2);//1/2概率
        if(i==0)
        {
            boom.SetActive(false);
            boomImage.color = new Color(255, 255, 255, 0);
            boomCost.color= new Color(255, 255, 255, 0);

        }
        else
        {
            boom.SetActive(true);
            boomImage.color = new Color(255, 255, 255, 255);
            boomCost.color = new Color(255, 255, 255, 255);
        }
        i = Random.Range(0, 3);//1/3概率
        if (i == 0||i==1)
        {
            water.SetActive(false);
            waterImage.color = new Color(255, 255, 255, 0);
            waterCost.color = new Color(255, 255, 255, 0);

        }
        else
        {
            water.SetActive(true);
            waterImage.color = new Color(255, 255, 255, 255);
            waterCost.color = new Color(255, 255, 255, 255);
        }
        i = Random.Range(0, 4);//1/4
        if (i == 0||i==1||i==2)
        {
            book.SetActive(false);
            bookImage.color = new Color(255, 255, 255, 0);
            bookCost.color = new Color(255, 255, 255, 0);

        }
        else
        {
            book.SetActive(true);
            bookImage.color = new Color(255, 255, 255, 255);
            bookCost.color = new Color(255, 255, 255, 255);
        }


    }
    public void TextOneSee()
    {
        choice = 1;
        boomExplanation.color=new Color(255, 255, 255, 255);
        waterExplanation.color = new Color(255, 255, 255, 0);
        bookExplanation.color = new Color(255, 255, 255, 0);
    }
    public void TextTwoSee()
    {
        choice = 2;
        boomExplanation.color = new Color(255, 255, 255, 0);
        waterExplanation.color = new Color(255, 255, 255, 255);
        bookExplanation.color = new Color(255, 255, 255, 0);
    }
    public void TextThreeSee()
    {
        choice = 3;
        boomExplanation.color = new Color(255, 255, 255, 0);
        waterExplanation.color = new Color(255, 255, 255, 0);
        bookExplanation.color = new Color(255, 255, 255, 255);
    }
    public void DestroyProduct()
    {
        if (choice == 0)
            return;
        else
        {
            if(choice==1)
            {
                boom.SetActive(false);
                boomImage.color = new Color(255, 255, 255, 0);
                boomCost.color = new Color(255, 255, 255, 0);
            }
            else if(choice==2)
            {
                water.SetActive(true);
                waterImage.color = new Color(255, 255, 255, 0);
                waterCost.color = new Color(255, 255, 255, 0);
            }
            else if(choice==3)
            {
                book.SetActive(false);
                bookImage.color = new Color(255, 255, 255, 0);
                bookCost.color = new Color(255, 255, 255, 0);
            }
        }
    }
}
