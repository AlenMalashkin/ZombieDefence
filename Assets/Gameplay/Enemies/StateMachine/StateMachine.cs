using UnityEngine;

public class StateMachine : MonoBehaviour
{
    private State _state;

    public void SetState(State state)
    {
        if (_state != null)
            _state.ExitState();
        
        _state = state;
        _state.EnterState();
    }

    private void Update()
    {
        _state.UpdateState();
    }
}
