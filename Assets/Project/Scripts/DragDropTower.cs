using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System;

public class DragDropTower : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    public GameObject towerPrefab;
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

        if (!GameManager.Instance.canPlaceTower(towerPrefab.GetComponent<TowerScript>().towerCost))
        {
            Debug.Log("Not enough coins to place tower");
            return;
        }
        
        currentTowerPreview = Instantiate(towerPrefab);
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

        if (GameManager.Instance.canPlaceTower(towerPrefab.GetComponent<TowerScript>().towerCost))
        {

            currentTowerPreview.transform.position = new Vector3(
                    Mathf.Round(currentTowerPreview.transform.position.x),
                    1,
                    Mathf.Round(currentTowerPreview.transform.position.z)
                );
            currentTowerScript.activeTower = true;

            if (!currentTowerScript.CanBePlaced())
            {
                Debug.Log("Invalid tower placement - cancelling placement");
                Destroy(currentTowerPreview);
                return;
            }

            GameManager.Instance.deductCoins(towerPrefab.GetComponent<TowerScript>().towerCost);
        }
        else
        {
            Debug.Log("Not enough coins to place tower - cancelling placement");
        }
        currentTowerPreview = null;
    }

    public bool isTowerPlacementAllowed(Vector3 position)
    {
        RaycastHit hit;
        Debug.Log("Checking tower placement at position: " + position);
        if (Physics.Raycast(position + Vector3.up * 5.0f, Vector3.down, out hit, 10f))
        {
            Debug.Log("Raycast hit: " + hit.collider.name);
            if (hit.collider.CompareTag("EnemyPath"))
            {
                Debug.Log("Tower placement not allowed on EnemyPath");
                return false;
            }
            Debug.Log("Tower placement allowed");
            return true;

        }
        return false;
    }

}
