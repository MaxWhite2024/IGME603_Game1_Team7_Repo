using TMPro;
using UnityEngine;

public class RatCountUI : MonoBehaviour
{
    [SerializeField] private PlayerRatCollection player;
    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private int requirement = 20;

    private void Start()
    {
        if (!player)
        {
            player = FindObjectOfType<PlayerRatCollection>();
        }
    }

    private void Update()
    {
        text.text = $"{player.ratCount}/{requirement}";
        if (player.ratCount >= requirement)
        {
            text.color = Color.green;
        }
        else if (player.ratCount <= 0)
        {
            text.color = Color.red;
        }
        else
        {
            text.color = Color.white;
        }
    }

    public void ChangeRequirement(int newRequirement)
    {
        requirement = newRequirement;
    }
}