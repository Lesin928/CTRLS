using UnityEngine;

public class EnemyBossMoveColliderTrigger : MonoBehaviour
{
    private void MoveCollider1()
    {
        Vector2 size = new Vector2(0.2911969f, 0.2378328f);
        Vector2 offset = new Vector2(0.03274375f, 0.01324288f);
        CapsuleDirection2D direction = CapsuleDirection2D.Horizontal;
        SetColliderSize(size, offset, direction);
    }

    private void MoveCollider2()
    {
        Vector2 size = new Vector2(0.1955425f, 0.3595921f);
        Vector2 offset = new Vector2(0.04800461f, 0.2141294f);
        CapsuleDirection2D direction = CapsuleDirection2D.Vertical;
        SetColliderSize(size, offset, direction);
    }

    private void MoveCollider3()
    {
        Vector2 size = new Vector2(0.2763791f, 0.2763791f);
        Vector2 offset = new Vector2(0.03136045f, 0.3139893f);
        CapsuleDirection2D direction = CapsuleDirection2D.Horizontal;
        SetColliderSize(size, offset, direction);
    }

    private void SetColliderSize(Vector2 size, Vector2 offset, CapsuleDirection2D direction)
    {
        var collider = transform.parent.GetComponent<CapsuleCollider2D>();

        if (collider != null)
        {
            collider.direction = direction;
            collider.size = size;
            collider.offset = offset;
        }
    }
}
