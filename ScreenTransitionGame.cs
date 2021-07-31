using UnityEngine;
using UnityEngine.UI;
public class ScreenTransitionGame : MonoBehaviour
{
    private Image image;
    private Color color;
    // Start is called before the first frame update
    void Start()
    {
        color = image.color;
    }
    private float alphaVal = 1.0f;
    private float speed = 2.0f;
    private bool done = false;
    // Update is called once per frame
    void Update()
    {
        transform.SetAsLastSibling();
        alphaVal -= Time.deltaTime;
        image.color = new Color(0.0f, 0.0f, 0.0f, alphaVal);
        if(alphaVal <= 0.0f)
        {
            done = true;
            gameObject.SetActive(false);
        }
    }
}
