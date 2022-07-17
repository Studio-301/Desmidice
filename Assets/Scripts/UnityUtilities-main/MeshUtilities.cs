using System.Collections;
using UnityEngine;

namespace Tools
{
    public class MeshUtilities
    {
        //May contain bugs
        public static (float width, float height, float depth) CalulateRotatedDimentions(MeshRenderer meshRenderer, Transform root = null)
        {
            Vector3[] points = new Vector3[8];

            var transform = root == null ? meshRenderer.transform : root;

            // Store original rotation
            Quaternion originalRotation = transform.rotation;

            // Reset rotation
            transform.rotation = Quaternion.identity;

            // Get object bounds from unrotated object
            Bounds bounds = meshRenderer.bounds;

            // Get the unrotated points
            var sourcePoints = new[] 
            {
                new Vector3(bounds.min.x, bounds.min.y, bounds.min.z) - transform.position, // Bot left near
                new Vector3(bounds.max.x, bounds.min.y, bounds.min.z) - transform.position, // Bot right near
                new Vector3(bounds.min.x, bounds.max.y, bounds.min.z) - transform.position, // Top left near
                new Vector3(bounds.max.x, bounds.max.y, bounds.min.z) - transform.position, // Top right near
                new Vector3(bounds.min.x, bounds.min.y, bounds.max.z) - transform.position, // Bot left far
                new Vector3(bounds.max.x, bounds.min.y, bounds.max.z) - transform.position, // Bot right far
                new Vector3(bounds.min.x, bounds.max.y, bounds.max.z) - transform.position, // Top left far
                new Vector3(bounds.max.x, bounds.max.y, bounds.max.z) - transform.position, // Top right far
            };
            // Restore rotation
            transform.rotation = originalRotation;

            // Apply scaling
            for (int s = 0; s < sourcePoints.Length; s++)
            {
                sourcePoints[s] = new Vector3(sourcePoints[s].x / transform.lossyScale.x,
                                              sourcePoints[s].y / transform.lossyScale.y,
                                              sourcePoints[s].z / transform.lossyScale.z);
            }

            // Transform points from local to world space
            for (int t = 0; t < points.Length; t++)
            {
                points[t] = transform.TransformPoint(sourcePoints[t]);
            }
            return (width:  Mathf.Abs((points[1] - points[0]).magnitude),
                    height: Mathf.Abs((points[7] - points[5]).magnitude),
                    depth:  Mathf.Abs((points[7] - points[3]).magnitude));

            //y: Debug.DrawLine(points[5], points[7], c4, 0, false); // Bot right far to top right far
            //z: Debug.DrawLine(points[3], points[7], c5, 0, false); // Top right near to top right far
            //x: Debug.DrawLine(points[0], points[1], c1, 0, false); // Bot left near to bot right near
        }
    }
}