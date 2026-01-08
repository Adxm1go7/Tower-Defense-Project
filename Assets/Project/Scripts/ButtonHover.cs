using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonHover : MonoBehaviour, IPointerEnterHandler, IPointerClickHandler
{
    [SerializeField] private AudioClip hoverSound;
    public bool hoverSoundEnabled;

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (hoverSoundEnabled)
        {
            AudioManager.Instance.PlaySFX(hoverSound);
        }
    }
    
    public void OnPointerClick(PointerEventData eventData)
    {
        if (!hoverSoundEnabled)
        {
            AudioManager.Instance.PlaySFX(hoverSound);
        }
    }
}
