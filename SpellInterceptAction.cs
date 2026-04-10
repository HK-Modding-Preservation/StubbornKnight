using HutongGames.PlayMaker;

namespace StubbornKnight;

public class SpellInterceptAction : FsmStateAction
{
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

        ArrowDirection spellDir;
        StubbornKnight.TryGetCurrentIntentDirection(out spellDir);
        bool isSuccess = arrowGame.IsSpellAllowed(spellDir);

        if (!isSuccess)
        {
            arrowGame.TriggerErrorEffect();
            Fsm.Event("FSM CANCEL");
        }
        else
        {
            arrowGame.OnSuccessfulAction();
        }

        Finish();
    }
}
