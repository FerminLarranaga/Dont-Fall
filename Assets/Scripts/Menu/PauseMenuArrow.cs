using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PauseMenuArrow : MonoBehaviour
{
    public int indexArrowBtn;
    public int currentIndexBtn = 4;

    public Button btn;
    public GameObject OptionsMenu;

    private RectTransform rt;
    private TextMeshProUGUI img;

    private int limitedIndex;
    public int fixingBug = 0;

    void Start()
    {
        rt = GetComponent<RectTransform>();
        img = GetComponent<TextMeshProUGUI>();

        limitedIndex = currentIndexBtn;
    }

    void Update()
    {
        rt.Rotate(200 * Time.unscaledDeltaTime, 0, 0);

        if (!OptionsMenu.activeSelf)
        {
            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                if (currentIndexBtn > 1)
                {
                    currentIndexBtn--;
                }
            } else if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                if (currentIndexBtn < limitedIndex)
                {
                    currentIndexBtn++;
                }
            }

            if (indexArrowBtn == currentIndexBtn)
            {
                if(fixingBug <= 5){
                    btn.interactable = false;
                    btn.interactable = true;
                    fixingBug++;
                }
                img.enabled = true;
                btn.Select();
            } else
            {
                img.enabled = false;
            }
        }
    }

}
