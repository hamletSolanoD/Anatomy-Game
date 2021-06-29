using UnityEngine;
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(CharacterController))]
public class PlayerMovments : MonoBehaviour

{
    public float Gravedad = 0.2f;
    public float Impulso_Salto = 2.5f;
    public float Velocidad_Correr = 20;
    public float Velocidad_Caminar = 15;
    public float Smooth_Running = 0.08f;
    public float Smooth_Rotacion = 0.1f;
    public float Doble_Salto_Multiplier = 1;
    [Range(0.1f, 1)]
    public float Control_Aereo = 1;

    
    private Animator Animator;
    private GameObject camera;
    private CharacterController Character;
    private Vector3 destino;
    private float velocidad;
    private float velocidadY;
    private float SmoothSupportRunVariable;

    //Multiples saltos:
    public int Saltos_Permitidos = 2;
    private int JumpCount;
    private bool InAir;
    private GroundRay ray;


    private float SmoothSupportAngleVariable;
    private Vector3 SmoothSupportMoveVariable;

    public float SmoothKnockBack = 8;
    private Vector3 knockbackforce;
    private Vector3 knockbackforceFinal;
    private Vector3 knockbackforceHelper;

    private bool Enfundado = true;


    // Start is called before the first frame update
    void Start()
    {
        Animator = GetComponent<Animator>();
        camera = Camera.main.gameObject;
        Character = GetComponent<CharacterController>();
        ray = GetComponentInChildren<GroundRay>();
    }
    public void setEnfundado(bool Enfundado) {
        this.Enfundado = Enfundado;
    }

    // Update is called once per frame
    void Update()
    {
        
        Vector2 vectorMov;
        vectorMov.x = Input.GetAxisRaw("Horizontal");
        vectorMov.y = Input.GetAxisRaw("Vertical");
        Vector2 dirMov = vectorMov.normalized;
     
       
        Movment(dirMov);
        float animationspeed = (velocidad >= Velocidad_Caminar ? 1 : 0.5f) * dirMov.magnitude;
        Animator.SetFloat("Running", animationspeed);
    }
    private float SmoothModificado(float Smooth) {
        if (!ray.InGround()) {
            return Smooth / Control_Aereo;
        }
        return Smooth;
    }

    private void Jump() {
        if (ray.InGround())
        {
            Animator.SetBool("Saltar", false);
            Animator.SetBool("DobleSalto", false);
            JumpCount = 0;
            InAir = false;
            if (Input.GetKeyDown(KeyCode.Space) )
            {
                Animator.SetBool("Saltar", true);
                velocidadY = Mathf.Sqrt(Impulso_Salto * Gravedad * 2);
                JumpCount = 1;
            }
        }
        if (InAir) {
            if (Input.GetKeyDown(KeyCode.Space) && JumpCount < Saltos_Permitidos && Enfundado)
            {
                velocidadY = Mathf.Sqrt(Impulso_Salto * Gravedad * 2) * Doble_Salto_Multiplier;
                velocidad *= Doble_Salto_Multiplier;
                Animator.SetBool("Saltar", true);
                Animator.SetBool("DobleSalto", true);
                JumpCount++;
            }
        }
        if (!ray.InGround() && InAir == false && Enfundado)
        {
            InAir = true;
            JumpCount = 1;
        }
    }
    public void  Knock_Back(Vector3 Direction, float Knock_Back_Multiplier) {
 
            Knock_Back_Multiplier = Knock_Back_Multiplier / 10;
            Direction = Direction.normalized;
            knockbackforceFinal =   Direction * Knock_Back_Multiplier;


    }
    private Vector3 knockBackCheck() {
        if (knockbackforce == knockbackforceFinal)
        {
            knockbackforceFinal = Vector3.zero;
            knockbackforce = Vector3.zero;
        }
      return knockbackforce = Vector3.SmoothDamp(knockbackforce, knockbackforceFinal, ref knockbackforceHelper, Time.deltaTime * SmoothKnockBack);

    }
   
    private void Movment(Vector2 dirMov) {
        if (dirMov != Vector2.zero)
        {
            float direccionDestino = Mathf.Atan2(dirMov.x, dirMov.y) * Mathf.Rad2Deg + camera.transform.eulerAngles.y;
            transform.eulerAngles = Vector3.up * Mathf.SmoothDampAngle(transform.eulerAngles.y, direccionDestino, ref SmoothSupportAngleVariable, SmoothModificado(Smooth_Rotacion));
        }
        float velocidadTarget = ((Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift) && Enfundado)? Velocidad_Correr : Velocidad_Caminar) * dirMov.magnitude;
        
        velocidad = Mathf.SmoothDamp(velocidad, velocidadTarget, ref SmoothSupportRunVariable, SmoothModificado(Smooth_Running));

        velocidadY = (ray.InGround() ? 0 : velocidadY - Gravedad);
        Jump();
        destino = destino + (transform.forward * velocidad + Vector3.up*velocidadY)*Time.deltaTime + (knockBackCheck());
        Character.Move(destino);
        destino = Vector3.zero;
    }
}
