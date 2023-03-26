using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeartSystem
{

    public Board board;
    public SpriteRenderer hearts;
    public Sprite sprite;
  
    // Start is called before the first frame update

    public HeartSystem(Board board)
    {
        this.board = board;
        //hearts = GetComponent<SpriteRenderer>();


        var sprite = Resources.Load<Texture2D>("Sprites/heart");
        sprite = Sprite.Create(tex, new Rect(0.0f, 0.0f, tex.width, tex.height), new Vector2(0.5f, 0.5f), 100.0f);
        //hearts = gameObject.GetComponent<SpriteRenderer>();
        hearts.sprite = sprite;
        
        
        
        //hearts.sprite = tex;

    }

    
}
