public class cBullet
{
    public enum HEAD_TYPE { FMJ, HE, RICOCHET }
    public enum PRIMER_TYPE { NONE, HEALING }

    public HEAD_TYPE HeadType { get; private set; }
    public PRIMER_TYPE PrimerType { get; private set; }

    // Bullet speed
    public float ProjSpeed { get; private set; }
    // Value from 0 to 100 to represent how much visual recoil the player gets
    public float RecoilAdj { get; private set; }
    // Knockback force to apply to player in m/s
    public float Knockback { get; private set; }
    // Explosion radius for explosive bullets

    public cBullet(GenericItem[] items)
    {
        foreach (GenericItem i in items)
        {
            if (i is Bullet)
            {
                if (i is ExplosiveBullet){
                    HeadType = HEAD_TYPE.HE;
                } else {
                    HeadType = HEAD_TYPE.FMJ;
                }
            }

            if (i is Gunpowder)
            {
                ProjSpeed += 75;
                RecoilAdj += 1;
            }

            if (i is Primer)
            {
                Knockback += 10;
                if (i is HealingPrimer){
                    PrimerType = PRIMER_TYPE.HEALING;
                } else {
                    PrimerType = PRIMER_TYPE.NONE;
                }
            }
        }
    }

    public static cBullet GetGenericBullet()
    {
        return new cBullet(new GenericItem[] { new Bullet(), new Gunpowder(), new Primer() });
    }
}