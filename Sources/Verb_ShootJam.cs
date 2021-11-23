using RimWorld;
using UnityEngine;
using Verse;
using Verse.Sound;

namespace Industrial_Revolution
{
    public class Verb_ShootJam : Verb_LaunchProjectile
    {
        public bool isJammed;
        public SoundDef jamSound = SoundDef.Named("Misfire");

        protected virtual int ShotsPerBurst => (int)((VerbProperties)((Verb)this).verbProps).burstShotCount;

        public virtual void WarmupComplete()
        {
            if (this.isJammed)
            {
                if (new System.Random().Next(0, 2) != 1)
                    return;
                this.isJammed = false;
                MoteMaker.ThrowText(new Vector3((float)((Thing)((Verb)this).caster).Position.x + 1f, (float)((Thing)((Verb)this).caster).Position.y, (float)((Thing)((Verb)this).caster).Position.z + 1f), ((Thing)((Verb)this).caster).Map, "Jam Cleared", Color.white, -1f);
            }
            else
            {
                QualityCategory qualityCategory;
                QualityUtility.TryGetQuality((Thing)((Verb)this).caster, out qualityCategory);
                int num1;
                switch ((int)qualityCategory)
                {
                    case 0:
                        num1 = 30;
                        break;
                    case 1:
                        num1 = 50;
                        break;
                    case 2:
                        num1 = 40;
                        break;
                    case 3:
                        num1 = 60;
                        break;
                    case 4:
                        num1 = 70;
                        break;
                    case 5:
                        num1 = 90;
                        break;
                    case 6:
                        num1 = 80;
                        break;
                    case 7:
                        num1 = 100;
                        break;
                    case 8:
                        num1 = 10000;
                        break;
                    default:
                        num1 = 60;
                        break;
                }
                if (Rand.Range(1, num1) == 1)
                {
                    MoteMaker.ThrowText(new Vector3((float)((Thing)((Verb)this).caster).Position.x + 1f, (float)((Thing)((Verb)this).caster).Position.y, (float)((Thing)((Verb)this).caster).Position.z + 1f), ((Thing)((Verb)this).caster).Map, "Jammed", Color.white, -1f);
                    this.isJammed = true;
                }
                else
                {
                    ((Verb)this).WarmupComplete();
                    if (!((Verb)this).CasterIsPawn || ((Verb)this).CasterPawn.skills == null)
                        return;
                    float num2 = 10f;
                    if (((LocalTargetInfo) ((Verb)this).currentTarget).Thing != null )
                        num2 = !GenHostility.HostileTo(((LocalTargetInfo) ((Verb)this).currentTarget).Thing, (Thing)((Verb)this).caster) ? 50f : 240f;
                    ((Pawn_SkillTracker)((Verb)this).CasterPawn.skills).Learn((SkillDef)SkillDefOf.Shooting, num2, false);
                }
            }
        }
    }
}
