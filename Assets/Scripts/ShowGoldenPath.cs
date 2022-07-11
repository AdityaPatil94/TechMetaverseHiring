// ShowGoldenPath
using UnityEngine;
using UnityEngine.AI;

public class ShowGoldenPath : MonoBehaviour
{
    public Transform target;
    private NavMeshPath path;
    private float elapsed = 0.0f;

    public LineRenderer pathIndicator;
    public Color c1 = Color.yellow;
    public Color c2 = Color.red;
    public int lengthOfLineRenderer = 20;
    void Start()
    {
        path = new NavMeshPath();
        elapsed = 0.0f;
        pathIndicator.material = new Material(Shader.Find("Sprites/Default"));
        pathIndicator.widthMultiplier = 0.2f;
        

        // A simple 2 color gradient with a fixed alpha of 1.0f.
        float alpha = 1.0f;
        Gradient gradient = new Gradient();
        gradient.SetKeys(
            new GradientColorKey[] { new GradientColorKey(c1, 0.0f), new GradientColorKey(c2, 1.0f) },
            new GradientAlphaKey[] { new GradientAlphaKey(alpha, 0.0f), new GradientAlphaKey(alpha, 1.0f) }
        );
        pathIndicator.colorGradient = gradient;
    }

    void Update()
    {
        // Update the way to the goal every second.
        elapsed += Time.deltaTime;
        if (elapsed > .20f)
        {
            elapsed -= .20f;
            NavMesh.CalculatePath(transform.position, target.position, NavMesh.AllAreas, path);
        }
        for (int i = 0; i < path.corners.Length - 1; i++)
            Debug.DrawLine(path.corners[i], path.corners[i + 1], Color.red);
        CreatePath();
    }


    public void CreatePath()
    {
        pathIndicator.positionCount = path.corners.Length;
        var points = new Vector3[path.corners.Length];
        for (int i = 0; i < path.corners.Length ; i++)
        {
            points[i] = path.corners[i];
        }
        pathIndicator.SetPositions(points);
       
        
        //for (int i = 0; i < path.corners.Length - 1; i++)
        //{
        //    //Debug.DrawLine(path.corners[i], path.corners[i + 1], Color.red);
        //    pathIndicator.SetPosition(i,path.corners[i]);
        //}
            
    }

}