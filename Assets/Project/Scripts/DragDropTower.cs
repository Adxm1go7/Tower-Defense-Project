using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System;

public class DragDropTower : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
{

    public GameManager gameManager;
    public TowerScript towerPrefab;
    private GameObject currentTowerPreview;
    private TowerScript currentTowerScript;
    public GameObject map;
    private RectTransform rectTransform;
    private Canvas canvas;
    
    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvas = GetComponentInParent<Canvas>();
    }

    
    public void OnBeginDrag(PointerEventData eventData)
    {

        if (!gameManager.canPlaceTower(towerPrefab.towerCost))
        {
            Debug.Log("Not enough coins to place tower");
            return;
        }
        
        currentTowerPreview = Instantiate(towerPrefab.gameObject);
        currentTowerScript = currentTowerPreview.GetComponent<TowerScript>();
        currentTowerScript.activeTower = false;
        currentTowerPreview.transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);

    }

    public void OnDrag(PointerEventData eventData)
    {
        if (currentTowerPreview != null)
        {
            Vector3 cursorPos = eventData.position;
            Ray ray = Camera.main.ScreenPointToRay(cursorPos);
            RaycastHit hit;

            if (map.GetComponent<Collider>().Raycast(ray, out hit, Mathf.Infinity))
            {
                currentTowerPreview.transform.position = new Vector3(
                    hit.point.x,
                    1,
                    hit.point.z
                );
            }
        }   
    }

    public void OnEndDrag(PointerEventData eventData)
    {

        if (gameManager.canPlaceTower(towerPrefab.towerCost))
        {

            currentTowerPreview.transform.position = new Vector3(
                    Mathf.Round(currentTowerPreview.transform.position.x),
                    1,
                    Mathf.Round(currentTowerPreview.transform.position.z)
                );
            currentTowerScript.activeTower = true;

            gameManager.deductCoins(towerPrefab.towerCost);
        }
        else
        {
            Debug.Log("Not enough coins to place tower - cancelling placement");
        }
        currentTowerPreview = null;
    }

    // public bool isTowerPlacementAllowed(Vector3 position)
    // {
    //     RaycastHit hit;
    //     Debug.Log("Checking tower placement at position: " + position);
    //     if (Physics.Raycast(position + Vector3.up, Vector3.down, out hit, 100f))
    //     {
    //         Debug.Log("Raycast hit: " + hit.collider.name);
    //         if (hit.collider.CompareTag("EnemyPath"))
    //         {
                
    //             return false;
    //         }
    //         else
    //         {
    //             return true;
    //         }
    //     }
    //     return false;
    // }

}
