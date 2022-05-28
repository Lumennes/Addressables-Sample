using System;
using UnityEngine;
using UnityEngine.AddressableAssets;

[Serializable]
public class Done_Boundary 
{
	public float xMin, xMax, zMin, zMax;
}

public class Done_PlayerController : MonoBehaviour
{
	public float speed; // скорость важна на пк, а в тач ригидбоди = позиции тача на экране)
	public float tilt;
	public Done_Boundary boundary; // зона (прямоугольник) из которого шатл не будет выходить)

    // ADDRESSABLES UPDATES
    public AssetReference shot; // перф выстрела

	public Transform shotSpawn;
	public float fireRate;
	float nextFire;

	Touch touch; // тач
	bool isMobile; // мобайл ли

	Rigidbody _rigidboy;
    private void Start()
    {
		isMobile = UnityEngine.Device.SystemInfo.deviceType == DeviceType.Handheld; // мобайл
		// UnityEngine.Device.SystemInfo.deviceType == DeviceType.Desktop - комп
		_rigidboy = GetComponent<Rigidbody>();
	}

    void Update ()
	{
		if (((!isMobile && Input.GetButton("Fire1")) ||// если комп и ЛКМ	
			(isMobile && Input.touchCount > 0)) // если мобайл и тач
			 && Time.time > nextFire) // если время пришло 
		{
			nextFire = Time.time + fireRate;
            // ADDRESSABLES UPDATES
            shot.InstantiateAsync(shotSpawn.position, shotSpawn.rotation);
			GetComponent<AudioSource>().Play ();
		}
	}

	void FixedUpdate()
	{		
		//  управление для компа(винды/вэбгл..)
		if (!isMobile)
		{
			Vector3 movement = new(Input.GetAxis("Horizontal"), 0.0f, Input.GetAxis("Vertical"));

			_rigidboy.velocity = movement * speed;
		}
		// управление для телефона
		else
		{
			if (Input.touchCount > 0) // тачи
			{
				touch = Input.GetTouch(0); // тач

				if (touch.phase == TouchPhase.Moved) // движем
				{
					var pos = Camera.main.ScreenToWorldPoint(touch.position); // позиция тача в 3д

					_rigidboy.MovePosition(pos); // движем ригидбоди к позиции тача в 3д
				}
			}
		}		
		// тут чтоб не выходили за пределы поля
		_rigidboy.position = new
		(
			Mathf.Clamp(_rigidboy.position.x, boundary.xMin, boundary.xMax),
			0.0f,
			Mathf.Clamp(_rigidboy.position.z, boundary.zMin, boundary.zMax)
		);
		// тут чтоб красиво поворачаивали при перелёте
		_rigidboy.rotation = Quaternion.Euler(0.0f, 0.0f, _rigidboy.velocity.x * -tilt);
		
	}
}
