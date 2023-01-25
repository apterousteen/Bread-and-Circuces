using UnityEngine;
using UnityEngine.SceneManagement;

public class mainMenuPhotonButton : MonoBehaviour
{
    public void ButtonPlay()
    {
        SceneManager.LoadScene("PhotonTest");
    }
}
 