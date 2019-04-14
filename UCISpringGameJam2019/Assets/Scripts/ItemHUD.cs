using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine;

public class ItemHUD : MonoBehaviour
{
    [SerializeField] private GameObject Canvas;
    [SerializeField] private GameObject Image;
    [SerializeField] private GameObject Player;

    bool showHelp;

    public GameObject helpPanel;

    // Start is called before the first frame update
    void Start()
    {
        RefreshHUD();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            RefreshHUD();
            Canvas.SetActive(true);
        }
        else if (Input.GetKeyUp(KeyCode.Tab))
        {
            Canvas.SetActive(false);
        }

        if (Input.GetKeyDown(KeyCode.F1))
        {
            showHelp = !showHelp;
            helpPanel.SetActive(showHelp);
        }
    }

    private void RefreshHUD()
    {
        foreach (Transform child in Canvas.transform)
        {
            Destroy(child.gameObject);
        }
        List<Sprite> items = Player.GetComponentInChildren<Weapon>().ItemIcons;
        int numItems = items.Count;
        int count = 0;
        Debug.Log(numItems);
        for (int i = 0; i < 6; ++i)
        {
            for (int j = 0; j < 12; ++j)
            {
                if (count < numItems)
                {
                    SpawnImage(items[count], new Vector3(-540 + 100 * j, 200 - 100 * i, 0));
                    ++count;
                }
                else
                {
                    return;
                }
            }
        }
    }

    private void SpawnImage(Sprite ImageSprite, Vector3 position)
    {
        GameObject image = Instantiate(Image);
        image.GetComponent<Image>().sprite = ImageSprite;
        image.transform.SetParent(Canvas.transform);
        image.GetComponent<RectTransform>().localPosition = position;
        image.transform.localScale = new Vector3(1, 1, 1);
    }
}
