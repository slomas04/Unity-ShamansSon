using UnityEngine;

public class cBullet
{
    public enum HEAD_TYPE { FMJ, HP, BLANK, HE, BUCK, DU, FRANCE }

    public HEAD_TYPE HeadType { get; private set;}

    // Bullet speed
    public float ProjSpeed { get;  private set;}
    // Value from 0 to 100 to represent how much visual recoil the player gets
    public float RecoilAdj { get;  private set;}
    // Knockback force to apply to player in m/s
    public float Knockback { get;  private set;}


    // TODO:: THIS IS A STUB, IMPLEMENT PROPERLY LATER
    public cBullet(GenericItem[] items)
    {
        foreach(GenericItem i in items)
        {
            if (i is Bullet)
            {
                HeadType = HEAD_TYPE.FMJ;
            }

            if (i is Gunpowder)
            {
                ProjSpeed += 75;
                RecoilAdj += 1;
            }

            if (i is Primer)
            {
                Knockback += 10;
            }
        }
    }
}
