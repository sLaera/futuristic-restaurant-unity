using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SojaExiles

{
	public class opencloseDoor : MonoBehaviour
	{

		public Animator openandclose;
		public bool open;
		public Transform Player;
		private OcclusionPortal _portal;
		
		void Start()
		{
			//open = true;
			//openandclose.Play("Opening");
			/*_portal = GetComponent<OcclusionPortal>();
			if (_portal)
			{
				_portal.open = false;
			}*/
		}
		/*
		private void OnTriggerEnter(Collider other)
		{
			if (other.CompareTag("robot") || other.CompareTag("Player"))
			{
				if (open == false)
				{
					openandclose.Play("Opening");
					open = true;
				}
            }
        }
		
		private void OnTriggerExit(Collider other)
		{
			if (other.CompareTag("robot") || other.CompareTag("Player"))
			{
				if (open == true)
				{
					openandclose.Play("Closing");
					open = false;
				}
				
            }
		}
		*/
		
		
		/*
		void OnMouseOver()
		{
			{
				if (Player)
				{
					float dist = Vector3.Distance(Player.position, transform.position);
					if (dist < 15)
					{
						if (_portal)
						{
							_portal.open = open;
						}
						if (open == false)
						{
							if (Input.GetMouseButtonDown(0))
							{
								StartCoroutine(opening());
							}
						}
						else
						{
							if (open == true)
							{
								if (Input.GetMouseButtonDown(0))
								{
									StartCoroutine(closing());
								}
							}

						}

					}
				}

			}

		}

		IEnumerator opening()
		{
			print("you are opening the door");
			openandclose.Play("Opening");
			open = true;
			yield return new WaitForSeconds(.5f);
		}

		IEnumerator closing()
		{
			print("you are closing the door");
			openandclose.Play("Closing");
			open = false;
			yield return new WaitForSeconds(.5f);
		}

		*/
	}
	
}