using UnityEngine;

namespace Sourav.Tools.WireFrame.Wireframe.Demo.Scripts
{
    public class Rotate : MonoBehaviour
    {
        public float speed = -5;
        private void Update()
        {
            transform.rotation *= Quaternion.AngleAxis(speed * Mathf.PI * Time.deltaTime, Vector3.up);
        }
    }

}
