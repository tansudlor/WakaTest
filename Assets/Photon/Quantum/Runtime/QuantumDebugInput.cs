namespace Quantum {
  using Photon.Deterministic;
  using UnityEngine;

  /// <summary>
  /// A Unity script that creates empty input for any Quantum game.
  /// </summary>
  public class QuantumDebugInput : MonoBehaviour {

    private void OnEnable() {
      QuantumCallback.Subscribe(this, (CallbackPollInput callback) => PollInput(callback));
    }

    /// <summary>
    /// Set an empty input when polled by the simulation.
    /// </summary>
    /// <param name="callback"></param>
    public void PollInput(CallbackPollInput callback) {
      Quantum.Input input = new Quantum.Input();

      float x = UnityEngine.Input.GetAxis("Horizontal");
      float y = UnityEngine.Input.GetAxis("Vertical");

      input.Direction  = new FPVector2(x.ToFP(), y.ToFP());
      input.Jump = UnityEngine.Input.GetButton("Jump");

      callback.SetInput(input, DeterministicInputFlags.Repeatable);
    }
  }
}
