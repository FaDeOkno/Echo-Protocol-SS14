using Content.Server.Actions;
using Content.Shared._ECHO.Customization;

namespace Content.Server._ECHO.Customization;

public sealed class CustomizableAppearanceSystem : SharedCustomizableAppearanceSystem
{
    [Dependency] private readonly ActionsSystem _actions = default!;

    public override void Initialize()
    {
        base.Initialize();

        SubscribeLocalEvent<CustomizableAppearanceComponent, MapInitEvent>(OnMapInit);
        SubscribeLocalEvent<CustomizableAppearanceComponent, ComponentShutdown>(OnShutdown);
    }

    private void OnMapInit(Entity<CustomizableAppearanceComponent> ent, ref MapInitEvent args)
    {
        _actions.AddAction(ent.Owner, ref ent.Comp.MenuAction, ent.Comp.ActionId);
    }

    private void OnShutdown(Entity<CustomizableAppearanceComponent> ent, ref ComponentShutdown args)
    {
        _actions.RemoveAction(ent.Comp.MenuAction);
    }
}
