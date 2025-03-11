using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CursorScript : MonoBehaviour
{
    [Header("Project Reference")]
    [SerializeField] private GameAssetSO gameAssetSO;
    [SerializeField] private GameEventSO gameEventSO;

    [SerializeField] private GameObject cursorGo;
    [SerializeField] private float scaleSizeMultipler = 1.2f;

    private void Start()
    {
        SetNewItemDetail();
    }

    void OnEnable()
    {
        gameEventSO.OnInventoryTabChanged += SetNewItemDetail;
    }

    void OnDisable()
    {
        gameEventSO.OnInventoryTabChanged -= SetNewItemDetail;
    }

    void Update()
    {
        FollowCurrentSelected();
    }

    private void SetNewItemDetail()
    {
        if (EventSystem.current.currentSelectedGameObject != null && EventSystem.current.currentSelectedGameObject.GetComponent<CraftingInventorySlotUI>())
        {
            gameEventSO.OnInventoryItemHoveredOver?.Invoke(EventSystem.current.currentSelectedGameObject.GetComponent<CraftingInventorySlotUI>().ItemSO);
        }
    }

    public void FollowCurrentSelected()
    {
        //Matches the position of the cursor to the currently selected game object
        //Used in the radial, spiral, and grid demos

        if (EventSystem.current.currentSelectedGameObject != null && EventSystem.current.currentSelectedGameObject.GetComponent<CraftingInventorySlotUI>())
        {
            transform.position = new Vector3(EventSystem.current.currentSelectedGameObject.transform.position.x,
                EventSystem.current.currentSelectedGameObject.transform.position.y, 0);

            var rect = (RectTransform)transform;
            rect.sizeDelta = EventSystem.current.currentSelectedGameObject.GetComponent<RectTransform>().sizeDelta * scaleSizeMultipler;
        }
    }

    public void HideCursor()
    {
        cursorGo.SetActive(false);
    }

    public void ShowCursor()
    {
        FollowCurrentSelected();
        cursorGo.SetActive(true);
    }
}
