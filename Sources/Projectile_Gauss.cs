using RimWorld;
using System;
using UnityEngine;
using Verse;
using Verse.Sound;

namespace Industrial_Revolution
{
    public class Projectile_Gauss : Projectile
    {
        public int tickCounter;
        public Thing hitThing;
        public CompExtraDamage compED;

        public virtual void SpawnSetup(Map map, bool respawningAfterLoad)
        {
            ((ThingWithComps)this).SpawnSetup(map, respawningAfterLoad);
            this.compED = (CompExtraDamage)((ThingWithComps)this).GetComp<CompExtraDamage>();
        }

        protected virtual void Impact(Thing hitThing)
        {
            Map map = ((Thing)this).Map;
            base.Impact(hitThing);
            if (hitThing != null)
            {
                int damageAmountBase = (int)((ProjectileProperties)((ThingDef)((Thing)this).def).projectile).damageAmountBase;
                ThingDef equipmentDef = (ThingDef)this.equipmentDef;
				// ISSUE: variable of the null type
				Verse.DamageDef damageDef = def.projectile.damageDef;
                int num = damageAmountBase;
                Quaternion exactRotation1 = this.ExactRotation;
                ThingDef thingDef = equipmentDef;
                // ISSUE: explicit constructor call
                if (!(hitThing is Pawn pawn2) || pawn2.Downed || (double)Rand.Value >= (double)this.compED.chanceToProc)
                    return;
                Thing thing = hitThing;
                DamageDef named = DefDatabase<DamageDef>.GetNamed(this.compED.damageDef, true);
                int damageAmount = this.compED.damageAmount;
                Quaternion exactRotation2 = this.ExactRotation;
                // ISSUE: variable of the null type
                DamageInfo damageInfo2 = new DamageInfo(named, damageAmount, (float)(DamageInfo.SourceCategory)0);
                thing.TakeDamage(damageInfo2);
            }
            else
            {
                SoundStarter.PlayOneShot((SoundDef)SoundDefOf.BulletImpact_Ground, SoundInfo.InMap(new TargetInfo(((Thing)this).Position, map, false)));
                MoteMaker.MakeStaticMote(this.ExactPosition, map, (ThingDef)ThingDefOf.Filth_Dirt, 1f);
                Projectile_Gauss.ThrowMicroSparksBlue(this.ExactPosition, ((Thing)this).Map);
            }
        }

        public static void ThrowMicroSparksBlue(Vector3 loc, Map map)
        {
            if (!GenView.ShouldSpawnMotesAt(loc, map) || ((MoteCounter)map.moteCounter).SaturatedLowPriority)
                return;
            MoteThrown moteThrown = (MoteThrown)ThingMaker.MakeThing(ThingDef.Named("Mote_MicroSparksBlue"), (ThingDef)null);
            ((Mote)moteThrown).Scale = Rand.Range(0.8f, 1.2f);
            ((Mote)moteThrown).rotationRate = Rand.Range(-12f, 12f);
            ((Mote)moteThrown).exactPosition = loc;
            ((Mote)moteThrown).exactPosition = new NewStruct(moteThrown.exactPosition, new Vector3(0.5f, 0.0f, 0.5f));
            ((Mote)moteThrown).exactPosition = new NewStruct1(moteThrown.exactPosition, new Vector3(Rand.Value, 0.0f, Rand.Value));
            moteThrown.SetVelocity((float)Rand.Range(35, 45), 1.2f);
            GenSpawn.Spawn((Thing)moteThrown, IntVec3Utility.ToIntVec3(loc), map);
        }
    }

	internal struct NewStruct
	{
		public Vector3 exactPosition;
		public Vector3 Item2;

		public NewStruct(Vector3 exactPosition, Vector3 item2)
		{
			this.exactPosition = exactPosition;
			Item2 = item2;
		}

		public override bool Equals(object obj)
		{
			return obj is NewStruct other &&
				   exactPosition.Equals(other.exactPosition) &&
				   Item2.Equals(other.Item2);
		}

		public override int GetHashCode()
		{
			int hashCode = 108502699;
			hashCode = hashCode * -1521134295 + exactPosition.GetHashCode();
			hashCode = hashCode * -1521134295 + Item2.GetHashCode();
			return hashCode;
		}

		public void Deconstruct(out Vector3 exactPosition, out Vector3 item2)
		{
			exactPosition = this.exactPosition;
			item2 = Item2;
		}

		public static implicit operator (Vector3 exactPosition, Vector3)(NewStruct value)
		{
			return (value.exactPosition, value.Item2);
		}

		public static implicit operator NewStruct((Vector3 exactPosition, Vector3) value)
		{
			return new NewStruct(value.exactPosition, value.Item2);
		}

		public static implicit operator Vector3(NewStruct v)
		{
			throw new NotImplementedException();
		}
	}

	internal struct NewStruct1
	{
		public Vector3 exactPosition;
		public Vector3 Item2;

		public NewStruct1(Vector3 exactPosition, Vector3 item2)
		{
			this.exactPosition = exactPosition;
			Item2 = item2;
		}

		public override bool Equals(object obj)
		{
			return obj is NewStruct1 other &&
				   exactPosition.Equals(other.exactPosition) &&
				   Item2.Equals(other.Item2);
		}

		public override int GetHashCode()
		{
			int hashCode = 108502699;
			hashCode = hashCode * -1521134295 + exactPosition.GetHashCode();
			hashCode = hashCode * -1521134295 + Item2.GetHashCode();
			return hashCode;
		}

		public void Deconstruct(out Vector3 exactPosition, out Vector3 item2)
		{
			exactPosition = this.exactPosition;
			item2 = Item2;
		}

		public static implicit operator (Vector3 exactPosition, Vector3)(NewStruct1 value)
		{
			return (value.exactPosition, value.Item2);
		}

		public static implicit operator NewStruct1((Vector3 exactPosition, Vector3) value)
		{
			return new NewStruct1(value.exactPosition, value.Item2);
		}

		public static implicit operator Vector3(NewStruct1 v)
		{
			throw new NotImplementedException();
		}
	}
}
