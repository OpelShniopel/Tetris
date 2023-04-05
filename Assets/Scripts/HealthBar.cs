using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] public float health;
    [SerializeField] private Image fullHealthBar;
    [SerializeField] private Image currentHealthBar;

    public void Awake()
    {
        fullHealthBar.fillAmount = health / 3;
    }

    public void Start() {
        fullHealthBar.fillAmount = health / 3;
    }

    public void Update() {
        //playerHealth = GameObject.FindWithTag("Player").GetComponent<HealthController>();
        currentHealthBar.fillAmount = health / 3;
    }

    public bool Damage()
    {
        health -= 1f;
	    if(health <= 0)
	        return true;
        return false;
    }

    
}
