using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeartSystem
{

    public Board board;
    public SpriteRenderer hearts;
    public Sprite sprites;
  
    // Start is called before the first frame update

    public HeartSystem(Board board)
    {
        this.board = board;
        //


        //hearts = gameObject.GetComponent<SpriteRenderer>();
        //sprites = Resources.LoadAll<sprite>("heart");
        //hearts.sprite = sprites;

    }

    
}
