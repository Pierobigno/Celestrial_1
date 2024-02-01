using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventEffects : MonoBehaviour
{   
    private CharacterStates playerCharacterStates;
    private EnemyDetection enemyDetection;
    private EventStates eventStates;
    private Animator animator;

    [Header("Player Movements")]
    public bool stuckThePlayerIfTriggered;
    public bool unlockThePlayerIfOver;

    [Header("Enemies Movements")]
    public bool stuckEnemiesIfTriggered;
    public bool unlockEnemiesIfOver;

    [Header("Event Behavior")]
    public bool disableEventIfOver;
    public bool destroyEventIfOver;
    public float destroyDelay = 0.1f;

    [Header("Event Animator")]
    public GameObject animatedObject;
    public bool animateIfIsAble;
    public bool animateIfIsTriggered;
    public bool animateIfIsOver;
    private bool animatorTriggered;

    void Start()
    {
        eventStates = GetComponent<EventStates>();
        enemyDetection = GetComponent<EnemyDetection>();
        playerCharacterStates = GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterStates>();

        if(animatedObject != null)
        {
            animator = animatedObject.GetComponentInChildren<Animator>();
        }
    }

    void Update()
    {
        // Player Movements ----------------------------------------------------
        if(eventStates.isTriggered && stuckThePlayerIfTriggered)
        {
            if(!playerCharacterStates.isStuckByEvent)
            {
                playerCharacterStates.isStuckByEvent = true;
            }
        }

        if(eventStates.isOver && unlockThePlayerIfOver)
        {
            if(playerCharacterStates.isStuckByEvent)
            {
                playerCharacterStates.isStuckByEvent = false;
            }
        }

        // Enemies Movements ----------------------------------------------------
        if(enemyDetection != null)
        {
            if(eventStates.isTriggered && stuckEnemiesIfTriggered)
            {
                Collider2D[] enemies = enemyDetection.enemiesInRange;
                foreach(Collider2D enemy in enemies)
                {
                    enemy.transform.root.GetComponentInChildren<CharacterStates>().isStuckByEvent = true;
                }
            }

            if(eventStates.isOver && unlockEnemiesIfOver)
            {
                Collider2D[] enemies = enemyDetection.enemiesInRange;
                foreach(Collider2D enemy in enemies)
                {
                    enemy.transform.root.GetComponentInChildren<CharacterStates>().isStuckByEvent = false;
                }
            }
        }

        // DestroyEvent -------------------------------------------------------
        if(eventStates.isOver && destroyEventIfOver)
        {
            Destroy(gameObject, destroyDelay);
        }

        // DisableEvent -------------------------------------------------------
        if(eventStates.isOver && disableEventIfOver)
        {
            GetComponent<BoxCollider2D>().enabled = false;
        }

        // Animator ------------------------------------------------------------
        if(animateIfIsAble)
        {
            if(eventStates.isAble && !animatorTriggered)
            {
                animatorTriggered = true;
                animator.SetBool("isAble", true);
            }
            else if(!eventStates.isAble && animatorTriggered)
            {
                animatorTriggered = false;
                animator.SetBool("isAble", false);
            }
        }
        
        if(animateIfIsTriggered)
        {
            if(eventStates.isTriggered && !animatorTriggered)
            {
                animatorTriggered = true;
                animator.SetBool("isTriggered", true);
            }
            else if(!eventStates.isTriggered && animatorTriggered)
            {
                animatorTriggered = false;
                animator.SetBool("isTriggered", false);
            }
        }

        if(animateIfIsOver)
        {
            if(eventStates.isOver && !animatorTriggered)
            {
                animatorTriggered = true;
                animator.SetBool("isTriggered", true);
            }
            else if(!eventStates.isOver && animatorTriggered)
            {
                animatorTriggered = false;
                animator.SetBool("isTriggered", false);
            }
        }
    }
}
