using Content.Server.Actions;
using Content.Server.DoAfter;
using Content.Shared._ECHO.Customization;
using Content.Shared.Body;
using Content.Shared.DoAfter;
using Content.Shared.Humanoid;
using Content.Shared.Humanoid.Markings;
using Robust.Shared.Prototypes;

namespace Content.Server._ECHO.Customization;

public sealed class CustomizableAppearanceSystem : SharedCustomizableAppearanceSystem
{
    [Dependency] private readonly ActionsSystem _actions = default!;
    [Dependency] private readonly DoAfterSystem _doAFter = default!;

    public override void Initialize()
    {
        base.Initialize();

        SubscribeLocalEvent<CustomizableAppearanceComponent, MapInitEvent>(OnMapInit);
        SubscribeLocalEvent<CustomizableAppearanceComponent, ComponentShutdown>(OnShutdown);
        SubscribeLocalEvent<CustomizableAppearanceComponent, ApplyCustomizableAppearanceMarkingsDoAfterEvent>(OnApplyDoAfter);
    }

    private void OnMapInit(Entity<CustomizableAppearanceComponent> ent, ref MapInitEvent args)
    {
        _actions.AddAction(ent.Owner, ref ent.Comp.MenuAction, ent.Comp.ActionId);
    }

    private void OnShutdown(Entity<CustomizableAppearanceComponent> ent, ref ComponentShutdown args)
    {
        _actions.RemoveAction(ent.Comp.MenuAction);
    }

    private void OnApplyDoAfter(Entity<CustomizableAppearanceComponent> ent, ref ApplyCustomizableAppearanceMarkingsDoAfterEvent args)
    {
        ent.Comp.AppearanceChangeDoAfter = null;
        if (args.Cancelled)
            return;

        VisualBody.ApplyMarkings(ent.Owner, args.Markings);
        UpdateUi(ent);
    }

    protected override void StartChangeDoAfter(Entity<CustomizableAppearanceComponent> ent, Dictionary<ProtoId<OrganCategoryPrototype>, Dictionary<HumanoidVisualLayers, List<Marking>>> markings)
    {
        if (ent.Comp.AppearanceChangeDoAfter != null)
        {
            _doAFter.Cancel(ent.Comp.AppearanceChangeDoAfter.Value);
        }

        var ev = new ApplyCustomizableAppearanceMarkingsDoAfterEvent(markings);
        var doAfterArgs = new DoAfterArgs(EntityManager, ent.Owner, ent.Comp.AppearanceChangeDuration, ev, ent.Owner)
        {
            BreakOnHandChange = false,
            BreakOnMove = false
        };

        _doAFter.TryStartDoAfter(doAfterArgs, out ent.Comp.AppearanceChangeDoAfter);
    }
}
