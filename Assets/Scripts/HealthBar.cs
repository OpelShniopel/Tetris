using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [field: SerializeField] public float Health { get; set; } = 3;
    [SerializeField] private Image fullHealthBar;
    [SerializeField] private Image currentHealthBar;

    public void Awake()
    {
        fullHealthBar.fillAmount = Health / 3;
    }

    public void Start()
    {
        fullHealthBar.fillAmount = Health / 3;
    }

    public void Update()
    {
        //playerHealth = GameObject.FindWithTag("Player").GetComponent<HealthController>();
        currentHealthBar.fillAmount = Health / 3;
    }

    public bool Damage()
    {
        Health -= 1f;
	    return Health <= 0;
    }
}
