using UnityEngine;
using UnityEngine.UI;

public class ArrowBtnsController : MonoBehaviour
{

    public int indexArrowBtn;
    public Button btn;
    public Canvas OptionsMenu;

    private int currentIndexBtn = 3;

    private Transform t;
    private SpriteRenderer spr;

    void Start()
    {
        t = GetComponent<Transform>();
        spr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        t.Rotate(0, 200 * Time.deltaTime, 0);

        if (!OptionsMenu.enabled)
        {
            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                if (currentIndexBtn > 1)
                {
                    currentIndexBtn--;
                }
            }
            else if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                if (currentIndexBtn < 3)
                {
                    currentIndexBtn++;
                }
            }

            if (indexArrowBtn == currentIndexBtn)
            {
                spr.enabled = true;
                btn.Select();
            }
            else
            {
                spr.enabled = false;
            }
        }
    }
}
