using UnityEngine;

[RequireComponent(typeof(CameraRaycaster))]
public class CursorHandler : MonoBehaviour
{
    [SerializeField]
    Texture2D WalkCursor = null;
    [SerializeField]
    Texture2D EnemyCursor = null;
    [SerializeField]
    Texture2D EndWorld = null;
    [SerializeField]
    Vector2 CursorHotspot = new Vector2(0, 0);

    private CameraRaycaster cameraRaycaster;

    void Start()
    {
        cameraRaycaster = GetComponent<CameraRaycaster>();
        //print("Camera Ray Caster " + cameraRaycaster.gameObject);
        if (cameraRaycaster == null) print("Problem!!!!");
        cameraRaycaster.LayerChangeObservers += ChangeCursor;
    }

   public void ChangeCursor(CameraRaycaster.Layer newLayer)
    {
        switch (newLayer)
        {
            case CameraRaycaster.Layer.Walkable:
                Cursor.SetCursor(WalkCursor, CursorHotspot, CursorMode.Auto);
                Debug.Log("WalkCursor");
                break;
            case CameraRaycaster.Layer.EndWorld:
                Cursor.SetCursor(EndWorld, CursorHotspot, CursorMode.Auto);
                Debug.Log("EndWorld");
                break;
            case CameraRaycaster.Layer.Enemy:
                Cursor.SetCursor(EnemyCursor, CursorHotspot, CursorMode.Auto);
                Debug.Log("EnemyCursor");
                break;
            default:
                Debug.LogWarning("Unknown layer hit by mouse cursor!");
                return;
        }
    }
}