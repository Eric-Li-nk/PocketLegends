using UnityEngine;

public class AnimationManager : MonoBehaviour
{
    [SerializeField] private Rigidbody body;
    [SerializeField] private CharacterController controller;
    [SerializeField] private Animator animator;
    
    private int _isRunning;
    // Start is called before the first frame update
    void Start()
    {
        _isRunning = Animator.StringToHash("IsRunning");
    }

    // Update is called once per frame
    /*void Update()
    {
        if (body.velocity.magnitude > 1f && controller.Constraints.CanJump)
        {
            animator.SetBool(_isRunning, true);
        }
        else
        {
            animator.SetBool(_isRunning, false);
        }
    }*/
}