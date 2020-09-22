using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;


public class PlayerMovement : MonoBehaviour
{
    Vector3 newPosition;
    private ThirdPersonCharacter PlayerMoveScript;

    
    void Start()
    {
        newPosition = transform.position;
        PlayerMoveScript = GetComponent<ThirdPersonCharacter>();
    }

    void FixedUpdate()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
            {
                newPosition = hit.point;
                //transform.position = newPosition;
            }
        }

        PlayerMoveScript.Move(newPosition - transform.position, false, false);
    }
}


