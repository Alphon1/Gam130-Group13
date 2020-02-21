using UnityEngine;

public class Doorways : MonoBehaviour {

    void OnDrawGizmos()
    {
		Ray ray = new Ray(new Vector3(transform.position.x, transform.position.y, transform.position.z), transform.rotation * Vector3.forward);

        Gizmos.color = Color.red;
        Gizmos.DrawRay(ray);
    }

}
