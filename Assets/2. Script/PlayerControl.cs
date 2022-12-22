using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerControl : MonoBehaviour
{
    public GameObject Player;

    public float MoveSpeed;
    public int Score;

    public TextMeshProUGUI ScoreText;

    public GameObject[] Soul;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SoulCreate());
        Score = 0;
    }

    // Update is called once per frame
    void Update()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        Vector3 dir = new Vector3(h, v, 0);
        //gameObject.transform.position += dir;
        GetComponent<Rigidbody2D>().velocity = dir * MoveSpeed;
        if(Input.GetMouseButtonDown(0))
        {
            gameObject.GetComponent<SpriteRenderer>().color = Color.white;
        }
        ScoreText.text = "Score : " + Score;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("WALL"))
        {
            GetComponent<SpriteRenderer>().color = collision.collider.GetComponent<SpriteRenderer>().color;

            StopCoroutine("ColorBack");
            StartCoroutine("ColorBack");
        }
    }

    IEnumerator ColorBack()
    {
        yield return new WaitForSeconds(3f);
        GetComponent<SpriteRenderer>().color = Color.white;
    }

    IEnumerator SoulCreate()
    {
        float x = Random.Range(-5.5f, 5.5f);
        float y = Random.Range(-3f, 3f);
        Vector3 pos = new Vector3(x, y, 0);

        int Index = Random.Range(0, 4);
        yield return new WaitForSeconds(3f);

        Instantiate(Soul[Index], pos, Quaternion.identity);

        StartCoroutine(SoulCreate());
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        float x = Random.Range(-5.5f, 5.5f);
        float y = Random.Range(-3f, 3f);
        if (other.CompareTag("SOUL"))
       {
            if(Player.GetComponent<SpriteRenderer>().color == other.GetComponent<SpriteRenderer>().color)
            {
                Destroy(other.gameObject);
                MoveSpeed++;
                GetComponent<AudioSource>().Play();
                Score++;
                
            }
            if (Player.GetComponent<SpriteRenderer>().color != other.GetComponent<SpriteRenderer>().color)
            {
                other.gameObject.transform.position = new Vector3( x, y, 0);
                MoveSpeed--;
                if (Score > 0)
                {
                    Score--;
                }
            }
        }
    }

    public void Left()
    {
        transform.position += new Vector3(-1, 0, 0) * MoveSpeed * 0.1f;
    }

    public void Right()
    {
        transform.position += new Vector3(1, 0, 0) * MoveSpeed * 0.1f;
    }

    public void Up()
    {
        transform.position += new Vector3(0, 1, 0) * MoveSpeed * 0.1f;
    }

    public void Down()
    {
        transform.position += new Vector3(0, -1, 0) * MoveSpeed * 0.1f;
    }
}
