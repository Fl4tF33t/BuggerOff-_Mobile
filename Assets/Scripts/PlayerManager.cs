using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerManager : Singleton<PlayerManager>
{
    LayerMask frogLayer;
    FrogBrain frogBrain;

    void Start()
    {
        frogLayer = LayerMask.GetMask("Frog");
        InputManager.Instance.OnTouchTap += InputManager_OnTouchTap;
    }

    void InputManager_OnTouchTap(Vector2 obj)
    {
        Ray ray = Camera.main.ScreenPointToRay(obj);
        RaycastHit hit;

        // Check if the ray hits a frog GameObject
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, frogLayer))
        {
            if (frogBrain != null)
            {
                frogBrain.OnUpgradeUI(false);
                frogBrain = hit.collider.gameObject.GetComponent<FrogBrain>();
                frogBrain.OnUpgradeUI(true);
                return;
            }
            // Get the GameObject hit by the raycast
            frogBrain = hit.collider.gameObject.GetComponent<FrogBrain>();
            frogBrain.OnUpgradeUI(true);
        }
        else
        {
            if(frogBrain != null)
            {
                frogBrain.OnUpgradeUI(false);
                frogBrain = null;
            }
        }
    }
}
