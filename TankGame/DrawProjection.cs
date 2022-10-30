using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawProjection : MonoBehaviour
{
    [SerializeField]private PlayerInputs inputs;
    [SerializeField]private PlayerShoot shootInfo;
    [SerializeField]private LineRenderer lineRenderer;

    public int numPoints = 50;
    public float timeBtwPoints = 0.1f;
    public LayerMask collidableLayers;
    // Start is called before the first frame update
    void Start()
    {        
    }

    // Update is called once per frame
    void Update()
    {
        DrawShootPreview();
    }

    private void DrawShootPreview()
    {
        
        lineRenderer.startColor = Color.red;
        lineRenderer.endColor = Color.red;
        if(shootInfo.Ammo <= 0 )
        {
            lineRenderer.startColor = Color.grey;
            lineRenderer.endColor = Color.grey;
        }
        if(!inputs.isShooting){
            lineRenderer.positionCount = 0;
            return;}
        lineRenderer.positionCount = numPoints;
        List<Vector3> points = new List<Vector3>();
        Vector3 startPosition = shootInfo.shotPoint.position;
        Vector3 startingVelocity = shootInfo.shotPoint.up * shootInfo.Power;

        for (float t = 0; t < numPoints; t += timeBtwPoints)
        {
            Vector3 newPoint = startPosition + t * startingVelocity;
            newPoint.y = startPosition.y + startingVelocity.y * t + Physics.gravity.y / 2f * t * t;
            points.Add(newPoint);
            if (Physics.OverlapSphere(newPoint, 2, collidableLayers).Length > 0)
            {
                lineRenderer.positionCount = points.Count;
                break;
            }
        }
        lineRenderer.SetPositions(points.ToArray());
    }
}
