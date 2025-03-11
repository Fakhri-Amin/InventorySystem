using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

public class CraftingInputManager : MonoBehaviour
{
    public static CraftingInputManager Instance { get; private set; }

    public InputAction playerInput; // Reference to input action

    [SerializeField] private float delayBetweenCraft = 0.1f;

    private Coroutine craftingCoroutine;
    private CraftingInventorySlotUI currentSlot;
    private bool isCrafting;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    private void OnEnable()
    {
        playerInput.Enable();
        playerInput.performed += StartCrafting;
        playerInput.canceled += StopCrafting;
    }

    private void OnDisable()
    {
        playerInput.performed -= StartCrafting;
        playerInput.canceled -= StopCrafting;
        playerInput.Disable();
    }

    private void StartCrafting(InputAction.CallbackContext context)
    {
        if (currentSlot == null) return;

        if (!isCrafting)
        {
            isCrafting = true;
            craftingCoroutine = StartCoroutine(CraftingLoop());
        }
    }

    private void StopCrafting(InputAction.CallbackContext context)
    {
        isCrafting = false;
        if (craftingCoroutine != null)
        {
            StopCoroutine(craftingCoroutine);
            craftingCoroutine = null;
        }
    }

    private IEnumerator CraftingLoop()
    {
        while (isCrafting)
        {
            currentSlot?.CraftItem(); // Calls the craft function on the active slot
            yield return new WaitForSeconds(delayBetweenCraft);
        }
    }

    // Called by inventory slots when hovered or clicked
    public void SetCurrentSlot(CraftingInventorySlotUI slot)
    {
        currentSlot = slot;
    }
}
