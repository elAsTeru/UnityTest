using UnityEngine;

public partial class Player
{
    public class StateSpin : PlayerStateBase
    {
        public override void OnUpdate(Player owner)
        {
            float deltatime = 0.0f;
            deltatime += Time.deltaTime;
            if(deltatime > 5.0f)
            {
                owner.ChangeState(idol);
            }
        }
    }
}