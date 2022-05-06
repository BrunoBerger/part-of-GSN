using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RhythmController : MonoBehaviour
{
    [SerializeField]
    GameState gameState;
    [SerializeField]
    PlayRhythm playRhythm;
    [SerializeField]
    AudioClips audioClips;
    [SerializeField]
    RhythmVisualisation rhythmVisualisation;
    public float tactSpeed = 1f;
    int beatPos = 0;
    bool nextTact = false;
    float slowestTactSpeed;
    // Start is called before the first frame update
    void Start()
    {
        tactSpeed = 1 / gameState.speed;
        nextTact = true;
        beatPos = 0;
        slowestTactSpeed = tactSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        if(nextTact)
            StartCoroutine(tact());

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            rhythmVisualisation.up(beatPos);
            playRhythm.up();
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            rhythmVisualisation.down(beatPos);
            playRhythm.down();
        }
    }

    void endOfRhythm()
    {
        if (rhythmVisualisation.rhythmCorrect() && gameState.beerCounter > 0)
        {
            tactSpeed /= 1.25f;
            gameState.speed *= 1.25f;
            gameState.beerCounter -= 1;
            gameState.danceRush = 3;
        }
        else if (gameState.danceRush == 0 && tactSpeed < slowestTactSpeed && gameState.speed > slowestTactSpeed)
        {
            Debug.Log(tactSpeed + " < " + slowestTactSpeed);
            tactSpeed *= 1.25f;
            gameState.speed /= 1.25f;
        }
        else if(gameState.danceRush > 0)
        {
            gameState.danceRush -= 1;
        }

        if (gameState.chord < audioClips.chords.Count - 1)
            gameState.chord++;
        else
            gameState.chord = 0;
    }

    IEnumerator tact()
    {
        if(beatPos == 0)
            rhythmVisualisation.resetAllColors();

        nextTact = false;
        rhythmVisualisation.onTact(beatPos);
        yield return new WaitForSeconds(tactSpeed/2);
        playRhythm.onTact(beatPos);
        yield return new WaitForSeconds(tactSpeed/2);

        if (beatPos == 7)
        {
            beatPos = 0;
            endOfRhythm();
        }
        else
            beatPos++;

        nextTact = true;
    }
}
