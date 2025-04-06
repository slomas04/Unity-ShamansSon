using UnityEngine;

public class cBullet
{
    public enum HEAD_TYPE { FMJ, HP, BLANK, HE, BUCK, DU, FRANCE }

    private HEAD_TYPE HeadType { get; }

    // How long (in ms) to wait between hammer drop and bullet shoot
    private double FireDelay { get; }
    // Bullet speed in m/s (160gr JHP for SAA fires at 343m/s)
    private double ProjSpeed { get; }
    // Value from 0 to 100 to represent how much visual recoil the player gets
    private double RecoilAdj { get; }
    // Knockback force to apply to player in m/s
    private double Knockback { get; }


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
                ProjSpeed += 1;
                RecoilAdj += 1;
            }

            if (i is Primer)
            {
                Knockback += 1;
            }
        }
    }
}
