using UnityEngine;

public partial class Player
{
    /// <summary>
    /// 通常の移動状態
    /// </summary>
    public class StateMove : PlayerStateBase
    {
        public override void OnUpdate(Player owner)
        {
            owner.MoveProc(owner.inputValue, owner.velocity, owner.maxNormalVel, owner.normalACC);

            // ジャンプ状態に遷移
            if(Input.GetKey(owner.jumpKey) && owner.isGround)
            {
                owner.ChangeMoveState(jump);
            }
            //通常状態に遷移
            else if (owner.inputValue.x == 0 && owner.inputValue.y == 0)
            {
                owner.ChangeMoveState(idol);
            }
            //ダッシュ状態に遷移
            else if (Input.GetKey(owner.dashKey) && owner.isGround)
            {
                owner.ChangeMoveState(dash);
            }
        }
    }

    /// <summary>
    /// 移動処理
    /// </summary>
    /// <param name="_MoveX">        左右の動きの割合   </param>
    /// <param name="_MoveZ">        前後の動きの割合   </param>
    /// <param name="_Velocity">     現在の移動速度     </param>
    /// <param name="_MaxVelocity">  移動速度の最大値   </param>
    /// <param name="_Acceleration"> 加速度             </param>
    private void MoveProc(Vector2 _InputValue, float _Velocity, float _MaxVelocity, float _Acceleration)
    {
        //上書きされる前に落下の力を記録
        float forceY = rb.velocity.y;
        //速度を正規化した移動量にする
        rb.velocity = new Vector3(_InputValue.x, 0, _InputValue.y).normalized;

        //加速上限と加速度から速度を求める
        velocity = Accelerate(_Velocity, _MaxVelocity, _Acceleration);
        //↑のvelocityは引数を使うと移動が全然できなくなる。解明できたら直す

        //速度を移動量に乗算する
        rb.velocity *= _Velocity;
        //落下の力を復帰させる
        rb.velocity = new Vector3(rb.velocity.x, forceY, rb.velocity.z);
    }

    /// <summary>
    /// 加減速処理
    /// </summary>
    /// <param name="_Velocity">現在の速度</param>
    /// <param name="_MaxVelocity">加速の目標速度</param>
    /// <param name="_Acceleration">加速度</param>
    /// <returns></returns>
    private float Accelerate(float _Velocity, float _MaxVelocity, float _Acceleration)
    {
        //移動速度の上限を超えてなければ上限まで加速
        if (_Velocity < _MaxVelocity)
        {
            //時間積分から速度を求める
            _Velocity += _Acceleration * Time.deltaTime;
            //加速後に上限を超えたら矯正する
            if (_Velocity > _MaxVelocity)
            {
                _Velocity = _MaxVelocity;
            }
        }
        //そうでなければ上限まで減速
        else if (_Velocity > _MaxVelocity)
        {
            _Velocity -= _Acceleration * Time.deltaTime;
            //減速後に上限を下回ったら矯正する
            if (_Velocity < _MaxVelocity)
            {
                _Velocity = _MaxVelocity;
            }
        }
        return _Velocity;
    }
}