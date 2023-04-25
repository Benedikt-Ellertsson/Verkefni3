using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    public NavMeshAgent agent;

    public Transform player; // gameObject spilara
    public Transform attackPoint; // Hvaðan kúlan á að birtast frá

    public LayerMask whatIsGround, whatIsPlayer;

    public float health; // Líf óvinar

    //Patroling
    public Vector3 walkPoint; // Breyta fyrir áfangastað óvinar sem gengur um heiminn.
    bool walkPointSet; // Breyta sem segir til hvort óvinur sé með áfangastað eða ekki
    public float walkPointRange; // Breyta sem er skipuð gildi í Inspector um hversu langt hann má labba út frá sinni staðsetningu

    //Attacking
    public float timeBetweenAttacks; // Tími milli árasa óvinar
    bool alreadyAttacked; // Breyta sem segir til hvort óvinur hafi skotið eða ekki
    public GameObject projectile; // gameObject fyrir kúluna sem óvinur mun skjóta

    //States
    public float sightRange, attackRange; // Stillanleg breyta fyrir sjónarsvið og árasarsvið óvinar
    public bool playerInSightRange, playerInAttackRange;

    private void Awake()
    {
        player = GameObject.Find("Player").transform;
        agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        //Check for sight and attack range
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer); // Athugar hvort spilari sé innan sjónarsvið óvinar(Eftir ásettu sjónarsviði)
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer); // Athugar hvort spilari sé innan árásarsvið óvinar(Eftir ásettu árásarsviði)

        if (!playerInSightRange && !playerInAttackRange) Patroling(); // Ef spilari ekki nægilega nálægt og ekki innan við árásarsvið þá keyrir Patroling()
        if (playerInSightRange && !playerInAttackRange) ChasePlayer(); // Ef spilari er nægilega nálægt en ekki innan við árásarsvið þá keyrir ChasePlayer()
        if (playerInAttackRange && playerInSightRange) AttackPlayer(); // Ef spilari er nægilega nálægt og innan árásarsvið þá keyrir AttackPlayer()
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.gameObject.tag == "Projectile")
        {
            //Debug.Log("Skot í óvin!");
            Destroy(gameObject);
            TakeDamage(25); // Ef gameobject af taginu "Projectile" snertir óvinn þá keyrir TakeDamage("hversu mikið líf hann missir")
        }
    }
    private void Patroling()
    {
        if (!walkPointSet) SearchWalkPoint(); // Ef enginn áfangastaður settur fyrir óvin þá keyrir SearchWalkPoint()

        if (walkPointSet)
            agent.SetDestination(walkPoint);

        Vector3 distanceToWalkPoint = transform.position - walkPoint;

        //Walkpoint reached
        if (distanceToWalkPoint.magnitude < 1f) // Ef óvinur kemur innan 1unit frá áfangastað sínum þá...
            walkPointSet = false; // Áfangastaður náður og núlstilltur til að finna nýjan áfangastað.
    }
    private void SearchWalkPoint() // Finnur áfangastað fyrir óvin með random tölum á X- og Y-ás
    {
        // Hér verður teknar random tölur innan marka sem ákveðið er í "Range". Random tölurnar munu svo verða áfangastaður óvinarins.
        float randomZ = Random.Range(-walkPointRange, walkPointRange); // Random tölur á Z-ás fyrir áfangastað óvinar
        float randomX = Random.Range(-walkPointRange, walkPointRange); // Random tölur á X-ás fyrir áfangastað óvinar

        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        if (Physics.Raycast(walkPoint, -transform.up, 2f, whatIsGround)) // Ef áfangastaður er á fleti sem hægt er að labba þá.....
            walkPointSet = true; // Stillt áfangastað óvinar á þann stað sem er ákveðinn af handahófi
    }

    private void ChasePlayer()
    {
        agent.SetDestination(player.position); // Segir óvin að elta staðsetningu spilarans
    }

    private void AttackPlayer()
    {
        //Make sure enemy doesn't move
        agent.SetDestination(transform.position);

        transform.LookAt(player); // Óvinur horfir á spilara

        if (!alreadyAttacked)
        {
            ///Attack code here
            Rigidbody rb = Instantiate(projectile, attackPoint.position, Quaternion.identity).GetComponent<Rigidbody>();
            rb.AddForce(transform.forward * 40f, ForceMode.Impulse); // Hraði á kúlum óvina
            rb.AddForce(transform.up * 2f, ForceMode.Impulse); // Hversu mikið á kúla óvinar að beina upp


            Destroy(rb, 1f);
            alreadyAttacked = true; // Óvinur búinn að skjóta kúlu
            Invoke(nameof(ResetAttack), timeBetweenAttacks); // Keyra ResetAttack()
        }
    }
    private void ResetAttack()
    {
        alreadyAttacked = false; // Endurstillir áras óvinar
    }

    public void TakeDamage(int damage)
    {
        health -= damage; // Minnkar líf óvinar eftir "damage"

        if (health <= 0) Invoke(nameof(DestroyEnemy), 0.5f); // Ef líf óvinar er 0 eða fer undir 0 þá er honum eytt með DestroyEnemy().
    }
    private void DestroyEnemy()
    {
        Destroy(gameObject); // Eyðir óvinar gameobjecti
    }
}
