using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplodeController : MonoBehaviour
{
    public PlayerAnim start;
    public PlayerAnim mid;
    public PlayerAnim end;

    public void SetActiveRenderer(PlayerAnim renderer)
    {
        start.enabled = renderer == start;
        mid.enabled = renderer == mid;
        end.enabled = renderer == end;

    }
    
    public void SetDirection(Vector2 direction)
    {
        float angle = Mathf.Atan2(direction.y, direction.x);

        transform.rotation = Quaternion.AngleAxis(angle * Mathf.Rad2Deg, Vector3.forward);

    }

    public void DestroyAfter(float seconds)
    {
        Destroy(gameObject, seconds);

    }

}
