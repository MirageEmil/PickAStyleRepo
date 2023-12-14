using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class BombController : MonoBehaviour
{
    public GameObject bombPrefab;

    public Tilemap breakableTiles;

    public ExplodeController explosionPrefab;
    public Breakable breakablePrefab;

    public LayerMask explodeLayerMask;
    
    public float explodeTime;
    public float bombTime;

    public int explodeRadius;
    public int bombAmount;
    private int bombsRemain;

    private void OnEnable()
    {
        bombsRemain = bombAmount;

    }

    // Update is called once per frame
    private void Update()
    {
        if(bombsRemain > 0 && Input.GetButtonDown("ColorBomb") && CompareTag("Player1"))
        {
            StartCoroutine(PlaceBomb());

        }

        if(bombsRemain > 0 && Input.GetButtonDown("MonoBomb") && CompareTag("Player2"))
        {
            StartCoroutine(PlaceBomb());

        }

    }

    private IEnumerator PlaceBomb()
    {
        Vector2 position = transform.position;

        position.x = Mathf.Round(position.x);
        position.y = Mathf.Round(position.y);

        GameObject bomb = Instantiate(bombPrefab, position, Quaternion.identity);

        bombsRemain--;

        yield return new WaitForSeconds(bombTime);

        position = bomb.transform.position;
        position.x = Mathf.Round(position.x);
        position.y = Mathf.Round(position.y);

        ExplodeController explode = Instantiate(explosionPrefab, position, Quaternion.identity);

        explode.SetActiveRenderer(explode.start);
        explode.DestroyAfter(explodeTime);

        Destroy(explode.gameObject, explodeTime);

        Explosion(position, Vector2.up, explodeRadius);
        Explosion(position, Vector2.down, explodeRadius);
        Explosion(position, Vector2.left, explodeRadius);
        Explosion(position, Vector2.right, explodeRadius);

        Destroy(bomb);

        bombsRemain++;

    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if(other.gameObject.layer == LayerMask.NameToLayer("Bomb"))
        {
            other.isTrigger = false;

        }

    }

    private void Explosion(Vector2 position, Vector2 direction, int length)
    {
        if(length <= 0)
        {
            return;

        }

        position += direction;

        if(Physics2D.OverlapBox(position, Vector2.one / 2f, 0f, explodeLayerMask))
        {
            ClearBreakable(position);

            return;
        }

        ExplodeController explode = Instantiate(explosionPrefab, position, Quaternion.identity);

        explode.SetActiveRenderer(length > 1 ? explode.mid : explode.end);
        explode.SetDirection(direction);
        explode.DestroyAfter(explodeTime);

        Destroy(explode.gameObject, explodeTime);

        Explosion(position, direction, length -1);

    }

    private void ClearBreakable(Vector2 position)
    {
        Vector3Int cell = breakableTiles.WorldToCell(position);
        TileBase tile = breakableTiles.GetTile(cell);

        if(tile != null)
        {
            Instantiate(breakablePrefab, position, Quaternion.identity);

            breakableTiles.SetTile(cell, null);

        }

    }

    public void AddBomb()
    {
        bombAmount++;
        bombsRemain++;

    }

}
