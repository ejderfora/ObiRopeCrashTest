

using UnityEngine;
using System.Collections;

public class ManliftAnimation : MonoBehaviour // liftAnim and C# file name must be the same
{
   
    
    private Animator anim;
    public HookMechanics HookMechanicsRight;
    public HookMechanics HookMechanicsLeft;
    public HeightLevelManager HeightLevelManager;
    private int goUp;
    private int goDown;
    private int topped;
    private int bottomed;
    private float animFrame;

    public Vector3 StationPos;
    public Vector3 CatwalkPos;

    public bool MoveTowardsCatwalkBool;
    public bool ShouldMoveButtonWorkBool;
    private bool once = false;
    public float ManliftSpeed;
    private bool isMoving;
    private int step;
    bool sfxIsPlaying = false;

    public GameObject Manlift;

    [Header("Manlift Object Audio References")]
    public AudioClip ManliftMovementSFX;

    public AudioSource ManliftAudioSource;

    //[Header("Script References")]
    //public InteractionSFXManager SFXManager;
    void Start() // Sets up Starting conditions
    {
        ManliftAudioSource.clip = ManliftMovementSFX;
        
        anim = GetComponent<Animator>();
        goDown = 2;
        goUp = 0;
        topped = 0;
        bottomed = 1;
        step = 0;

        isMoving = false;
        MoveTowardsCatwalkBool = true;
        ShouldMoveButtonWorkBool = false;
    }
    private void Update()
    {
        if(HeightLevelManager.isGameComplete==true){
            if (step == 2) //!!!!!!!!!!!!!!!!!!!!!!!!!!!!KODDA BURASI KALDIRILACAK, OYUN BITISINDEKI AKSIYONA GORE STEP 2---->>>3 YAPILACAK
            {
                step = 3;
            }

        }
    }

    void FixedUpdate() // Runs once every frame
    {
        animFrame = anim.GetCurrentAnimatorStateInfo(0).normalizedTime;

        if (animFrame > 1.0f) // Check if we reched the top
        {
            topMax();
        }
        if (animFrame < 0.0f) // Check if we reached the bottom
        {
            bottomMax();
        }

        Movement();
    }


    public void Up() // Start raising the lift
    {
        if ((!MoveTowardsCatwalkBool && ShouldMoveButtonWorkBool && !isMoving && step == 1 && (HookMechanicsRight.isHooked||HookMechanicsLeft.isHooked)) || (step == 6))
        {
            anim.SetFloat("Direction", 1);
            anim.speed = 1.0f;
            anim.Play("move", -1, float.NegativeInfinity);
            goUp = 1;
            goDown = 2;
            if (step == 1)
            {
                step = 2;
                HeightLevelManager.canExit=true;
            }
            
        }
    }


    public void Down() // Start lowering the lift
    {
        if ((!MoveTowardsCatwalkBool && ShouldMoveButtonWorkBool && !isMoving && step == 3 && (HookMechanicsRight.isHooked||HookMechanicsLeft.isHooked)) || (step == 6))
        {
            anim.SetFloat("Direction", -1);
            anim.speed = 1.0f;
            anim.Play("move", -1, float.NegativeInfinity);
            goDown = 1;
            goUp = 2;
            if (step == 3)
            {
                step = 4;
            }
        }
    }

    public void Pause() // Stop the lift where it is
    {
        goUp = 0;
        goDown = 0;
        topped = 0;
        bottomed = 0;
        anim.speed = 0.0f;
    }

    public void topMax() // Stop if we reached the top
    {
        switch (topped)
        {
            case 0:
                goUp = 2;
                anim.speed = 0.0f;
                goDown = 0;
                topped = 1;
                bottomed = 0;
                break;

            default:
                topped = 1;
                break;
        }
    }

