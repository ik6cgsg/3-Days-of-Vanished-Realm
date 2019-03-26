using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PickupableObjectController : IInteractiveObject
{
    public IInteractiveObject interactiveObject;
    public string itemName;
    public float maxDistance;

    private bool isWatched;

    // Start is called before the first frame update
    void Start()
    {
        // Add EventTrigger components
        EventTrigger trigger = gameObject.AddComponent(typeof(EventTrigger)) as EventTrigger;

        EventTrigger.Entry entryEnter = new EventTrigger.Entry();
        entryEnter.eventID = EventTriggerType.PointerEnter;
        entryEnter.callback.AddListener((eventData) => { OnPointerEnter(); });

        EventTrigger.Entry entryExit = new EventTrigger.Entry();
        entryExit.eventID = EventTriggerType.PointerExit;
        entryExit.callback.AddListener((eventData) => { OnPointerExit(); });

        EventTrigger.Entry entryClick = new EventTrigger.Entry();
        entryClick.eventID = EventTriggerType.PointerClick;
        entryClick.callback.AddListener((eventData) => { OnPointerClick(eventData); });

        trigger.triggers.Add(entryEnter);
        trigger.triggers.Add(entryExit);
        trigger.triggers.Add(entryClick);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void Interact()
    {
        IItem item = (IItem)ScriptableObject.CreateInstance(itemName + "Item");
        InventoryController.AddItem(item);
        Destroy(interactiveObject.gameObject);
    }

    public override bool CanInteract()
    {
        return true; // !InventoryContoller.IsFull();
    }

    private void OnPointerEnter()
    {
        isWatched = true;

        // Set up cursor mode
        if (interactiveObject.CanInteract())
        {
            if (GvrPointerInputModule.CurrentRaycastResult.distance <= maxDistance)
            {
                ; // Set cursor to can interact
            }
            else
            {
                ; // Set cursor to too far to interact
            }
        }
        else
        {
            ; // Set cursor to can't interact
        }
    }

    private void OnPointerExit()
    {
        isWatched = false;
        ; // Set cursor as neutral
    }

    private void OnPointerClick(BaseEventData eventData)
    {
        if (GvrPointerInputModule.CurrentRaycastResult.distance <= maxDistance
            && interactiveObject.CanInteract())
        {
            interactiveObject.Interact();
        }
    }
}
