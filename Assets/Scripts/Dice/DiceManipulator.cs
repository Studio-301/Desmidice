using UnityEngine;

public class DiceManipulator : MonoBehaviour
{
    [Header("References")]
    public Camera Camera;

    [Header("Raycast Settings")]
    public LayerMask ManipulatableLayerMask;
    public LayerMask GridLayerMask;

    //Internals
    [SerializeField] Manipulatable currentlyHeld;

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
                    currentlyHeld.SetColliderState(false);
                }
            }
        }
        else
        {
            //Try to release

            Vector3? point = RaycastForGrid();

            if (point != null)
            {
                Vector3 snappedPosition = Vector3Int.RoundToInt(point.Value);
                currentlyHeld.SnapTo(snappedPosition);
                currentlyHeld.SetColliderState(true);
                currentlyHeld = null;
            }
        }
    }

    void OnRightMouseClick()
    {
        Manipulatable manipulatable = RaycastForManipulatable();
        if (manipulatable == null)
            return;

        manipulatable.Rotate();
    }

    void OnMiddleMouseClick()
    {

    }

    void MoveCurrentlyHeld()
    {
        if (currentlyHeld == null)
            return;

        Vector3? point = RaycastForGrid();

        if (point != null)
            currentlyHeld.MoveTo(point.Value);
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

    Vector3? RaycastForGrid()
    {
        Ray ray = Camera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, float.PositiveInfinity, GridLayerMask))
        {
            if (hit.transform.gameObject.TryGetComponent(out GameGrid gameGrid))
                return hit.point;
        }

        return null;
    }
}
