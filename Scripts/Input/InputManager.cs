using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InputManager : MonoBehaviour, IPointerClickHandler, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    public System.Action<Tile> OnDragStarted;
    public System.Action<Vector2Int> OnDragResult;
    public System.Action<Tile> OnDragEnded;
    public System.Action<Tile> OnClick;
    public System.Action<Vector2> OnDragContinues;
    public LayerMask inputMask;
    public float dragResultThreshold = 5;
    Vector2 dragStartPosition;
    public void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log("On Begin Drag");
        RaycastHit2D hit = Physics2D.GetRayIntersection(Camera.main.ScreenPointToRay(Input.mousePosition), 50, 
            inputMask);

        if (hit.transform != null)
        {
            if (hit.transform.TryGetComponent(out Tile tile))
            {
                OnDragStarted?.Invoke(tile);
                dragStartPosition = Camera.main.ScreenToWorldPoint( eventData.position);
                return;
            }
        }
        //OnOtherInput?.Invoke();
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Debug.Log("On End Drag");

        RaycastHit2D hit = Physics2D.GetRayIntersection(Camera.main.ScreenPointToRay(Input.mousePosition), 50, inputMask);

        if (hit.transform != null)
        {
            if (hit.transform.TryGetComponent(out Tile tile))
            {
                OnDragEnded?.Invoke(tile);
                return;
            }
        }
        
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        //lastClickPointerEventData = eventData;

        Debug.Log("OnPointerClick");
        RaycastHit2D hit = Physics2D.GetRayIntersection(Camera.main.ScreenPointToRay(Input.mousePosition), 50, inputMask);
        if (hit.transform != null)
        {
            if (hit.transform.TryGetComponent(out Tile tile))
            {
                OnClick?.Invoke(tile);
                return;
            }
        }
        

    }

    public void OnDrag(PointerEventData eventData)
    {

        Vector2 dragWorldPosition = Camera.main.ScreenToWorldPoint(eventData.position);
        
        Vector2 positionDelta = dragWorldPosition-dragStartPosition;
        OnDragContinues?.Invoke(positionDelta);
        Vector2Int dragResult;
        if(Mathf.Abs(positionDelta.x)>dragResultThreshold||Mathf.Abs(positionDelta.y)>dragResultThreshold)
        {
            if(Mathf.Abs( positionDelta.y)>=Mathf.Abs(positionDelta.x))
            {
                positionDelta = new Vector2(0, positionDelta.y).normalized;
            }
            else
            {
                positionDelta = new Vector2(positionDelta.x, 0).normalized;

            }
            dragResult = new Vector2Int((int)positionDelta.x, (int)positionDelta.y);
            Debug.Log($"Drag result:{dragResult}");
            OnDragResult?.Invoke(dragResult);

        }

    }
}
