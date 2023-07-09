//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class PlayerDie : MonoBehaviour
//{
//    public int MaxCount = 3;
//    public static int count = 0;
//    public LoseUIManager lose;
//    public GameObject[] heart;
//    private GameObject[] origin;

//    private void Start()
//    {
//        lose = FindObjectOfType<LoseUIManager>();
//        origin = (GameObject[])heart.Clone();
//        count = 0;
//    }

//    // Update is called once per frame
//    void Update()
//    {
//        if (count >= MaxCount)
//        {
//            lose.AfterLoseGame();
//        }

//        if (count <= MaxCount)
//        {
//            heart[count - 1].SetActive(false);
//        }
//    }

//    private void ResetObjects()
//    {
//        for (int i = 0; i < heart.Length; i++)
//        {
//            heart[i].SetActive(true);
//        }
//    }

//    void RestartGame()
//    {
//        count = 0;
//        heart = (GameObject[])origin.Clone();

//        ResetObjects();
//    }

//}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PlayerDie : MonoBehaviour
{
    //public int health;
    //public int numOfHearts;

    public Image[] hearts;
    public Sprite fullHeart;
    public Sprite emptyHeart;
    public int MaxCount = 3;
    public static int count = 0;
    public LoseUIManager lose;

    private void Update()
    {
        for (int i = 0; i < hearts.Length; i++)
        {
            if(i < count)
            {
                hearts[i].sprite = fullHeart;
            }
            else
            {
                hearts[i].sprite = emptyHeart;
            }
            if(i < MaxCount)
            {
                hearts[i].enabled = true;
            }
            else
            {
                hearts[i].enabled = false;
            }
        }
        if(count >= MaxCount)
        {
            lose.AfterLoseGame();
            count = 0;
        }
        
    }

}


