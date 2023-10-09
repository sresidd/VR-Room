
using UnityEngine;
using DG.Tweening;

public class ClockBehavior : MonoBehaviour {

    public GameObject secondHand;
    public GameObject minuteHand;
    public GameObject hourHand;
    string oldSeconds;

	void Update () {

        string seconds = System.DateTime.UtcNow.ToString("ss");
        

        if (seconds != oldSeconds) {
            UpdateTimer();
        }
        oldSeconds = seconds;
	}

    void UpdateTimer() {

        int secondsInt = int.Parse(System.DateTime.UtcNow.ToString("ss"));
        int minutesInt = int.Parse(System.DateTime.UtcNow.ToString("mm"));
        int hoursInt = int.Parse(System.DateTime.UtcNow.ToLocalTime().ToString("hh"));
        // print(hoursInt + " : " + minutesInt +  " : " + secondsInt);


        secondHand.transform.DORotate(new Vector3(secondsInt * 6 + 90 ,0,-90),1).SetEase(Ease.OutQuint);
        minuteHand.transform.DORotate(new Vector3(minutesInt * 6 + 90,0,-90),1).SetEase(Ease.OutElastic);
        float hourDistance = (float)(minutesInt) / 60f;
        hourHand.transform.DORotate(new Vector3(((hoursInt + hourDistance) * 360/12) + 90,0,-90),1).SetEase(Ease.OutQuint);
    }
}