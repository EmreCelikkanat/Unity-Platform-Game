using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectionArrow : MonoBehaviour
{
    private RectTransform rect;
    [SerializeField] private RectTransform[] options;
    [SerializeField]private AudioClip clip;
    [SerializeField] private AudioClip interactSound;


    private int currentpos;

    private void Awake()
    {
        rect = GetComponent<RectTransform>();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
            ChangePos(-1);
       else if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
            ChangePos(1);
        if (Input.GetKeyDown(KeyCode.KeypadEnter) || Input.GetKeyDown(KeyCode.E))
            Interact();
    }
    private void ChangePos(int change)
    {
        currentpos += change;
        if (change != 0)
            SoundManager.instance.PlaySound(clip);
        if (currentpos < 0)
            currentpos = options.Length - 1;
        else if(currentpos> options.Length-1)
            currentpos = 0;
        rect.position = new Vector3(rect.position.x, options[currentpos].position.y, 0);

    }
    private void Interact()
    {
        SoundManager.instance.PlaySound(interactSound);
        options[currentpos].GetComponent<Button>().onClick.Invoke();
    }
}
