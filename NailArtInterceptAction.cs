using HutongGames.PlayMaker;

namespace StubbornKnight;

public class NailArtInterceptAction : FsmStateAction
{
    public ArrowDirection ExpectedDirection;
    public string NailArtName;

    public override void OnEnter()
    {
        if (!StubbornKnight.IsModEnabled)
        {
            Finish();
            return;
        }

        var arrowGame = HeroController.instance?.GetComponent<ArrowGame>();
        if (arrowGame == null)
        {
            Finish();
            return;
        }

        ArrowDirection actualDir;

        if (ExpectedDirection == ArrowDirection.Left || ExpectedDirection == ArrowDirection.Right)
        {
            actualDir = HeroController.instance.cState.facingRight ? ArrowDirection.Right : ArrowDirection.Left;
        }
        else
        {
            StubbornKnight.TryGetCurrentIntentDirection(out actualDir);
        }

        bool isSuccess = arrowGame.IsSpellAllowed(actualDir);

        if (!isSuccess)
        {
            arrowGame.TriggerErrorEffect();
            Fsm.Event("CANCEL");
        }
        else
        {
            arrowGame.OnSuccessfulAction();
        }

        Finish();
    }
}
