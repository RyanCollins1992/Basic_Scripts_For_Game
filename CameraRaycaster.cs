using UnityEngine;

public class CameraRaycaster : MonoBehaviour
{
    public enum Layer
    {
        Walkable = 8,
        Enemy = 9,
        //This is not an actual layer value. Instead, we can
        //set up a dummy RaycastHit with this in the event that our
        //cursor does not hit any of the other layers
        EndWorld = -1,
        //endworldCursor = 10
    }

    [SerializeField]
    float distanceToBackground = 100f;
    private Camera viewCamera;

    private RaycastHit hitInfo;
    public RaycastHit Hit
    {
        get { return hitInfo; }
    }

    private Layer layerVal;
    public Layer CurrentLayerHit
    {
        get { return layerVal; }
    }

    public Layer[] layerPriorities = {
         Layer.Enemy,
         Layer.Walkable
    };


    public delegate void OnLayerChange(Layer newLayer);
    public event OnLayerChange LayerChangeObservers;

    void Awake()
    {
        viewCamera = Camera.main;
    }

    void Update()
    {
        foreach (CameraRaycaster.Layer layer in layerPriorities)
        {
            var hit = RaycastForLayer(layer);


            if (hit.HasValue)
            {
                hitInfo = hit.Value;

                if (layerVal != layer)
                {
                    layerVal = layer;
                    if (LayerChangeObservers != null)
                    {
                        LayerChangeObservers(layer);
                    }
                }
                layerVal = layer;
                return;
            }
            hitInfo.distance = distanceToBackground;
            layerVal = Layer.EndWorld;
            LayerChangeObservers(CameraRaycaster.Layer.EndWorld);
            Debug.Log(layerVal);

        }
        

    }

    RaycastHit? RaycastForLayer(Layer layer)
    {
        int layerMask = 1 << (int)layer;
        Ray ray = viewCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        bool HasHit = Physics.Raycast(ray, out hit, distanceToBackground, layerMask);

        if (HasHit)
        {
            return hit;
        }

        return null;
    }
}