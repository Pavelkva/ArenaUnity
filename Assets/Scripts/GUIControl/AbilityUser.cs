using Abilities;
using Assigners;
using characters;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace Gui
{ 
    public class AbilityUser : MonoBehaviour
    {
        public bool AbilityInUse { get; set; }

        public void Use(GameObject casterGO, GameObject targetGO, Ability ability)
        {
            AbilityInUse = true;
            Animator animator = casterGO.GetComponent<Animator>();
            Fighter caster = casterGO.GetComponent<Fighter>();
            Fighter target = targetGO.GetComponent<Fighter>();

            if (!ability.IsAbleTouse(caster, target))
            {
                AbilityInUse = false;
                return;
            }

            if (AnimationAssigner.GetAnimationName(ability.Id) != null)
            {
                animator.Play(AnimationAssigner.GetAnimationName(ability.Id)[0]);
            }

            StartCoroutine(CreateParticle(ParticleAssigner.GetParticlesSettings(ability.Id), ability, caster, target, casterGO, targetGO, 0, false));

            caster.UseAbility(ability, target, true);
        }

        private IEnumerator CreateParticle(ParticleAssigner.PrefabSettings[] prefabSettings, Ability ability, Fighter caster, Fighter target, GameObject casterGO, GameObject targetGO, int indexForuse, bool used)
        {
            Transform startPosition = casterGO.transform;
            Vector3 particleTarget = casterGO.transform.position;
            if (prefabSettings != null && prefabSettings.Length > 0)
            {
                if (indexForuse == prefabSettings[0].IndexOfUse && !used)
                {
                    used = true;
                    caster.UseAbility(ability, target, false);
                }
                foreach (ParticleAssigner.PrefabSettings prefabSetting in prefabSettings)
                {
                    switch (prefabSetting.PrefabMovement)
                    {
                        case ParticleAssigner.PrefabSettings.Movement.MOVETARGETCASTER:
                            startPosition = targetGO.transform;
                            particleTarget = targetGO.transform.position;
                            break;
                        case ParticleAssigner.PrefabSettings.Movement.STATICTARGET:
                            startPosition = targetGO.transform;
                            particleTarget = new Vector3(targetGO.transform.position.x, targetGO.transform.position.y + prefabSetting.PositionY, targetGO.transform.position.z);
                            break;
                        case ParticleAssigner.PrefabSettings.Movement.MOVECASTERTARGET:
                            startPosition = casterGO.transform;
                            particleTarget = new Vector3(targetGO.transform.position.x, targetGO.transform.position.y + prefabSetting.PositionY, targetGO.transform.position.z);
                            break;
                        case ParticleAssigner.PrefabSettings.Movement.STATICCASTER:
                            startPosition = casterGO.transform;
                            particleTarget = casterGO.transform.position;
                            break;
                        default:
                            startPosition = casterGO.transform;
                            particleTarget = new Vector3(targetGO.transform.position.x, targetGO.transform.position.y + prefabSetting.PositionY, targetGO.transform.position.z);
                            break;
                    }

                    GameObject prefabNew = Resources.Load(prefabSetting.PrefabName, typeof(GameObject)) as GameObject;
                    GameObject prefabInsta = Instantiate(prefabNew, startPosition);
                    prefabInsta.transform.Rotate(new Vector3(0, 0, prefabSetting.RotationZ));
                    prefabInsta.transform.position = new Vector3(prefabInsta.transform.position.x, prefabInsta.transform.position.y + prefabSetting.PositionY, prefabInsta.transform.position.z);


                    float distance = Vector3.Distance(prefabInsta.transform.position, particleTarget);
                    var currentPos = prefabInsta.transform.position;
                    var t = 0f;
                    while (t < 1)
                    {
                        if (prefabSetting.PrefabMovement == ParticleAssigner.PrefabSettings.Movement.MOVECASTERTARGET || prefabSetting.PrefabMovement == ParticleAssigner.PrefabSettings.Movement.MOVETARGETCASTER)
                        {
                            t += Time.deltaTime / (prefabSetting.TimeToGetTarget * (distance / 10));
                        }
                        else
                        {
                            t += Time.deltaTime / (prefabSetting.TimeToGetTarget);
                        }
                        prefabInsta.transform.position = Vector3.Lerp(currentPos, particleTarget, t);
                        yield return null;
                    }



                    Destroy(prefabInsta.transform.gameObject);
                    StartCoroutine(CreateParticle(prefabSetting.EffectAfter, ability, caster, target, casterGO, targetGO, indexForuse + 1, used));
                }

            }
            else if (!used)
            {
                caster.UseAbility(ability, target, false);
            }
            yield return new WaitForSeconds(1);
            AbilityInUse = false;

        }
    }

   
}

