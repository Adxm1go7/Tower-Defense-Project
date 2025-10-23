using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System;

public class DragDropTower : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler
{

    public GameManager gameManager;
    public TowerScript towerPrefab;
    private GameObject currentTowerPreview;
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
        Debug.Log("Begin dragging tower");

        if (gameManager.canPlaceTower(towerPrefab.towerCost))
        {
            currentTowerPreview = Instantiate(towerPrefab.gameObject);
            currentTowerPreview.transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        } else
        {
            Debug.Log("Not enough coins to place tower");
        }

    }

    public void OnDrag(PointerEventData eventData)
    {
        Debug.Log("Dragging tower");
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
        Debug.Log("End dragging tower");

        if (gameManager.canPlaceTower(towerPrefab.towerCost))
        {

            currentTowerPreview.transform.position = new Vector3(
                    Mathf.Round(currentTowerPreview.transform.position.x),
                    1,
                    Mathf.Round(currentTowerPreview.transform.position.z)
                );

            gameManager.deductCoins(towerPrefab.towerCost);
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("Tower Clicked");
    }


}
