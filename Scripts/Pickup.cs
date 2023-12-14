using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    public enum PickupType
    {
        Bomb,
        Fire,
        Speed,
    }

    public PickupType type;

    private void OnPickup(GameObject player)
    {
        switch (type)
        {
            case PickupType.Bomb:
                player.GetComponent<BombController>().AddBomb();
                break;

            case PickupType.Fire:
                player.GetComponent<BombController>().explodeRadius++;
                break;

            case PickupType.Speed:
                player.GetComponent<PlayerController>().speed++;
                break;

        }

        Destroy(gameObject);

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            OnPickup(other.gameObject);
        }

    }

}
