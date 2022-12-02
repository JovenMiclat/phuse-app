using UnityEngine;
using UnityEngine.AI;

namespace Assets.Scripts.AlgorithmHandler
{
    public class ObjectController : MonoBehaviour
    {
        private APathfinder aPathfinder;
        public LineRenderer route;

        public NavMeshAgent obj;
        Vector3 direction;

        public Color publColor;

        void Start()
        {
            aPathfinder = APathfinder.Instance;

            Color rdmColor = new Color(
                Random.Range(0f, 1f),
                Random.Range(0f, 1f),
                Random.Range(0f, 1f)
            );

            route.startColor = rdmColor;
            route.endColor = rdmColor;
            publColor = route.endColor;

        }
        void Update()
        {
            DrawPath(obj.path);
        }

        void DrawPath(NavMeshPath path)
        {
            route.enabled = true;

            if ((Vector3.Distance(route.GetPosition(0), route.GetPosition(route.positionCount - 1))) <= aPathfinder.radius)
            {
                route.enabled = false;
            }

            if (path.corners.Length < 2)
                return;

            route.positionCount = path.corners.Length;
            route.SetPositions(path.corners);
            route.SetPosition(0, (path.corners[0]) + direction * aPathfinder.radius);
            for (var i = 1; i < path.corners.Length; i++)
            {
                route.SetPosition(i, path.corners[i]);
            }
        }
    }
}