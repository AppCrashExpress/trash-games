using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotController : ControllerBase
{
    private enum RacketType
    {
        left,
        right
    };

    public float m_FieldHeight;

    private RacketType m_RacketType;
    private Transform m_BallTransform;
    private Rigidbody2D m_BallRigidbody;
    void Start()
    {
        GameObject ball = GameObject.FindGameObjectWithTag("Ball");
        m_BallTransform = ball.transform;
        m_BallRigidbody = ball.GetComponent<Rigidbody2D>();

        GameObject[] rackets = GameObject.FindGameObjectsWithTag("Racket");
        
        GameObject other;
        if (rackets[0] == gameObject)
            other = rackets[1];
        else
            other = rackets[0];

        if (transform.position.x < other.transform.position.x)
            m_RacketType = RacketType.left;
        else
            m_RacketType = RacketType.right;
    }

    public override float GetMovement()
    {
        bool ballIncoming = CheckBallIncoming();
        if (!ballIncoming)
            return 0.0f;
/*
        Vector2 ballPos = m_BallTransform.position;
        Vector2 ballVelocity = m_BallRigidbody.velocity;
        float slope = ballVelocity.y / ballVelocity.x;
        float intercept = -ballPos.x * slope + ballPos.y;
        float val = (slope * transform.position.y + intercept) % (2 * m_FieldHeight);
        float yPred = Mathf.Min(val, 2 * m_FieldHeight - val);

        if (Mathf.Abs(transform.position.y - yPred) < 0.3f
            return 0.0f;
        else if (transform.position.y > yPred)
            return -1.0f;
        else
            return 1.0f;
*/

        Vector2 pos = transform.position;
        Vector2 ballPos = m_BallTransform.position;

        if (Mathf.Abs(ballPos.y - pos.y) < 0.3f)
            return 0.0f;
        else if (ballPos.y > pos.y)
            return 1.0f;
        else
            return -1.0f;
    }

    private bool CheckBallIncoming()
    {
        float velocity = m_BallRigidbody.velocity.x;
        return m_RacketType == RacketType.left && velocity < 0.0f || m_RacketType == RacketType.right && velocity > 0.0f;
    }
}
