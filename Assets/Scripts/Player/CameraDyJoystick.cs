using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Cinemachine;

public class CameraDyJoystick : Joystick
{
    //protected Vector2 input = Vector2.zero;
	
	public float MoveThreshold { get { return moveThreshold; } set { moveThreshold = Mathf.Abs(value); } }
	
    [SerializeField]
	private float moveThreshold = 1;
	
	//referência do rect transform
	private RectTransform rect;
	
	//referência do cinemachine
	private CinemachineFreeLook cine;

    protected override void Start()
    {
        MoveThreshold = moveThreshold;
        base.Start();
        background.gameObject.SetActive(false);
		
		//pega o componente de rect transform
		rect = GetComponent<RectTransform>();
		//muda a largura e altura do rect transform
		//para poder ser usado em qualquer parte da tela
		rect.sizeDelta = new Vector2(Screen.width, Screen.height);
		
		//encontra e pega o componente do cinemachine
		cine = GameObject.FindWithTag("Cinemachine").GetComponent<CinemachineFreeLook>();
    }
	
	void Update()
	{
		//rotaciona a camera com base no input
		if(Horizontal != 0 || Vertical != 0)
		{
			cine.m_XAxis.Value += Horizontal * 1.5f;
			cine.m_YAxis.Value += -Vertical / 30;
		}
	}
	
    public override void OnPointerDown(PointerEventData eventData)
    {
        background.anchoredPosition = ScreenPointToAnchoredPosition(eventData.position);
        background.gameObject.SetActive(true);
        base.OnPointerDown(eventData);
    }

    public override void OnPointerUp(PointerEventData eventData)
    {
        background.gameObject.SetActive(false);
        base.OnPointerUp(eventData);
    }

    protected override void HandleInput(float magnitude, Vector2 normalised, Vector2 radius, Camera cam)
    {
        if (magnitude > moveThreshold)
        {
            Vector2 difference = normalised * (magnitude - moveThreshold) * radius;
            background.anchoredPosition += difference;
        }
        base.HandleInput(magnitude, normalised, radius, cam);
    }
}
