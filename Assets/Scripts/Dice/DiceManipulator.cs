using UnityEditor;
using UnityEngine;

public class DiceManipulator : MonoBehaviour
{
    [Header("References")]
    public Camera Camera;

    [Header("Raycast Settings")]
    public LayerMask ManipulatableLayerMask;
    public LayerMask GridLayerMask;
    public LayerMask BoxCastMask;

    //Internals
    Manipulatable currentlyHeld;
    Vector3 grabOffset = Vector3.zero;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
            OnLeftMouseClick();

        if (Input.GetMouseButtonDown(1))
            OnRightMouseClick();

        if (Input.GetMouseButtonDown(2))
            OnMiddleMouseClick();

        MoveCurrentlyHeld();
    }



    void OnLeftMouseClick()
    {
        if (currentlyHeld == null)
        {
            //Try to grab some
            Manipulatable manipulatable = RaycastForManipulatable();

            if (manipulatable != null)
            {
                //Got a cube
                if (manipulatable.EnableMove)
                {
                    //Can move it
                    currentlyHeld = manipulatable;
                    currentlyHeld.Grab();

                    Vector3? point = RaycastForGrid(false);

                    if (point != null)
                        grabOffset = currentlyHeld.transform.position - point.Value;
                    else
                        grabOffset = Vector3.zero;
                }
            }
        }
        else
        {
            //Try to release
            Vector3? point = RaycastForGrid(true);

            if (point != null)
            {
                Vector3 snappedPosition = Vector3Int.RoundToInt(point.Value + grabOffset);
                snappedPosition.y = currentlyHeld.GroundY;// + 0.1f;

                if (!BoxCast(snappedPosition, currentlyHeld.transform.rotation, currentlyHeld.transform.localScale))
                {
                    currentlyHeld.SnapTo(snappedPosition);
                    currentlyHeld.Release();
                    currentlyHeld = null;
                }
            }
        }
    }

    void OnRightMouseClick()
    {
        if (currentlyHeld != null)
        {
            currentlyHeld.Rotate();
        }
        else
        {
            Manipulatable manipulatable = RaycastForManipulatable();
            if (manipulatable == null)
                return;

            manipulatable.Rotate();
        }
    }

    void OnMiddleMouseClick()
    {

    }

    void MoveCurrentlyHeld()
    {
        if (currentlyHeld == null)
            return;

        Vector3? point = RaycastForGrid(false);

        if (point != null)
            currentlyHeld.MoveTo(point.Value + grabOffset);


        Vector3? shadowPoint = RaycastForGrid(true);

        if (point != null)
        {
            Vector3 snappedPosition = Vector3Int.RoundToInt(shadowPoint.Value + grabOffset);
            snappedPosition.y = currentlyHeld.GroundY;
            currentlyHeld.MoveShadowTo(snappedPosition);
        }
    }



    Manipulatable RaycastForManipulatable()
    {
        Ray ray = Camera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, float.PositiveInfinity, ManipulatableLayerMask))
        {
            if (hit.transform.gameObject.TryGetComponent(out Manipulatable manipulatable))
                return manipulatable;
        }

        return null;
    }

    Vector3? RaycastForGrid(bool hasToBeActualGrid)
    {
        Ray ray = Camera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, float.PositiveInfinity, GridLayerMask))
        {
            if (hasToBeActualGrid)
            {
                if (hit.transform.gameObject.TryGetComponent(out GameGrid gameGrid))
                    return hit.point;
            }
            else
            {
                return hit.point;
            }
        }

        return null;
    }

    /// <summary>
    /// Return true if it finds a collision
    /// </summary>
    bool BoxCast(Vector3 position, Quaternion rotation, Vector3 scale)
    {
        Collider[] x = Physics.OverlapBox(position, scale / 2f, rotation, BoxCastMask);

        foreach (var item in x)
        {
            if (!item.transform.IsChildOf(currentlyHeld.transform))
                return true;
        }

        return false;
        return x.Length != 0;
    }
}
