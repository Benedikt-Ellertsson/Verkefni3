using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using TMPro;

public class Player : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI healthText;
    public int health = 100; // Breyta fyrir líf leikmanns og stillt sem 100.
    public int score; // Breyta fyrir stig leikmanns, byjar á núl.

    private void Start()
    {
        score = 0; // Við ræsingur er núlstillt stig.
        SetScoreText(); // Setur texta í gang fyrir stig leikmanns og uppfærir það í UI hjá leikmanni
        SetHealthText(); // Setur texta í gang fyrir líf leikmanns og uppfærir það í UI hjá leikmanni
    }
    void SetScoreText()
    {
        scoreText.text = "Score: " + score.ToString(); // Uppfærir textan í nýjan textan (Ef spilari fær stig eða missir sitg)
        if (score >= 8) SceneManager.LoadScene(3); // Ef leikmaður fær 8 stig eða meira þá lýkur leiknum og leikmaður hefur unnið.
    }
    void SetHealthText()
    {
        healthText.text = "Líf: " + health.ToString(); // Uppfærir textan fyrir UI og sýnir nýju stöðuna á lífi leikmanns
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("PickUp")) // Ef gameObject er merkt "PickUp" þá ->
        {
            collision.gameObject.SetActive(false); // Slekkur á gameObject
            score = score + 2; //  Gefur leikmanni 2 stig.
            health = health + 10; // Gefur leikmanni 10 auka líf
            SetScoreText(); // Sendir á fall að uppfæra UI texta
            SetHealthText(); // Sendir á fall að uppfæra UI texta
        }
        if (collision.gameObject.CompareTag("EnemyProjectile")) // Ef gameObject af taginu "Projectile_Ovinur" snertir leikmann þá...
        {
            TakeDamage(25); // TakeDamage("Magn lífs sem leikmaður missir") keyrt.
            score = score - 1; // Ef leikmaður er skotin af óvin þá missir hann eitt stig
            Destroy(collision.gameObject); // Eyðir gameobjectinu sem var skotið á leikmann
            SetScoreText(); // Uppfæra UI texta
            //if (score < 0) SceneManager.LoadScene(2);     // Ef spilari fer undir 0 í stigum þát tapar hann.
        }
        if (collision.gameObject.CompareTag("Water"))
        {
            TakeDamage(100);
        }
    }
    private void TakeDamage(int damage)
    {
        health -= damage; // Leikmaður missir líf samkvæmt magninu sem "damage" inniheldur
        SetHealthText();
        Debug.Log("Spilari var skotin!");
        if (health <= 0) // Ef líf leikmanns verður eða fer undir 0 þá....
        {
            Death(); // Death() keyrt
        }
    }
    private void Death()
    {
        Debug.Log("Spilari deyr hér!");
        SceneManager.LoadScene(2); // Leikmaður tapaði og sena keyrð sem leyfir leikmanni að reyna aftur.

    }

}
