using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

public class CraftingInputManager : MonoBehaviour
{
    public static CraftingInputManager Instance { get; private set; }

    public InputAction playerInput;

    [SerializeField] private float delayBetweenCraft = 0.1f;
    [SerializeField] float minDelayBetweenCraft = 0.075f;
    [SerializeField] float delayDecreaseAmountPerCycle = 0.005f;

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
        float currentDelay = delayBetweenCraft;

        while (isCrafting)
        {
            currentSlot?.CraftItem();

            yield return new WaitForSeconds(currentDelay);

            // Reduce delay gradually but stop at minDelay
            currentDelay = Mathf.Max(currentDelay - delayDecreaseAmountPerCycle, minDelayBetweenCraft);
        }
    }


    // Called by inventory slots when hovered or clicked
    public void SetCurrentSlot(CraftingInventorySlotUI slot)
    {
        currentSlot = slot;
    }
}
