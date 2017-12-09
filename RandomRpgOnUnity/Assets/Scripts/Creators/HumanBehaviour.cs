using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanBehaviour : MonoBehaviour
{

    bool foundhuman = false;
    int itertionNumber = 0;
    bool hasToPause = false;
    Vector3 direction;

    // Use this for initialization
    void Start()
    {
        direction = GetNewDirection();
    }

    HumanBehaviour GetHumanTarget() {

        float minDistanceSoFar = float.MaxValue;
        HumanBehaviour chosenTarget = null;
        foreach (var obj in GameObject.FindObjectsOfType(this.GetType()))
        {

            HumanBehaviour currentlyChecked = (HumanBehaviour)obj;
            if (Vector3.Distance(transform.position, currentlyChecked.transform.position) < minDistanceSoFar)
            {

                Vector3.Distance(transform.position, currentlyChecked.transform.position);
                chosenTarget = currentlyChecked;
            }
           
        }
        return chosenTarget;
    }

    // Update is called once per frame
    void Update()
    {
        itertionNumber++;
        foundhuman = false;
        
        HumanBehaviour target = GetHumanTarget();
        

        if (target != null) {

        }
       

        if (target == null)
        {
            if (itertionNumber > 100)
            {
                direction = GetNewDirection();
                itertionNumber = 0;
            }

            transform.Translate(direction * Time.deltaTime);
        }
        else
        {
            Vector3 targetPosition = target.transform.position;
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, 0.02f);
        }
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Collider2D collider  = collision.collider;
        Debug.Log("Managed to collide!");
        if (collider != null && collider.gameObject != null) {
            Object.Destroy(collider.gameObject);
        }
    }


    private Vector3 GetNewDirection()
    {
        int d = Random.Range(1, 4);
        switch (d)
        {
            case 1: return Vector3.up; break;
            case 2: return Vector3.down; break;
            case 3: return Vector3.left; break;
            case 4: return Vector3.right; break;
            default: throw new System.Exception("Impossible");
        }
    }
}
