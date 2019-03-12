using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InteractiveObjectController : MonoBehaviour
{
    public EventTrigger.TriggerEvent interactionCallback;
    public float maxDistance;
    public bool canInteractTwice;

    private bool isWatched;
    private bool canInteract = true;

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

    void Update()
    {
    }

    private void OnPointerEnter()
    {
        isWatched = true;

        // Set up cursor mode
        if (canInteract) // don't change from neutral if can't interact
        {
            if (GvrPointerInputModule.CurrentRaycastResult.distance <= maxDistance)
            {
                ; // Set cursor to can interact
            }
            else
            {
                ; // Set cursor to can't interact
            }
        }
    }

    private void OnPointerExit()
    {
        isWatched = false;
        ; // Set cursor as neutral
    }

    private void OnPointerClick(BaseEventData eventData)
    {
        if (canInteract && GvrPointerInputModule.CurrentRaycastResult.distance <= maxDistance)
        {
            interactionCallback.Invoke(eventData);
            if (!canInteractTwice)
            {
                canInteract = false;
            }
        }
    }
}
