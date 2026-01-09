using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System;


public class DragDropTower : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    [SerializeField] private AudioClip placeTowerSound;
    public GameObject towerPrefab;
    private GameObject currentTowerPreview;
    private TowerScript currentTowerScript;
    public GameObject map;
    public GameObject rock;
    private RectTransform rectTransform;
    private Canvas canvas;

    public float OffsetY;
    
    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvas = GetComponentInParent<Canvas>();
    }

    
    public void OnBeginDrag(PointerEventData eventData)
    {
        if (!GameManager.Instance.canPlaceTower(towerPrefab.GetComponent<TowerScript>().towerStats.towerCost) || GameManager.Instance.isPaused)
        {
            Debug.Log("Not enough coins to place tower");
            return;
        }
        OffsetY = 1; //Offset to ensure tower is placed above ground level
        if (SceneStackManager.Instance.Peek() == 9)
        {
            OffsetY +=1.02f;
        }

        // Instantiate a preview of the tower being dragged
        currentTowerPreview = Instantiate(towerPrefab.gameObject);
        currentTowerScript = currentTowerPreview.GetComponent<TowerScript>(); //Get the TowerScript component of the preview
        currentTowerScript.activeTower = false; //Disable tower acctack functionality during placement
        currentTowerPreview.transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition); //Set initial position to cursor position

    }

    public void OnDrag(PointerEventData eventData)
    {
        if (currentTowerPreview != null) 
        {
            Vector3 cursorPos = eventData.position; //Get cursor position in screen space
            Ray ray = Camera.main.ScreenPointToRay(cursorPos); //Create ray from camera to cursor position
            RaycastHit hit; //Store information about what ray hits

            if (SceneStackManager.Instance.Peek() == 10)
            {
                ;
            }

            if (map.GetComponent<Collider>().Raycast(ray, out hit, Mathf.Infinity)) //If ray hits the map collider
            {
            
                currentTowerPreview.transform.position = new Vector3(
                    hit.point.x,
                    OffsetY,
                    hit.point.z
                ); //Move tower preview to hit point with y offset of 1 so that it is level with the map
            }
        
        }   
    }

    public void OnEndDrag(PointerEventData eventData)
    {

        if (GameManager.Instance.canPlaceTower(towerPrefab.GetComponent<TowerScript>().towerStats.towerCost))
        {

            currentTowerPreview.transform.position = new Vector3(
                    Mathf.Round(currentTowerPreview.transform.position.x),
                    OffsetY,
                    Mathf.Round(currentTowerPreview.transform.position.z)
                );



            // Ensure Tower can be placed at location
            if (!currentTowerScript.CanBePlaced())
            {
                Debug.Log("Invalid tower placement - cancelling placement");
                Destroy(currentTowerPreview);//If it cannot be placed then destroy its instance
                return; //Exit early to prevent money deduction
            }
            currentTowerScript.activeTower = true; //Activate the tower attack functionality
            GameManager.Instance.deductCoins(towerPrefab.GetComponent<TowerScript>().towerStats.towerCost); //Deduct coins only if placement is valid
            AudioManager.Instance.PlaySFX(placeTowerSound); //Play tower placement sound
        }
        else //prevent the tower placement as not enough coins
        {
            Debug.Log("Not enough coins to place tower - cancelling placement"); 
        }
        currentTowerPreview = null; //Reset the preview variable so no accidental deletions occur
    }

}
