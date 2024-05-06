using UnityEngine;

namespace GenshintImpact2
{
    public class EnemyHealthBar : MonoBehaviour
    {
        // Update is called once per frame
        void Update()
        {
            transform.rotation = Quaternion.LookRotation(transform.position - Camera.main.transform.position);
        }
    }
}
