using UnityEngine;
using System.Collections;

public class CamaeraFollow : MonoBehaviour
{
    /// <summary>
    /// The camera's max x pos
    /// </summary>
    [SerializeField]
    private float xMax;

    /// <summary>
    /// The camera's min x pos
    /// </summary>
    [SerializeField]
    private float xMin;

    /// <summary>
    /// The camera's max y pos
    /// </summary>
    [SerializeField]
    private float yMax;

    /// <summary>
    /// The camera's min y pos
    /// </summary>
    [SerializeField]
    private float yMin;

    /// <summary>
    /// The target to follow
    /// </summary>
    private Transform target;

    // Use this for initialization
    void Start()
    {
        //Sets a reference to the player
        target = GameObject.Find("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        //Follows the player
        transform.position = new Vector3(Mathf.Clamp(target.position.x, xMin, xMax), Mathf.Clamp(target.position.y, yMin, yMax), -10);
    }
}
