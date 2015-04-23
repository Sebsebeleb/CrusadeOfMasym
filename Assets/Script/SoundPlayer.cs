using Assets.Script;
using UnityEngine;

public class SoundPlayer : MonoBehaviour
{
    public AudioClip AttackSound;
    public AudioClip DeathSound;
    public AudioClip HitSound;
    public AudioClip SpawnSound;

    private CreatureStats owner;
    private AudioSource audio;

    void OnDestroy()
    {
        EventManager.OnCreatureSpawned -= EventManagerOnOnCreatureSpawned;
        EventManager.OnCreatureAttack -= EventManagerOnOnCreatureAttack;
        EventManager.OnCreatureAttacked -= EventManagerOnOnCreatureAttacked;
        EventManager.OnPermanentDestroyed -= EventManagerOnOnPermanentDestroyed;

    }

    void Awake()
    {
        audio = GetComponent<AudioSource>();
        owner = GetComponent<CreatureStats>();
        EventManager.OnCreatureSpawned += EventManagerOnOnCreatureSpawned;
        EventManager.OnCreatureAttack += EventManagerOnOnCreatureAttack;
        EventManager.OnPermanentDestroyed += EventManagerOnOnPermanentDestroyed;
        EventManager.OnCreatureAttacked += EventManagerOnOnCreatureAttacked;
    }

    private void EventManagerOnOnCreatureAttacked(CreatureStats creature, CreatureStats source)
    {
        Debug.Log("Hello something is attacked. Is it us?: " + creature + " == " + owner);
        if (creature == owner)
        {
            LeanTween.delayedCall(0.4f, o => PlayClip(HitSound));
        }
    }

    private void EventManagerOnOnPermanentDestroyed(CreatureStats creature, Source killSource)
    {
        Debug.Log("Hello something is dying. Is it us?: " + creature + " == " + owner);
        if (creature == owner)
        {
            // Ah yes, not makeshift at all...
            GameObject obj = Instantiate(new GameObject());
            AudioSource aud = obj.AddComponent<AudioSource>();

            aud.clip = DeathSound;
            aud.Play();
            LeanTween.delayedCall(5f, o => Destroy(obj));
        }
    }

    private void EventManagerOnOnCreatureAttack(CreatureStats attacker, CreatureStats target, Damage damageDone)
    {
        if (attacker == owner)
        {
            PlayClip(AttackSound);
        }
    }

    private void EventManagerOnOnCreatureSpawned(CreatureStats creature, MapPosition at)
    {
        Debug.Log(creature);
        Debug.Log(owner);
        if (creature == owner)
        {
            PlayClip(SpawnSound);
        }
    }

    void Update()
    {

    }

    private void PlayClip(AudioClip clip)
    {
        Debug.Log("Lets play");
        audio.clip = clip;
        audio.Play();
    }
}