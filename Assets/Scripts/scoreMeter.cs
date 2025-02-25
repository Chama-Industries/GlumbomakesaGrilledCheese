using UnityEngine;
using UnityEngine.UI;

public class scoreMeter : MonoBehaviour
{
    public Slider glumboMeter;
    private float sliderValue;
    private collectibleData playerScore = new collectibleData();

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        sliderValue = glumboMeter.value;
    }

    private void FixedUpdate()
    {
        //get an input on when score decreases

        //boolean/identifier output to change Glumbo Image

        //get an input on when score increases

        //boolean/identifier output to change Glumbo Image
    }

}
