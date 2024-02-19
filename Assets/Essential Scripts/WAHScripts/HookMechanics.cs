/*-----------------------------------------------------

PEILABS LLC.
info@peilabs.com

Creation Date: 01.01.2023
Author: Halil Doğan
Description: This script handles all the Hook Mechanics in Work at Height VR simulation.
Please do not tweak or change unless you are authorized!

-----------------------------------------------------*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Autohand;
using Obi;
using Unity.Mathematics;

public class HookMechanics : MonoBehaviour
{
    public HookWarningScript HookWarningScript;

    private Transform LimitStart;
    private Transform LimitEnd;

    [Header("Objects")]
    public GameObject XRPlayer;// değişecek
    public Transform HolsterTrigger;
    public GameObject Hook;
    public Rigidbody rbOfHook;
    public GameObject XRCamera;
    public GameObject BeltCylinder;
    public Transform HooksParent;
    public Transform BeltTargetPosition;
    public float BeltTurnSpeed;

    public GameObject FallSupport;

    /*--- HOLSTER VARIABLES ---*/
    [Header("Variables")]
    public float HolsterSpeed;
    private Vector3 HolsterPos;
    public Vector3 LimitStartPos;
    private bool once=true;
    private bool isInHand;

    private bool isHolstered;

    private bool canBeHolstered;

    private bool hookGoToHolster;
    private bool isManliftHooked;
    private bool isGrabbingLadder;

    private bool isClimbingWithHook;


    /*---HOOK VARIABLES---*/
    public bool isHooked;

    private bool canBeHooked;
    public float HookSpeed;
    public float RopeLength=1.75f;
    private float holsterOffsetLenght = 1.2f;
    public float holsterStartPose_y;
    public Transform SideRail;

    private Vector3 toCameraVector;
    private Vector3 LetstryVector;
    private Vector3 displacement;
    Quaternion desiredHookRotation;
    Vector3 desiredHookPosition;


    public Grabbable hookGrabbable;

    public float HookPullSpeed;
    //test variables
    private GameObject collidedLine;

    // Start is called before the first frame update
    void Start()
    {
        //test
        holsterStartPose_y = HolsterTrigger.transform.localPosition.y;
        Physics.IgnoreLayerCollision(8, 9, true);
        Physics.IgnoreLayerCollision(8, 28, true);

        //test
        rbOfHook = GetComponent<Rigidbody>();
        rbOfHook.isKinematic = true;
        once=true;

        hookGoToHolster = true;
        isHolstered = true;
        isInHand = false;
        canBeHolstered = false;
        canBeHooked = false;
        isHooked = false;
        isManliftHooked = false;

        isGrabbingLadder = false;
        isClimbingWithHook = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        HolsterRepositioner();
        HookWarning();
        BeltRotate();
        PullForceHookToPlayer();
        ReturnHook();
        HookFollowsPlayer();


    }
    public void HookFollowsPlayer()
    {
        if (isHooked == true)
        {
            toCameraVector = new Vector3(collidedLine.transform.InverseTransformPoint(XRCamera.transform.position).x - collidedLine.transform.InverseTransformPoint(collidedLine.transform.position).x, collidedLine.transform.InverseTransformPoint(XRCamera.transform.position).y - collidedLine.transform.InverseTransformPoint(collidedLine.transform.position).y, collidedLine.transform.InverseTransformPoint(XRCamera.transform.position).z - collidedLine.transform.InverseTransformPoint(collidedLine.transform.position).z);


            if ((LimitStart.localPosition.z > collidedLine.transform.localPosition.z) && (LimitEnd.localPosition.z < collidedLine.transform.localPosition.z))
            {
                displacement = new Vector3(toCameraVector.x, 0, 0);
                collidedLine.transform.Translate(displacement * 20, Space.Self);
            }
            else
            {
                if (Mathf.Abs(collidedLine.transform.localPosition.z - LimitStart.localPosition.z) > Mathf.Abs(collidedLine.transform.localPosition.z - LimitEnd.localPosition.z))
                {
                    collidedLine.transform.localPosition = new Vector3(LimitEnd.transform.localPosition.x, LimitEnd.transform.localPosition.y, LimitEnd.transform.localPosition.z);

                    if (toCameraVector.x < 0)
                    {
                        displacement = new Vector3(toCameraVector.x, 0, 0);
                        collidedLine.transform.Translate(displacement * 20, Space.Self);
                    }
                }
                else
                {
                    collidedLine.transform.localPosition = new Vector3(LimitStart.transform.localPosition.x, LimitStart.transform.localPosition.y, LimitStart.transform.localPosition.z);
                    if (toCameraVector.x > 0)
                    {
                        displacement = new Vector3(toCameraVector.x, 0, 0);
                        collidedLine.transform.Translate(displacement * 20, Space.Self);
                    }
                }

            }

            transform.position = collidedLine.transform.position;
            transform.Translate(Vector3.down * 0.15f, Space.Self);

            if (collidedLine.transform.parent.rotation.eulerAngles.x == 90)
            {
                LimitStart.transform.position = collidedLine.transform.position;

            }
        }
    }
    public void OnDrawGizmos()
    {
        if (collidedLine != null)
        {
            if (collidedLine.tag == "HookLine")
            {
                //Debug.Log("bisekil girdi:");
                //Gizmos.color = Color.green;
                //Gizmos.DrawLine(collidedLine.transform.position, collidedLine.transform.position + toCameraVector);
            }

        }
    }
    public void ReturnHook()
    {
        HolsterPos = HolsterTrigger.transform.position;                     //Keeps Track of the Holster's Location in Vector3 

        if (hookGoToHolster == true)
        {
            rbOfHook.isKinematic = true;                                    //Make Hook Kinematic while it goes to Holster (not necesarry since we did it once in HolsterHook method but nvm)
            Hook.transform.position = Vector3.Lerp(transform.position, HolsterPos, HolsterSpeed * Time.deltaTime);
            Hook.transform.SetParent(HooksParent);
        }
    }
    public void HolsterRepositioner()
    {
        float angle = XRCamera.transform.localEulerAngles.x%360;
        float offsetLength;
        Vector3 newPoseHeight;
        //Debug.Log(angle);
        if (angle<=180)
        {
            offsetLength = Mathf.Lerp(-holsterOffsetLenght, holsterOffsetLenght, (angle + 90f) / 180f);
        }
        else
        {
            offsetLength = Mathf.Lerp(-holsterOffsetLenght,holsterOffsetLenght, Mathf.Abs(angle - 270) / 180f);
        }
        
        //Debug.Log("Collider pose: " + offsetLength);
        newPoseHeight = new Vector3(HolsterTrigger.transform.parent.localPosition.x, holsterStartPose_y + offsetLength, HolsterTrigger.transform.parent.localPosition.z);
        // -0.3f ile 0.3f arasında bir değer üretmek için açıyı yeniden haritala
        HolsterTrigger.transform.parent.localPosition = Vector3.Lerp(HolsterTrigger.transform.parent.localPosition, newPoseHeight, HolsterSpeed * Time.deltaTime);
    }
    public void PullForceHookToPlayer()
    {
        float DistanceBetweenHookAndPlayer = Vector3.Distance(XRCamera.transform.position, Hook.transform.position);
        if (isHooked == true && DistanceBetweenHookAndPlayer > RopeLength)
        {
            Vector3 HookForceDirection = (XRCamera.transform.position - Hook.transform.position).normalized;    //Create a vector from Player to Hook for the pulling force towards hook
            HookForceDirection = new Vector3(HookForceDirection.x, 0, HookForceDirection.z);                    //Force Direction for pulling Player towards hook
            XRPlayer.transform.Translate(-HookForceDirection * HookPullSpeed * Time.deltaTime, Space.World);

            //Debug.Log(HookForceDirection + "---" + DistanceBetweenHookAndPlayer);
            //Debug.DrawLine(Hook.transform.position, Hook.transform.position + HookForceDirection * 10, Color.red, Mathf.Infinity);    //Visualization of Vectors of Forces
            //Debug.DrawLine(HookForceDirection * 1, HookForceDirection * 2, Color.green, Mathf.Infinity);
        }
    }
    public void BeltRotate()
    {
        Quaternion desiredBeltRotation = Quaternion.Euler(BeltCylinder.transform.rotation.eulerAngles.x, XRCamera.transform.eulerAngles.y, BeltCylinder.transform.rotation.eulerAngles.z);
        BeltCylinder.transform.rotation = Quaternion.Lerp(BeltCylinder.transform.rotation, desiredBeltRotation, Time.deltaTime * BeltTurnSpeed);

    }
    public void HookWarning()
    {
        if (HookWarningScript.isClimbing == true && isGrabbingLadder == true && isClimbingWithHook == true)
        {

            FallSupport.transform.position = new Vector3(FallSupport.transform.position.x, XRCamera.transform.position.y - 1.5f, FallSupport.transform.position.z);
            //Debug.Log("FallSupport Position : " + FallSupport.transform.position.y + "XR Camera Position = " + XRCamera.transform.position.y);
            //Debug.Log("TAKİP ETMELİ");
        }
    }
    public bool IsGrabbed()
    {
        return hookGrabbable.BeingGrabbed();// burası denemelik
    }

    public void GrabHook()
    {



        //Physics.IgnoreLayerCollision(8, 28, false);

        isClimbingWithHook = false;
        Hook.transform.SetParent(null);
        //hookGoToHolster = false;

        if (isHooked == false && isHolstered == true && isInHand == false && canBeHolstered == false && canBeHooked == false)
        {               //WHEN WE PICKED THE HOOK FROM HOLSTER

            rbOfHook.isKinematic = false;

            isManliftHooked = false;
            isHolstered = false;
            isInHand = true;
            hookGoToHolster = false;
            canBeHolstered = true;
            canBeHooked = true;
            isHooked = false;

            //Debug.Log("Holster ---> Hand");

        }
        else if (isHooked == true && canBeHooked == false && isInHand == false && isHolstered == false && canBeHooked == false)
        {              //WHEN WE PICKED THE HOOK FROM RAIL


            //Debug.Log("Grabbed hook from rail");


            Invoke("EnableAppCollider", 0.4f);
            rbOfHook.isKinematic = false;

            isManliftHooked = false;
            isHolstered = false;
            isInHand = true;
            hookGoToHolster = false;
            canBeHolstered = true;
            canBeHooked = true;
            isHooked = false;

            //Debug.Log("Rail ---> Hand");

            if (collidedLine.transform.parent.rotation.eulerAngles.x == 90)
            {
                Debug.Log("gittii grabdeki railden hand ");
                LimitStart.transform.position=LimitStartPos;
                collidedLine.transform.position=LimitStartPos;
            }
        }
        else
        {

            //Debug.Log("HOOKED");
        }
        //Debug.Log("Equip Hook From Holster Worked");
    }
    public void EnableAppCollider()
    {
        collidedLine.GetComponent<Collider>().enabled = true;
    }

    public void ReleaseHook()
    {

        if (isHolstered == false && canBeHolstered == true && isHooked == false && isInHand == true && canBeHooked == true)        //Holster On Release
        {
            //Physics.IgnoreLayerCollision(8, 28, true);
            rbOfHook.isKinematic = true;                                                    //Make Hook Kinematic while it goes to Holster

            hookGoToHolster = true;
            isHolstered = true;
            canBeHolstered = false;
            isInHand = false;
            canBeHooked = false;

            hookGrabbable.HandsRelease();
            
         
            
            //Debug.Log("Holstered");

        }
        else
        {
            //Debug.Log("Released Because Hooked");
        }
    }
    public void HookToLine2(Vector3 contactPoint, GameObject collidedLine)
    {

        if (canBeHooked && isHooked == false)
        {

            //colider kapama
            collidedLine.GetComponent<Collider>().enabled = false;


            canBeHooked = false;
            canBeHolstered = false;
            isHolstered = false;
            isInHand = false;
            hookGoToHolster = false;
            isHooked = true;
            hookGrabbable.HandsRelease();
            Hook.transform.SetParent(null);//
            rbOfHook.isKinematic = true;


            desiredHookPosition = collidedLine.transform.position;


            desiredHookRotation = Quaternion.Euler(collidedLine.transform.parent.rotation.eulerAngles.x - 90f, collidedLine.transform.parent.rotation.eulerAngles.y + 270f, collidedLine.transform.parent.rotation.eulerAngles.z);


            rbOfHook.isKinematic = true;
            Hook.transform.position = desiredHookPosition;
            Hook.transform.rotation = desiredHookRotation;
            //şunu düzelt
            if (collidedLine.transform.parent.rotation.eulerAngles.x == 90)
            {
                Hook.transform.Rotate(0, 65, -90, Space.Self);

            }
            else
            {
                Hook.transform.Rotate(0, (((collidedLine.transform.parent.rotation.eulerAngles.x / 15) * 10) + 5), (collidedLine.transform.parent.rotation.eulerAngles.x == 0 ? 0 : (-(5 * (((collidedLine.transform.parent.rotation.eulerAngles.x / 15) * ((collidedLine.transform.parent.rotation.eulerAngles.x / 15) + 1)) / 2)))), Space.Self);
            }
        }
    }
    public void LadderGrabbed()
    {
        isGrabbingLadder = true;
    }
    public void LadderReleased()
    {
        isGrabbingLadder = false;
    }
    private void OnTriggerStay(Collider TriggerForHolster)
    {

        if (TriggerForHolster.tag == "Holster" && isHolstered == false && isInHand == true)
        {
            canBeHolstered = true;
            canBeHooked = true;

        }
        if (TriggerForHolster.tag == "Holster" && isHolstered == true && isInHand == false)
        {

            canBeHolstered = false;
            canBeHooked = false;

        }
        if (TriggerForHolster.tag == "Holster" && isHolstered == true && isInHand == true)
        {

            canBeHolstered = true;
            canBeHooked = false;

        }
        if (TriggerForHolster.tag == "Holster" && isHolstered == false && isInHand == false)
        {

            canBeHolstered = true;
            canBeHooked = false;

        }

    }


    private void OnCollisionEnter(Collision CollisionForHook)
    {

        if (CollisionForHook.gameObject.tag == "HookLine" && isHooked == false && canBeHooked == true && isManliftHooked == false)
        {

            foreach (ContactPoint contact in CollisionForHook.contacts)
            {
                //Debug.Log("Collision Enter Triggered");

                Vector3 contactPoint = contact.point;
                collidedLine = CollisionForHook.gameObject;
                //Debug.Log(collidedLine.gameObject.name);
                LimitStart = collidedLine.transform.parent.Find("LimitStart");
                if(once && collidedLine.transform.parent.rotation.eulerAngles.x == 90 )
                {
                    once=false;
                    LimitStartPos=LimitStart.position;
                    
                }
                
                LimitEnd = collidedLine.transform.parent.Find("LimitEnd");
                HookToLine2(contactPoint, collidedLine);
            }
        }
    }
    private void OnCollisionExit(Collision CollisionForHook)
    {


    }

    private void OnTriggerExit(Collider CollisionForHook)
    {
        if (CollisionForHook.gameObject.tag == "Apparatus<Exit")
        {
        }

        if (CollisionForHook.gameObject.tag == "HookLine")
        {
            //Debug.Log("Trigger Active Again");

        }
        if (CollisionForHook.gameObject.tag == "HookLineExitTrigger")
        {

        }
    }
}
