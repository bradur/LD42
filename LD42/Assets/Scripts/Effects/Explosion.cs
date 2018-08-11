// Date   : 11.08.2018 11:41
// Project: LD42
// Author : bradur

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Explosion : MonoBehaviour {

    [SerializeField]
    private float lifeTime = 0.2f;
    [SerializeField]
    private bool alive = false;

    private float viewRadius;
    public float ViewRadius { get { return viewRadius; } }

    [SerializeField]
    [Range(0, 360)]
    private float viewAngle;
    public float ViewAngle { get { return viewAngle; } }

    [SerializeField]
    private LayerMask targetMask;

    [SerializeField]
    private LayerMask obstacleMask;

    [SerializeField]
    private float meshResolution = 25f;

    [SerializeField]
    private MeshFilter viewMeshFilter;
    private Mesh viewMesh;

    [SerializeField]
    private int edgeResolveIterations = 5;

    [SerializeField]
    private float edgeDistanceTreshold = 1f;

    private List<Transform> visibleTargets = new List<Transform>();
    public List<Transform> VisibleTargets { get { return visibleTargets; } }

    MapGrid mapGrid;

    public void Initialize(int x, int y, float radius, MapGrid mapGrid)
    {
        viewRadius = radius;
        transform.localPosition = new Vector3(x, y, 0);
        alive = true;
        this.mapGrid = mapGrid;
        StartCoroutine("FindTargetsWithDelay", 0.2f);
        viewMesh = new Mesh
        {
            name = "View Mesh"
        };
        viewMeshFilter.mesh = viewMesh;
    }

    void Start () {
    }

    void Kill()
    {
        Destroy(gameObject);
    }

    void Update () {
        if (alive)
        {
            lifeTime -= Time.deltaTime;
            if (lifeTime <= 0f)
            {
                Kill();
            }
        }
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (alive)
        {
            DrawFieldOfView();
        }
    }

    void DrawFieldOfView()
    {
        int stepCount = Mathf.RoundToInt(viewAngle * meshResolution);
        float stepAngleSize = viewAngle / stepCount;
        List<Vector3> viewPoints = new List<Vector3>();
        ViewCastInfo oldViewCast = new ViewCastInfo();

        for (int i = 0; i <= stepCount; i += 1)
        {
            float angle = transform.eulerAngles.y - viewAngle / 2 + stepAngleSize * i;
            //Debug.DrawLine(transform.position, transform.position + DirFromAngle(angle, true) * viewRadius, Color.cyan);
            ViewCastInfo newViewCast = ViewCast(angle);

            if (i > 0)
            {
                bool edgeDistanceTresholdExceeded = Mathf.Abs(oldViewCast.Distance - newViewCast.Distance) > edgeDistanceTreshold;
                if (oldViewCast.Hit != newViewCast.Hit || (oldViewCast.Hit && newViewCast.Hit && edgeDistanceTresholdExceeded))
                {
                    EdgeInfo edge = FindEdge(oldViewCast, newViewCast);
                    if (edge.PointA != Vector3.zero)
                    {
                        viewPoints.Add(edge.PointA);
                    }
                    if (edge.PointB != Vector3.zero)
                    {
                        viewPoints.Add(edge.PointB);
                    }
                }
            }

            viewPoints.Add(newViewCast.Point);
            oldViewCast = newViewCast;
        }

        int vertexCount = viewPoints.Count + 1;
        Vector3[] vertices = new Vector3[vertexCount];
        int[] triangles = new int[(vertexCount - 2) * 3];

        List<Vector3> pointsForLineRenderer = new List<Vector3>
        {
            Vector3.zero
        };
        vertices[0] = Vector3.zero;
        for (int i = 0; i < vertexCount - 1; i += 1)
        {
            vertices[i + 1] = transform.InverseTransformPoint(viewPoints[i]);
            if (i < vertexCount - 2)
            {
                triangles[i * 3] = 0;
                triangles[i * 3 + 1] = i + 1;
                triangles[i * 3 + 2] = i + 2;
            }
        }
        viewMesh.Clear();
        viewMesh.vertices = vertices;
        viewMesh.triangles = triangles;
        viewMesh.RecalculateNormals();
    }

    EdgeInfo FindEdge(ViewCastInfo minViewCast, ViewCastInfo maxViewCast)
    {
        float minAngle = minViewCast.Angle;
        float maxAngle = maxViewCast.Angle;
        Vector3 minPoint = Vector3.zero;
        Vector3 maxPoint = Vector3.zero;
        for (int i = 0; i < edgeResolveIterations; i += 1)
        {
            float angle = (minAngle + maxAngle) / 2;
            ViewCastInfo newViewCast = ViewCast(angle);

            bool edgeDistanceTresholdExceeded = Mathf.Abs(minViewCast.Distance - newViewCast.Distance) > edgeDistanceTreshold;
            if (minViewCast.Hit != newViewCast.Hit || (minViewCast.Hit && newViewCast.Hit && edgeDistanceTresholdExceeded))
                if (newViewCast.Hit == minViewCast.Hit)
                {
                    minAngle = angle;
                    minPoint = newViewCast.Point;
                }
                else
                {
                    maxAngle = angle;
                    maxPoint = newViewCast.Point;
                }
        }

        return new EdgeInfo(minPoint, maxPoint);
    }

    private ViewCastInfo ViewCast(float globalAngle)
    {
        Vector3 direction = DirFromAngle(globalAngle, true);
        RaycastHit hit;

        if (Physics.Raycast(transform.position, direction, out hit, viewRadius, obstacleMask))
        {
            return new ViewCastInfo(true, hit.point, hit.distance, globalAngle);
        }
        else
        {
            return new ViewCastInfo(false, transform.position + direction * viewRadius, viewRadius, globalAngle);
        }
    }

    struct ViewCastInfo
    {
        private bool hit;
        public bool Hit { get { return hit; } }
        private Vector3 point;
        public Vector3 Point { get { return point; } }
        private float distance;
        public float Distance { get { return distance; } }
        private float angle;
        public float Angle { get { return angle; } }

        public ViewCastInfo(bool _hit, Vector3 _point, float _distance, float _angle)
        {
            hit = _hit;
            point = _point;
            distance = _distance;
            angle = _angle;
        }
    }

    struct EdgeInfo
    {
        private Vector3 pointA;
        public Vector3 PointA { get { return pointA; } }
        private Vector3 pointB;
        public Vector3 PointB { get { return pointB; } }

        public EdgeInfo(Vector3 _pointA, Vector3 _pointB)
        {
            pointA = _pointA;
            pointB = _pointB;
        }
    }

    IEnumerator FindTargetsWithDelay(float delay)
    {
        while (true)
        {
            yield return new WaitForSeconds(delay);
            FindVisibleTargets();
        }
    }

    void FindVisibleTargets()
    {
        visibleTargets.Clear();
        Collider[] targetsInViewRadius = Physics.OverlapSphere(transform.position, viewRadius, targetMask);

        for (int i = 0; i < targetsInViewRadius.Length; i += 1)
        {
            Transform target = targetsInViewRadius[i].transform;
            Vector3 directionToTarget = (target.position - transform.position).normalized;
            if (Vector3.Angle(transform.forward, directionToTarget) < viewAngle / 2)
            {
                float distanceToTarget = Vector3.Distance(transform.position, target.position);
                if (!Physics.Raycast(transform.position, directionToTarget, distanceToTarget, obstacleMask))
                {
                    visibleTargets.Add(target);
                }
            }
        }
        mapGrid.ProcessExplosionTargets(visibleTargets);
    }

    public Vector3 DirFromAngle(float angleInDegrees, bool angleIsGlobal)
    {
        if (!angleIsGlobal)
        {
            angleInDegrees += transform.eulerAngles.y;
        }
        return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), 0, Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
    }

}
