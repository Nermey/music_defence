using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;

public class TowerManager : Loader<TowerManager>
{
    TowerButton towerBtnPressed;

    SpriteRenderer spriteRenderer;
    
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 mousePoint = Camera.main.ScreenToWorldPoint(Input.mousePosition); // постановак башни
            RaycastHit2D hit = Physics2D.Raycast(mousePoint, Vector2.zero);

            if (hit.collider.tag == "TowerSide")
            {
                hit.collider.tag = "TowerSideFull";
                PlaceTower(hit);
            }
        }
        if (spriteRenderer.enabled)
        {
            FollowMouse();
        }
    }

    public void SelectedTower(TowerButton towerSelected)
    {
        towerBtnPressed = towerSelected;
        EnableDrag(towerBtnPressed.DragSprite);
    }

    public void PlaceTower(RaycastHit2D hit)
    {
        if (!EventSystem.current.IsPointerOverGameObject() && towerBtnPressed != null)
        { 
            GameObject newTower = Instantiate(towerBtnPressed.TowerObject);
            newTower.transform.position = hit.transform.position;
            DisableDrag();
        }
    }

    public void FollowMouse()
    {
        transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = new Vector2(transform.position.x, transform.position.y);
    }

    public void EnableDrag(Sprite sprite)
    {
        spriteRenderer.enabled = true;
        spriteRenderer.sprite = sprite;
    }

    public void DisableDrag()
    {
        spriteRenderer.enabled = false;
    }
}
