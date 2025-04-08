using System.Collections;
using System.Runtime.CompilerServices;
using UnityEngine;

public class EnemySpanwer : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField]
    private StageData stageData;
    [SerializeField]
    private GameObject enemyPrefab;
    [SerializeField]
    private GameObject enemyHPSliderPrefab;
    [SerializeField]
    private Transform canvasTransform;
    [SerializeField]
    private GameObject textBossWarning;
    [SerializeField]
    private GameObject panelBossHP;
    [SerializeField]
    private GameObject boss;
    [SerializeField]
    private float spawnTime;
    [SerializeField]
    private int maxEnemyCount = 5;
    private bool startSpawn = true;

    private void Awake()
    {
        textBossWarning.SetActive(false);
        panelBossHP.SetActive(false);
        boss.SetActive(false);
        StartCoroutine("SpawnEnemy");
        StartCoroutine("SpawnBoss");
    }

    private IEnumerator SpawnEnemy()
    {
        while (startSpawn)
        {
            float positionX = Random.Range(stageData.LimitMin.x, stageData.LimitMax.x);
            Vector3 position = new Vector3(positionX, stageData.LimitMax.y + 1.0f, 0.0f);
            GameObject enemyClone = Instantiate(enemyPrefab, new Vector3(positionX, stageData.LimitMax.y + 1.0f, 0.0f), Quaternion.identity);
            SpawnEnemyHPSlider(enemyClone);
            yield return new WaitForSeconds(spawnTime);
        }
    }

    private void SpawnEnemyHPSlider(GameObject enemy)
    {
        GameObject sliderClone = Instantiate(enemyHPSliderPrefab);
        sliderClone.transform.SetParent(canvasTransform);
        sliderClone.transform.localScale = Vector3.one;
        sliderClone.GetComponent<SliderPositionAutoSetter>().Setup(enemy.transform);
        sliderClone.GetComponent<EnemyHPViewer>().Setup(enemy.GetComponent<EnemyHP>());
    }

    private IEnumerator SpawnBoss()
    {   
        yield return new WaitForSeconds(5.0f);
        textBossWarning?.SetActive(true);
        yield return new WaitForSeconds(5.0f);
        startSpawn = false;
        textBossWarning.SetActive(false);
        panelBossHP.SetActive(true);
        boss.SetActive(true);
        boss.GetComponent<Boss>().ChangeState(BossState.MoveToAppearPoint);
    }
}