    public void bottomMax() // Stop if we reached the bottom
    {
        switch (bottomed)
        {
            case 0:
                goDown = 2;
                anim.speed = 0.0f;
                goUp = 0;
                bottomed = 1;
                topped = 0;
                break;

            default:
                bottomed = 1;
                break;
        }
    }

    public void checkIfUpOrDown()
    {
        //SFXManager.PlayPhysicalButtonSFX();
        
        switch (topped)
        {

            case 0:
                Up();
                break;

            case 1:
                Down();
                break;
        }
    }

    public void liftMovementController(){
        //SFXManager.PlayPhysicalButtonSFX();
        
		if((5.4 > Manlift.transform.position.z || Manlift.transform.position.z > 37.30) &&  (HookMechanicsRight.isHooked||HookMechanicsLeft.isHooked)){
			ShouldMoveButtonWorkBool = true;
            PlayManliftAudio(true);

        }else{
			Debug.Log("You can not use it while you move");
		}
		if(5.4 > Manlift.transform.position.z && (HookMechanicsRight.isHooked||HookMechanicsLeft.isHooked))
		{
            
			MoveTowardsCatwalkBool = true;
			isMoving = false;
			
		}
		if(37.30 < Manlift.transform.position.z && (HookMechanicsRight.isHooked||HookMechanicsLeft.isHooked))
		{
           
			MoveTowardsCatwalkBool = false;
			isMoving=false;
			
        }
	}
    public void CanLeft()
    {
        if ((37.30 < Manlift.transform.position.z) && (HookMechanicsRight.isHooked || HookMechanicsLeft.isHooked) && once==false && step==4)
        {
            
            Debug.Log("sağa gidiyorum");
            MoveTowardsCatwalkBool = false;
            isMoving = false;
            if (step == 4)
            {
                step = 5;
            }
            once = true;
        }
    }

    public void Movement()
    {
       
		
		if((ShouldMoveButtonWorkBool == true && MoveTowardsCatwalkBool == true && bottomed==1 && step==0 && (HookMechanicsRight.isHooked||HookMechanicsLeft.isHooked)) || (step == 6))
		{
            
			goToCatwalk();
			isMoving = true;

            if (37.30 < Manlift.transform.position.z)
            {
                MoveTowardsCatwalkBool = false;

                isMoving = false;
				if (step==0)
				{
                    
                    step = 1;
                    
                }
            }
        }
        if((ShouldMoveButtonWorkBool == true && MoveTowardsCatwalkBool == false && bottomed == 1 && step==5 && (HookMechanicsRight.isHooked||HookMechanicsLeft.isHooked)) || (step == 6))
        {
			goToStation();
			isMoving = true;
			
            if (37.30 > Manlift.transform.position.z)
            {
                MoveTowardsCatwalkBool = true;

                isMoving = false;
				if (step == 5)
				{
                    step = 6;
                    ManliftAudioSource.Stop();
                }
            }
        }
	}



    public void goToCatwalk()
    {
        float step = ManliftSpeed * Time.deltaTime;
        Manlift.transform.position = Vector3.MoveTowards(transform.position, CatwalkPos, step);

        //Manlift.transform.position = Vector3.Lerp(transform.position, CatwalkPos, step);
        //Debug.Log("Moving towards to catwalk");
    }

    public void goToStation()
    {
        float step = ManliftSpeed * Time.deltaTime;
        Manlift.transform.position = Vector3.MoveTowards(transform.position, StationPos, step);
        // Debug.Log("Moving towards to base station");
    }


    public void PlayManliftAudio(bool state)
    {
        switch (state)
        
        {
            
            case true:
                if (!sfxIsPlaying)
                {
                    sfxIsPlaying = true;
                    ManliftAudioSource.Play();
                    Debug.Log("Şarkı çalıyor");
                    
                }
                break;
            case false:
                if (sfxIsPlaying)
                {
                    sfxIsPlaying = false;
                    ManliftAudioSource.Stop();
                    Debug.Log("Ses Durdu");
                }
                break;
            
            
            
        }
    }
    

}