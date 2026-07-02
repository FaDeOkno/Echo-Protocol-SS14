using Content.Server.Actions;
using Content.Server.DoAfter;
using Content.Shared._ECHO.Customization;
using Content.Shared.Body;
using Content.Shared.DoAfter;
using Content.Shared.Humanoid;
using Content.Shared.Humanoid.Markings;
using Robust.Shared.Prototypes;

namespace Content.Server._ECHO.Customization;

public sealed class MidroundCustomizationSystem : SharedMidroundCustomizationSystem
{
    [Dependency] private readonly ActionsSystem _actions = default!;
    [Dependency] private readonly DoAfterSystem _doAFter = default!;

    public override void Initialize()
    {
        base.Initialize();

        SubscribeLocalEvent<MidroundCustomizationComponent, MapInitEvent>(OnMapInit);
        SubscribeLocalEvent<MidroundCustomizationComponent, ComponentShutdown>(OnShutdown);
        SubscribeLocalEvent<MidroundCustomizationComponent, ApplyMidroundCustomizationMarkingsDoAfterEvent>(OnApplyDoAfter);
    }

    private void OnMapInit(Entity<MidroundCustomizationComponent> ent, ref MapInitEvent args)
    {
        _actions.AddAction(ent.Owner, ref ent.Comp.MenuAction, ent.Comp.ActionId);
    }

    private void OnShutdown(Entity<MidroundCustomizationComponent> ent, ref ComponentShutdown args)
    {
        _actions.RemoveAction(ent.Comp.MenuAction);
    }

    private void OnApplyDoAfter(Entity<MidroundCustomizationComponent> ent, ref ApplyMidroundCustomizationMarkingsDoAfterEvent args)
    {
        ent.Comp.AppearanceChangeDoAfter = null;
        if (args.Cancelled)
            return;

        VisualBody.ApplyMarkings(ent.Owner, args.Markings);
        UpdateUi(ent);
    }

    protected override void StartChangeDoAfter(Entity<MidroundCustomizationComponent> ent, Dictionary<ProtoId<OrganCategoryPrototype>, Dictionary<HumanoidVisualLayers, List<Marking>>> markings)
    {
        if (ent.Comp.AppearanceChangeDoAfter != null)
        {
            _doAFter.Cancel(ent.Comp.AppearanceChangeDoAfter.Value);
        }

        var ev = new ApplyMidroundCustomizationMarkingsDoAfterEvent(markings);
        var doAfterArgs = new DoAfterArgs(EntityManager, ent.Owner, ent.Comp.AppearanceChangeDuration, ev, ent.Owner)
        {
            BreakOnHandChange = false,
            BreakOnMove = false
        };

        _doAFter.TryStartDoAfter(doAfterArgs, out ent.Comp.AppearanceChangeDoAfter);
    }
}
