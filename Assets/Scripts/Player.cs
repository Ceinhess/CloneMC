using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    [Range(0, 89)] public float MaxXAngle = 85f;

    public float XAxisSensitivity = 1f;
    public float YAxisSensitivity = 1f;

    public KeyCode Forwards = KeyCode.W;
    public KeyCode Backwards = KeyCode.S;
    public KeyCode Left = KeyCode.A;
    public KeyCode Right = KeyCode.D;
    public KeyCode Up = KeyCode.Space;
    public KeyCode Sprint = KeyCode.LeftShift;
    public KeyCode SwitchCamera = KeyCode.F5;

    public float Speed = 6f;




    private bool ThirdPerson = false;

    private CharacterController Controller;

    private GameObject Cam;

    private Vector3 Velocity;
    


    // Start is called before the first frame update
    void Start()
    {
        Controller = this.gameObject.GetComponent<CharacterController>();
        Cam = gameObject.transform.Find("Head").gameObject.transform.Find("Camera").gameObject;

        Velocity = new Vector3(0,0,0);

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        this.gameObject.GetComponent<MeshRenderer>().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        // Rotation souris/camera si pas echap presse
        if(!Cursor.visible)
            HandleMouseRotation();

        //Deplacements
        HandleKeys();
        

        // Gravite
        if(!Controller.isGrounded && Controller.velocity.y > -8)
        {
            Velocity.y -= 25f * Time.deltaTime;
        }
        else if (Controller.isGrounded && Controller.velocity.y < 0)
        {
            Velocity.y = 0;
        }

        // Rotation objet (calcule dans HandleMouseRotation) fois velocite pr rotationner le vecteur velocite dans la direction du joueur
        Quaternion angle = gameObject.transform.rotation;
        angle.x = 0;
        Vector3 MovementVel = (angle  *  (new Vector3(Velocity.x, 0, Velocity.z)));
        // on traite le vel Y differemment pcq gravite tt ca
        MovementVel.y = Velocity.y;


        Controller.Move(MovementVel * Time.deltaTime);
        


        
    }

    private float _rotationX;

    private void HandleMouseRotation()
    {
        //mouse input
        var rotationHorizontal = XAxisSensitivity * Input.GetAxis("Mouse X");
        var rotationVertical = YAxisSensitivity * Input.GetAxis("Mouse Y");

        //applying mouse rotation
        // always rotate Y in global world space to avoid gimbal lock
        transform.Rotate(Vector3.up * rotationHorizontal, Space.World);

        var rotationY = transform.localEulerAngles.y;

        _rotationX += rotationVertical;
        _rotationX = Mathf.Clamp(_rotationX, -MaxXAngle, MaxXAngle);

        transform.localEulerAngles = new Vector3( 0, rotationY, 0);
        gameObject.transform.Find("Head").gameObject.transform.localEulerAngles = new Vector3(-_rotationX, 0, 0);
    }



    private void HandleKeys()
    {
        if (Input.GetKey(Forwards))
        {
            Velocity.z = Speed;
        }
        else if(Velocity.z > 0)
        {
            Velocity.z = 0;
        }

        if (Input.GetKey(Backwards))
        {
            Velocity.z = -6;
        }
        else if(Velocity.z < 0)
        {
            Velocity.z = 0;
        }

        if (Input.GetKey(Left))
        {
            Velocity.x = -6;
        }
        else if(Velocity.x < 0)
        {
            Velocity.x = 0;
        }

        if (Input.GetKey(Right))
        {
            Velocity.x = 6;
        }
        else if(Velocity.x > 0)
        {
            Velocity.x = 0;
        }

        if (Input.GetKey(Sprint))
        {
            Speed = 9f;
        }
        else if(Speed != 1f)
        {
            Speed = 6f;
        }

        if(Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.visible = !Cursor.visible;
            if(Cursor.visible)
                Cursor.lockState = CursorLockMode.None;
            else
                Cursor.lockState = CursorLockMode.Locked;
        }

        if(Input.GetKeyDown(SwitchCamera))
        {
            if(ThirdPerson)
            {
                Cam.transform.Translate(new Vector3(0,0,5));
                this.gameObject.GetComponent<MeshRenderer>().enabled = false;
            }
                
            else
            {
                Cam.transform.Translate(new Vector3(0,0,-5));
                this.gameObject.GetComponent<MeshRenderer>().enabled = true;
            }
                

            ThirdPerson = !ThirdPerson;

        }


        if (Input.GetKey(Up) && Controller.isGrounded)
        {
            Velocity.y += 8;
        }


    }


}
