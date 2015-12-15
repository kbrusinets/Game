using UnityEngine;
using System.Collections;

public class OnContact : MonoBehaviour
{
    public GameObject blood;

    private GameController gameController;
    // Use this for initialization
    void Start()
    {
        GameObject gameControllerObject = GameObject.FindWithTag("GameController");
        if (gameControllerObject != null)
        {
            gameController = gameControllerObject.GetComponent<GameController>();
        }
        if (gameController == null)
        {
            Debug.Log("Cannot find 'GameController' script");
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (tag == "Enemy" && other.tag == "Castle")
        {
            gameController.changeHp(-20.0f);
            Destroy(gameObject);
            return;
        }
        else
            if (tag == "Girl" && other.tag == "Castle")
            {
                gameController.addExp(100.0f);
                Destroy(gameObject);
                return;
            }
            else
                if (tag == "Food" && other.tag == "Castle")
                {
                    gameController.changeHp(20.0f);
                    Destroy(gameObject);
                    return;
                }
                else
                    if (tag == "Bullet" && other.tag == "Castle")
                    {
                        return;
                    }
                    else
                        if (tag == "Enemy" && other.tag == "Bullet")
                        {
                            gameController.addExp(20.0f);
                            Instantiate(blood, transform.position, Random.rotation);
                        }
                        else
                            if (tag == "Girl" && other.tag == "Bullet")
                            {
                                gameController.changeHp(-20.0f);
                                Instantiate(blood, transform.position, Random.rotation);
                            }
                            else
                                if (other.tag == "BorderLine")
                                {
                                    return;
                                }
        Destroy(other.gameObject);
        Destroy(gameObject);
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "BorderLine")
        {
            Destroy(gameObject);
        }
    }
}
