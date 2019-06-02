using System.Collections;
using UnityEngine;

public class SceneManager : MonoBehaviour
{
    [SerializeField]
    public GameObject letter;

    [SerializeField]
    public GameObject cursor;

    [SerializeField]
    public AudioClip typeSound;

    [SerializeField]
    public AudioClip endOfLineSound;

    [SerializeField]
    public Sprite[] characters;

    Vector2 startPos = new Vector2(0f, 3.6f);
    Vector2 pos;
    readonly float lineSpacing = 0.5f;   
    readonly float charSpacing = 0.35f;
    char[] convertedString;
    GameObject spriteChar;
    GameObject spriteCursor;
    AudioSource audioPlayer;

    readonly string[] data = { "terminal or", "typewriter effect", " ", "check out description", "for source code" };

    void Start()
    {
        pos = startPos;
        audioPlayer = GetComponent<AudioSource>();
        StartCoroutine(PrintOut());
    }

    IEnumerator PrintOut()
    {
        int charToPrint;
        Vector2 lastPos = new Vector2(0, 0);

        spriteCursor = Instantiate(cursor, pos, Quaternion.identity);
        foreach (string singleLine in data) // Handle each line
        {
            convertedString = singleLine.ToCharArray();
            pos.x = ((singleLine.Length * charSpacing) / 2) * -1;
            foreach (char singleChar in singleLine) // Handle each character
            {
                charToPrint = SelectChar(singleChar);
                if (charToPrint >= 0 && charToPrint <= 26)  // only accept lower cap characters
                {
                    spriteChar = Instantiate(letter, pos, Quaternion.identity);
                    spriteChar.GetComponent<SpriteRenderer>().sprite = characters[charToPrint];
                    spriteCursor.transform.position = pos + new Vector2(charSpacing, 0);
                    audioPlayer.PlayOneShot(typeSound, 1f);
                    yield return new WaitForSeconds(Random.Range(0.05f, 0.35f));    // Min/max random delay between characters
                }
                pos.x += charSpacing; // testnumber for char width + char spacing
            }
            lastPos = pos;
            pos.y -= lineSpacing;
            pos.x = startPos.x;
        }
        spriteCursor.transform.position = lastPos;
    }

    int SelectChar(char selChar) 
    {
        return selChar - 97; 
    }
}
