using UnityEngine;
using System.Collections;

public class PlayerScript: MonoBehaviour {
	public float movementSpeed = 5;
	public float jumpHeight = 3;
	private float jumpSpeed;
	private float verticalSpeed = 0;
	private bool onGround = false;
	[Range(50, 100)]
	public int cameraSensitivity = 50;

	public bool inverX = false;
	public bool inverY = false;

	public int maximumPitch = 90;
	public int minimumPitch = -90;
	private float currentPitch;

	public int maxAmmo = 90;
	public int startingAmmo = 30;
	private int currentAmmo;

	public float maxStamina = 100;
	private float currentStamina;

	private bool couldSprint = false;
	private bool sprinting = false;
	private float sprintDelay = 0.3f;
	private float sprintWindowStart;

	public Texture2D crosshairTexture;
	private Rect crosshairRect;

	public int maximumHp = 100;
	private int currentHp;

	void OnGUI() {
		GUI.DrawTexture(crosshairRect, crosshairTexture);
	}

	// Use this for initialization
	void Start() {
		currentPitch = Camera.main.transform.localEulerAngles.x;

		Cursor.lockState = CursorLockMode.Locked;
		Cursor.visible = false;

		crosshairRect = new Rect(5, 5, crosshairTexture.width / 4, crosshairTexture.height / 4);
		jumpSpeed = Mathf.Sqrt(2 * -Physics.gravity.y * jumpHeight);
		currentAmmo = startingAmmo;
		currentHp = maximumHp;
	}

	// Update is called once per frame
	void Update() {
		Vector3 direction = new Vector3();
		if(Input.GetKeyDown(KeyCode.W)) {
			if(Time.realtimeSinceStartup - sprintWindowStart <= sprintDelay) {
				sprinting = true;
			}

			sprintWindowStart = Time.realtimeSinceStartup;
		}
	
			if(Input.GetKeyUp(KeyCode.W)) {
				sprinting = false;
			}
		
		if (Input.GetKey(KeyCode.W))
        {
						direction.z += 1;
				}
        if (Input.GetKey(KeyCode.S))
        {
            direction.z -= 1;
						sprinting = false;
        }
        if (Input.GetKey(KeyCode.A))
        {
            direction.x -= 1;
        }
        if (Input.GetKey(KeyCode.D))
        {
            direction.x += 1;
        }
				
        direction.Normalize();
        direction = transform.TransformDirection(direction);

		if(sprinting) {
			currentStamina -= Time.deltaTime * 20;
			if(currentStamina <= 0) {
				currentStamina = 0;
				sprinting = false;
			}
		}else{
			currentStamina += Time.deltaTime * 35;
			currentStamina = Mathf.Min(currentStamina, maxStamina);

		}

				float sprintModifier = sprinting ? 5 : 1;
        GetComponent<CharacterController>().Move(direction * Time.deltaTime * movementSpeed* sprintModifier);

        UpdateCamera();
		//Do jumping
				if(Input.GetKeyDown(KeyCode.Space)) {
				if(onGround) {
				verticalSpeed = jumpSpeed;
				}
				}
				onGround = false;
					if(!onGround) {
					verticalSpeed += Physics.gravity.y * Time.deltaTime;
				}
				
				GetComponent<CharacterController>().Move(Vector3.up * verticalSpeed*Time.deltaTime);
			
		//Do shooting
		if(Input.GetMouseButtonDown(0)) {
			if(currentAmmo > 0) {
				currentAmmo -= 1;
						GetComponent<AudioSource>().Play();
								RaycastHit hitInfo;
								if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.TransformDirection(Vector3.forward),out hitInfo, 100)) ;
								{
										if (hitInfo.collider.gameObject.tag=="Enemy")
										{
												Debug.Log("Bang!");
												hitInfo.collider.gameObject.GetComponent<EnemyScript>().TakeDamage(20);
										}
								}
					}
			}
			
						
       

        crosshairRect.x = (Screen.width - crosshairRect.width) / 2;
        crosshairRect.y = (Screen.height - crosshairRect.height) / 2;
	}
    void UpdateCamera()
    {
        float dx = Input.GetAxis("Mouse X");
        float dy = -Input.GetAxis("Mouse Y");

        dx *= cameraSensitivity;
        dy *= cameraSensitivity;

        if(inverX)
        {
            dx = -dx;
        }
        if(inverY)
        {
            dy = -dy;
        }

        transform.Rotate(0, dx * Time.deltaTime, 0);

        currentPitch += dy*Time.deltaTime;
        currentPitch = Mathf.Clamp(currentPitch, minimumPitch, maximumPitch);

        Vector3 rotation = Camera.main.transform.localEulerAngles;
        rotation.x = currentPitch;
        Camera.main.transform.localEulerAngles = rotation;
    }
	void OnControllerColliderHit(ControllerColliderHit hit) {
		if(hit.normal.y>Mathf.Abs(hit.normal.x) 
			&& hit.normal.y > Mathf.Abs(hit.normal.z)) {
			onGround = true;
			verticalSpeed = -1;
		}

		if(hit.gameObject.tag == "PickUp") {
			currentAmmo += hit.gameObject.GetComponent<PickUpScript>().ammoValue;
			Destroy(hit.gameObject);

			currentAmmo = Mathf.Min(currentAmmo, maxAmmo);
		}
	}

		public void TakeDamage(int amount) {
			currentHp = currentHp - amount;
			if(currentHp <= 0) {
				Application.LoadLevel(Application.loadedLevel);
			}
		}
}
