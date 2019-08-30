using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViewCone : MonoBehaviour
{
    //private Vector3 _noAngle;
    private BuildMesh _meshScript;
    private Vector3 forwardDistance;

    private Sentry _guardScript;

    public float distance = 10;
    public float radius = 8;
    //public float directionMultiplier;

    public List<Vector3> _directionList = new List<Vector3>();
    public Vector3[] vertArray;

    // Start is called before the first frame update
    //void Start()
    //{
    //    _meshScript = GetComponent<BuildMesh>();
    //    //_noAngle = transform.forward * 10.0f;
    //    CreateDirectionList();
    //}

    IEnumerator Start()
    {
        _guardScript = GetComponentInParent<Sentry>();
        _meshScript = GetComponent<BuildMesh>();
        //_noAngle = transform.forward * 10.0f;
        yield return new WaitForEndOfFrame();
        CreateDirectionList();
    }

    // Update is called once per frame
    void Update()
    {
        //SetRaycast();
        ViewConeCheck();
        vertArray = _meshScript.vertices;
    }
   

    private void CreateDirectionList()
    {
        Vector3 tempVec = new Vector3();

        // middle cast
        //tempVec = new Vector3(0, 0, distance);
        tempVec = _meshScript.vertices[9];
        SetDirectionPoint(tempVec);
        // outer diamond
        //tempVec = new Vector3(5, 0, distance);
        tempVec = _meshScript.vertices[3];
        SetDirectionPoint(tempVec);
        //tempVec = new Vector3(0, 5, distance);
        tempVec = _meshScript.vertices[1];
        SetDirectionPoint(tempVec);
        //tempVec = new Vector3(-5, 0, distance);
        tempVec = _meshScript.vertices[7];
        SetDirectionPoint(tempVec);
        //tempVec = new Vector3(0, -5, distance);
        tempVec = _meshScript.vertices[5];
        SetDirectionPoint(tempVec);

        // outer box
        //tempVec = new Vector3(3.75f, -3.75f, distance);
        //SetDirectionPoint(tempVec);
        tempVec = _meshScript.vertices[2];
        SetDirectionPoint(tempVec);
        //tempVec = new Vector3(-3.75f, 3.75f, distance);
        //SetDirectionPoint(tempVec);
        tempVec = _meshScript.vertices[4];
        SetDirectionPoint(tempVec);
        //tempVec = new Vector3(3.75f, 3.75f, distance);
        //SetDirectionPoint(tempVec);
        tempVec = _meshScript.vertices[6];
        SetDirectionPoint(tempVec);
        //tempVec = new Vector3(-3.75f, -3.75f, distance);
        //SetDirectionPoint(tempVec);
        tempVec = _meshScript.vertices[8];
        SetDirectionPoint(tempVec);

        //// inner diamond
        //tempVec = new Vector3(2.5f, 0, distance);
        //SetDirectionPoint(tempVec);
        //tempVec = new Vector3(0, 2.5f, distance);
        //SetDirectionPoint(tempVec);
        //tempVec = new Vector3(-2.5f, 0, distance);
        //SetDirectionPoint(tempVec);
        //tempVec = new Vector3(0, -2.5f, distance);
        //SetDirectionPoint(tempVec);

        //// inner box
        //tempVec = new Vector3(2.5f, -2.5f, distance);
        //SetDirectionPoint(tempVec);
        //tempVec = new Vector3(-2.5f, 2.5f, distance);
        //SetDirectionPoint(tempVec);
        //tempVec = new Vector3(2.5f, 2.5f, distance);
        //SetDirectionPoint(tempVec);
        //tempVec = new Vector3(-2.5f, -2.5f, distance);
        //SetDirectionPoint(tempVec);

        //tempVec = new Vector3(2.5f, 0, distance);
        //SetDirectionPoint(tempVec);
        //tempVec = new Vector3(0, 2.5f, distance);
        //SetDirectionPoint(tempVec);
        //tempVec = new Vector3(-2.5f, 0, distance);
        //SetDirectionPoint(tempVec);
        //tempVec = new Vector3(0, -2.5f, distance);
        //SetDirectionPoint(tempVec);
        //tempVec = new Vector3(1.875f, -1.875f, distance);
        //SetDirectionPoint(tempVec);
        //tempVec = new Vector3(-1.875f, 1.875f, distance);
        //SetDirectionPoint(tempVec);
        //tempVec = new Vector3(1.875f, 1.875f, distance);
        //SetDirectionPoint(tempVec);
        //tempVec = new Vector3(-1.875f, -1.875f, distance);
        //SetDirectionPoint(tempVec);

        //tempVec = new Vector3(1.25f, 0, distance);
        //SetDirectionPoint(tempVec);
        //tempVec = new Vector3(0, 1.25f, distance);
        //SetDirectionPoint(tempVec);
        //tempVec = new Vector3(-1.25f, 0, distance);
        //SetDirectionPoint(tempVec);
        //tempVec = new Vector3(0, -1.25f, distance);
        //SetDirectionPoint(tempVec);
        //tempVec = new Vector3(0.9375f, -0.9375f, distance);
        //SetDirectionPoint(tempVec);
        //tempVec = new Vector3(-0.9375f, 0.9375f, distance);
        //SetDirectionPoint(tempVec);
        //tempVec = new Vector3(0.9375f, 0.9375f, distance);
        //SetDirectionPoint(tempVec);
        //tempVec = new Vector3(-0.9375f, -0.9375f, distance);
        //SetDirectionPoint(tempVec);

        //vertical
        //diagonal
    }

    void SetDirectionPoint(Vector3 direction)
    {
        direction = transform.TransformDirection(direction);
        _directionList.Add(direction);
    }

    void ViewConeCheck()
    {
        RaycastHit hit;
        for (int i = 0; i < _directionList.Count; i++)
        {
            Debug.DrawRay(transform.position, _directionList[i], Color.cyan);
            if (Physics.Raycast(transform.position, _directionList[i], out hit, _meshScript.distance))
                _guardScript.SendMessage("SeePlayer", hit);
        }
        _directionList.Clear();
        CreateDirectionList();
    }

    private void SetRaycast()
    {
        // The triangle is isosceles, so half the base and find the hypotenuse of one side, which will be the same for the other side
        float hypotenuse = Mathf.Sqrt(Mathf.Pow(distance, 2) + radius/2 * radius/2);

        // Find how far the raycast should be shot and what direction the rotations will be based from
        forwardDistance = transform.forward * hypotenuse;

        // Find the Tangent of this "half triangle"
        float TOA = (radius / 2) / distance;

        // Turn that into an angle
        float vertexAngle = Mathf.Atan(TOA);
        
        TOA = Mathf.Rad2Deg * Mathf.Atan(TOA);

        // Create a blank quaternion, set the W to vertexAngle because that's the rotation we need 
        Quaternion outerHorizontal = new Quaternion(0, 1, 0, vertexAngle);

        // Find the direction of the raycast by multiplying the rotation by the forward direction, that's where the angled line will be facing
        Vector3 outerHoriAngle = -(outerHorizontal * forwardDistance);//= new Vector3(0, TOA, 0);
        Debug.Log(outerHoriAngle);   
        //rightRaycastAngle *= forwardDistance

        //transform.RotateAround(transform.position, Vector3.up, TOA);
        Debug.DrawRay(transform.position, forwardDistance, Color.cyan);



        ///TESTING
        // X, Y, Z
        Vector3 foo = new Vector3(1, 1, 10);
        Vector3 Testforward = transform.TransformDirection(foo);
        Debug.DrawRay(transform.position, Testforward, Color.white);

        ///

        

        // Reverse it when it's used
        //Debug.DrawRay(transform.position, outerHoriAngle, Color.magenta);

        // This will be the rotation to the left as it's taking away that rotation
        outerHorizontal = new Quaternion(0, 1, 0, -vertexAngle);
        

        outerHoriAngle = -(outerHorizontal * forwardDistance);
        // Still reverse it
       // Debug.DrawRay(transform.position, outerHoriAngle, Color.magenta);

        Quaternion outerVertical = new Quaternion(0, 0, 1, vertexAngle);


        Vector3 outerVertAngle = outerVertical * forwardDistance;
        Vector3 TestAngle = -(outerVertical * forwardDistance);
        Debug.DrawRay(transform.position, TestAngle, Color.red);
        Quaternion outerVertical2 = new Quaternion(0, 0, 1, 90);

        Quaternion combinedQuats = new Quaternion();

        combinedQuats.x = outerVertical.x;
        //combinedQuats.y = outerVertical.y;
        //outerVertical.z = outerVertical2.z;

        outerVertAngle = -(outerVertical * forwardDistance);
        //Debug.DrawRay(transform.position, outerVertAngle, Color.green);

        outerVertical = new Quaternion(1, 0, 0, -vertexAngle);
        outerVertAngle = outerVertical * forwardDistance;
        //Debug.DrawRay(transform.position, outerVertAngle, Color.yellow);
    }
}
